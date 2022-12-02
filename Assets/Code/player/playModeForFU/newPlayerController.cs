using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace anotherMethodForControl {
    public class newPlayerController : MonoBehaviour
    {
        #region characater value
        public GameObject player;
        public float runSpeed;
        public float runIncremental = 2f;
        public float jumpForce;
        public float rollForce;
        public float backJumpForce;
        #endregion

        #region characater component
        Animator anim;
        inputSingleHandle inputSingnal;
        Rigidbody rb;
        Vector3 planarVec;
        Vector3 thrusVec;
        private bool lockPlanar = false;
        #endregion

        private void Awake()
        {
            anim = player.GetComponent<Animator>();
            inputSingnal = GetComponent<inputSingleHandle>();
            rb = GetComponent<Rigidbody>();
        }
        // Start is called before the first frame update
        private void Update()
        {
            anim.SetFloat("speed", inputSingnal.targetMagtitue * (inputSingnal.isRun ? runIncremental : 1f));
            if (inputSingnal.jump)
                anim.SetTrigger("jump");

            if (rb.velocity.magnitude > 1.0f)
                anim.SetTrigger("roll");

            if (inputSingnal.targetMagtitue > 0.1f) {
                player.transform.forward = Vector3.Slerp(player.transform.forward, inputSingnal.targetVector, 0.3f);
            }

            if (lockPlanar == false)
                planarVec = inputSingnal.targetMagtitue * player.transform.forward * runSpeed *
                    (inputSingnal.isRun ? runIncremental : 1f);

        }

        private void FixedUpdate()
        {
            rb.velocity = new Vector3(planarVec.x, rb.velocity.y, planarVec.z);


        }







        //Message Receive Implement
        //
        //

        public void leaveTheGround() {
            inputSingnal.inputDisable = true;
            lockPlanar = true;
        }

        public void onJumpEnter() {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        public void onGround() {
            anim.SetBool("isGorund", true);
        }

        public void notOnGround() {
            anim.SetBool("isGorund", false);
        }

        public void onGroundEnter() {
            inputSingnal.inputDisable = false;
            lockPlanar = false;
        }

        public void onRollEnter() {
            rb.AddForce(Vector3.forward * rollForce, ForceMode.Impulse);
        }

        public void onBackJumpEnter() {
            rb.AddForce(Vector3.back * backJumpForce, ForceMode.Impulse);
        }
    }
}
