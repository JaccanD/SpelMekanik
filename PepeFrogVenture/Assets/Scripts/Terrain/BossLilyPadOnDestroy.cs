using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Callback;

public class BossLilyPadOnDestroy : MonoBehaviour
{
    private void OnDestroy()
    {
        EventSystem.Current.FireEvent(new LilyPadDestroyedEvent(this.gameObject));
    }
}
