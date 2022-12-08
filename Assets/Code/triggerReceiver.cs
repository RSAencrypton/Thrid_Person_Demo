using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerReceiver : MonoBehaviour
{

    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void resetTrigger(string triggerName) {
        anim.ResetTrigger(triggerName);
    }
}
