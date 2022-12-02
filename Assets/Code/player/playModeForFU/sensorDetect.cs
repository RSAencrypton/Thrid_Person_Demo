using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sensorDetect : MonoBehaviour
{

    public Transform detectPosition;
    public float detectRadius;
    public LayerMask checkLayer;
    Collider[] colliderList;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        colliderList = Physics.OverlapSphere(detectPosition.position, detectRadius, checkLayer);

        if (colliderList.Length != 0)
        {
            SendMessageUpwards("onGround");
        }
        else {
            SendMessageUpwards("notOnGround");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(detectPosition.position, detectRadius);
    }
}
