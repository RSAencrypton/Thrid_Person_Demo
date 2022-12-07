using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anotherMethodForControl;

public class leftWeaponFix : MonoBehaviour
{
    Animator anim;
    newPlayerController NPC;
    public Vector3 changePara;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        NPC = GetComponentInParent<newPlayerController>();
    }


    private void OnAnimatorIK(int layerIndex)
    {
        if (NPC.leftHandShield) {
            if (anim.GetBool("defence") == false)
            {
                Transform leftArm = anim.GetBoneTransform(HumanBodyBones.LeftLowerArm);
                leftArm.localEulerAngles += changePara;
                anim.SetBoneLocalRotation(HumanBodyBones.LeftLowerArm, Quaternion.Euler(leftArm.localEulerAngles));
            }
        }
    }
}
