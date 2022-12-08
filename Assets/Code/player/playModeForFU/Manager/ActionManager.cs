using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using anotherMethodForControl;

public class ActionManager : MonoBehaviour
{
    private battleManager bm;
    public newPlayerController NPC;
    public WeaponManager wm;
    public int HP = 0;

    private void Awake()
    {
        GameObject sensor = transform.Find("detectPoint").gameObject;
        NPC = GetComponent<newPlayerController>();
        bm = GetComponent<battleManager>();

        if (bm == null) {
            bm = sensor.AddComponent<battleManager>();
        }

        bm.am = this;

        GameObject model = NPC.player;
        wm = model.GetComponent<WeaponManager>();

        if (wm == null)
        {
            wm = model.AddComponent<WeaponManager>();
        }

        wm.am = this;
        HP = 15;
    }

    public void doDamage() {

        if (NPC.isInThisAnimation("blocked")
            || NPC.isInThisAnimation("roll")||
            NPC.isInThisAnimation("idleJump")) {
            return;
        }
        HP -= 5;
        if (HP <= 0)
        {
            NPC.animTriggerSetting("isDIE");

            if (NPC.camCon.isLock == true) {
                NPC.camCon.unlockOn();
            }

            NPC.camCon.enabled = false;
        }
        else {
            NPC.animTriggerSetting("isHit");
        }
    }
}
