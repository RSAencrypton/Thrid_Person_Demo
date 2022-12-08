using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rootMotionControl : MonoBehaviour
{
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnAnimatorMove()
    {
        SendMessageUpwards("onRootMotionUpdate",(object)anim.deltaPosition);
    }
}
