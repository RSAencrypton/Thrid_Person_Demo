using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class battleManager : MonoBehaviour
{
    private CapsuleCollider capCol;
    public ActionManager am;

    private void Start()
    {
        capCol = GetComponent<CapsuleCollider>();
        capCol.center = new Vector3(0, 0.857689f, 0);
        capCol.height = 1.681154f;
        capCol.radius = 0.25f;
        capCol.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (am.HP > 0) {
            am.doDamage();
        }
    }
}
