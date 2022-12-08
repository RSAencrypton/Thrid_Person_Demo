using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anotherMethodForControl;

public class WeaponManager : MonoBehaviour
{
    private Collider weapon;
    public ActionManager am;
    public GameObject leftHandWeapon;
    public GameObject rightHandWeapon;

    private void Start()
    {
        weapon = rightHandWeapon.GetComponentInChildren<Collider>();
        //Debug.Log(transform.FindExcuateObject("SM_Wep_Sword_Rapier_01"));

        leftHandWeapon = transform.FindExcuateObject("weapon2HandleLeft").gameObject;
        rightHandWeapon = transform.FindExcuateObject("weaponHandleRight").gameObject;


        weapon = rightHandWeapon.GetComponentInChildren<Collider>();

    }
    public void HitEnable() {
        weapon.enabled = true;
    }
    public void HitDisable() {
        weapon.enabled = false;
    }
}
