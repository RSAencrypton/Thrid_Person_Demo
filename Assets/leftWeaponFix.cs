using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leftWeaponFix : MonoBehaviour
{
    Animator anim;
    public Vector3 changePara;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }


    private void OnAnimatorIK(int layerIndex)
    {
        if (anim.GetBool("defence") == false) {
            Transform leftArm = anim.GetBoneTransform(HumanBodyBones.LeftLowerArm);
            leftArm.localEulerAngles += changePara;
            anim.SetBoneLocalRotation(HumanBodyBones.LeftLowerArm, Quaternion.Euler(leftArm.localEulerAngles));
        }
    }
}
