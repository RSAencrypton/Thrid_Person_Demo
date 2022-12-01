using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace anotherMethodForControl {
    public class newPlayerController : MonoBehaviour
    {
        public GameObject player;
        public float runSpeed;

        Animator anim;
        inputSingleHandle inputSingnal;
        Rigidbody rb;
        Vector3 movTarget;

        private void Awake()
        {
            anim = player.GetComponent<Animator>();
            inputSingnal = GetComponent<inputSingleHandle>();
            rb = GetComponent<Rigidbody>();
        }
        // Start is called before the first frame update
        private void Update()
        {
            anim.SetFloat("speed", inputSingnal.targetMagtitue);
            anim.SetBool("falling", !inputSingnal.fall);
            if (inputSingnal.targetMagtitue > 0.1f) {
                player.transform.forward = Vector3.Slerp(player.transform.forward, inputSingnal.targetVector, 0.3f);
            }
            movTarget = inputSingnal.targetMagtitue * player.transform.forward * runSpeed;

        }

        private void FixedUpdate()
        {
            rb.position += new Vector3(movTarget.x, rb.velocity.y, movTarget.z);


        }

    }
}
