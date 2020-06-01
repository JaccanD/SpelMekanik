using System.Collections.Generic;
using UnityEngine;
using Callback;

namespace EnemyAI
{
    public class EnemyCoordinator : MonoBehaviour
    {
        static private EnemyCoordinator currentCoordinator;

        private List<Enemy> enemiesThatSeeThePlayer = new List<Enemy>();
        private List<Enemy> enemiesEngaged = new List<Enemy>();
        private void Awake()
        {
            EventSystem.Current.RegisterListener(typeof(EnemyDeathEvent), EnemyDead);
        }
        static public EnemyCoordinator current
        {
            get
            {
                if (currentCoordinator == null)
                {
                    currentCoordinator = FindObjectOfType<EnemyCoordinator>();
                }
                return currentCoordinator;
            }
        }
        public void AddEngagedEnemy(Enemy enemy)
        {
            if (!enemiesEngaged.Contains(enemy))
            {
                enemiesEngaged.Add(enemy);
            }
        }
        public void AddEnemyInRange(Enemy enemy)
        {
            if(enemiesThatSeeThePlayer.Contains(enemy))
            {
                return;
            }
            enemiesThatSeeThePlayer.Add(enemy);
            if (enemiesEngaged.Contains(enemy))
            {
                enemiesEngaged.Remove(enemy);
            }
        }
        public void RemoveEngagedEnemy(Enemy enemy)
        {
            enemiesEngaged.Remove(enemy);
        }
        public void RemoveEnemyInRangeOfPlayer(Enemy enemy)
        {
            enemiesThatSeeThePlayer.Remove(enemy);
            AddEngagedEnemy(enemy);
            if(enemiesThatSeeThePlayer.Count == 0)
            {
                EventSystem.Current.FireEvent(new PlayerLostEvent());
                enemiesEngaged.Clear();
            }
        }
        private void EnemyDead(Callback.Event eb)
        {
            EnemyDeathEvent e = (EnemyDeathEvent)eb;
            RemoveEnemyInRangeOfPlayer(e.enemy);
            RemoveEngagedEnemy(e.enemy);
        }
    }
}