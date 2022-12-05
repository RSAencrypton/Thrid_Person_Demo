using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace anotherMethodForControl {
    public class newPlayerController : MonoBehaviour
    {
        #region characater value
        public cameraController camCon;
        public GameObject player;
        public float runSpeed;
        public float runIncremental = 2f;
        public float jumpForce;
        public float rollForce;
        public float backJumpForce;
        private bool canAttack;
        private bool trackDir = false;
        private float targetLerp;
        private float defenceLerp;
        public PhysicMaterial frictionOne;
        public PhysicMaterial frictionZero;
        private Vector3 deltaPos;
        #endregion

        #region characater component
        Animator anim;
        public IUserInput inputSingnal;
        Rigidbody rb;
        CapsuleCollider capcol;
        Vector3 planarVec;
        Vector3 thrusVec;
        private bool lockPlanar = false;
        #endregion

        private void Awake()
        {
            anim = player.GetComponent<Animator>();
            rb = GetComponent<Rigidbody>();
            capcol = GetComponent<CapsuleCollider>();

            IUserInput[] inputList = GetComponents<IUserInput>();
            foreach (var item in inputList)
            {
                if (item.enabled == true)
                {
                    inputSingnal = item;
                    break;
                }
            }
        }

        // Start is called before the first frame update
        private void Update()
        {

            if (inputSingnal.isLockOn) {
                camCon.unlockOn();
            }

            if (camCon.isLock == false)
            {
                anim.SetFloat("speed", inputSingnal.targetMagtitue * (inputSingnal.isRun ? runIncremental : 1f));
                anim.SetFloat("right", 0);
            }
            else {
                anim.SetFloat("speed", inputSingnal.targetVector.z * (inputSingnal.isRun ? runIncremental : 1f));
                anim.SetFloat("right", inputSingnal.targetVector.x * (inputSingnal.isRun ? runIncremental : 1f));
            }

            anim.SetBool("defence", inputSingnal.defence);
            if (inputSingnal.jump)
            {
                anim.SetTrigger("jump");
                canAttack = false;
            }

            if (inputSingnal.attack && isInThisAnimation("BasicMove") && canAttack)
                anim.SetTrigger("attack");

            if (inputSingnal.isRoll || rb.velocity.magnitude >= 7f) {
                anim.SetTrigger("roll");
                canAttack = false;
            }


            if (camCon.isLock == false)
            {
                if (inputSingnal.targetMagtitue > 0.1f)
                {
                    player.transform.forward = Vector3.Slerp(player.transform.forward, inputSingnal.targetVector, 0.3f);
                }

                if (lockPlanar == false)
                    planarVec = inputSingnal.targetMagtitue * player.transform.forward * runSpeed *
                        (inputSingnal.isRun ? runIncremental : 1f);
            }
            else {
                if (trackDir == false)
                {
                    player.transform.forward = transform.forward;
                }
                else {
                    player.transform.forward = planarVec.normalized;
                }
                if (lockPlanar == false)
                    planarVec = inputSingnal.targetVector * runSpeed *
                        (inputSingnal.isRun ? runIncremental : 1f);
            }

        }

        private void FixedUpdate()
        {
            rb.position += deltaPos;
            rb.velocity = new Vector3(planarVec.x, rb.velocity.y, planarVec.z);
            deltaPos = Vector3.zero;

        }

        public bool isInThisAnimation(string animName, string layerName = "Base Layer") {
            return anim.GetCurrentAnimatorStateInfo(anim.GetLayerIndex(layerName)).IsName(animName);
        }





        //Message Receive Implement
        //
        //
        #region Message Event
        public void leaveTheGround() {
            capcol.material = frictionZero;
            inputSingnal.inputDisable = true;
            lockPlanar = true;
        }

        public void onJumpEnter() {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            trackDir = true;
        }

        public void onGround() {
            anim.SetBool("isGorund", true);
            canAttack = true;
        }

        public void notOnGround() {
            anim.SetBool("isGorund", false);
            canAttack = false;
        }

        public void onGroundEnter() {
            capcol.material = frictionOne;
            inputSingnal.inputDisable = false;
            lockPlanar = false;
            trackDir = false;
        }

        public void onRollEnter() {
            rb.AddForce(Vector3.forward * rollForce, ForceMode.Impulse);
            trackDir = true;
        }

        public void onBackJumpEnter() {
            rb.AddForce(Vector3.up * 2f, ForceMode.Impulse);
        }

        public void onBackJumpUpdate() {
            rb.AddForce(Vector3.back * anim.GetFloat("jabVelocity"), ForceMode.Impulse);
        }

        public void onOtherMotionIdleEnter() {
            inputSingnal.inputDisable = false;
            //lockPlanar = false;
            targetLerp = 0;
        }

        public void onOtherMotionIdleUpdate() {
            int curIndex = anim.GetLayerIndex("attack layer");
            float curWeight = anim.GetLayerWeight(curIndex);
            curWeight = Mathf.Lerp(curWeight, targetLerp, 0.1f);
            anim.SetLayerWeight(curIndex, curWeight);
        }

        public void onOtherMotionEnter() {
            inputSingnal.inputDisable = true;
            //lockPlanar = true;
            targetLerp = 1f;
        }

        public void onOtherMotionUpdate() {
            rb.AddForce(player.transform.forward * anim.GetFloat("attackVelocity"), ForceMode.Impulse);
            int curIndex = anim.GetLayerIndex("attack layer");
            float curWeight = anim.GetLayerWeight(curIndex);
            curWeight = Mathf.Lerp(curWeight, targetLerp, 0.1f);
            anim.SetLayerWeight(curIndex, curWeight);
        }

        public void onRootMotionUpdate(object _deltaPos) {
            if (isInThisAnimation("final_light_slash", "attack layer"))
                deltaPos += (Vector3)_deltaPos;
        }

        public void onDefenceMotionIdleEnter()
        {
            //inputSingnal.inputDisable = false;
            //lockPlanar = false;
            defenceLerp = 0;
        }

        public void onDefenceMotionIdleUpdate()
        {
            int curIndex = anim.GetLayerIndex("defence layer");
            float curWeight = anim.GetLayerWeight(curIndex);
            curWeight = Mathf.Lerp(curWeight, defenceLerp, 0.1f);
            anim.SetLayerWeight(curIndex, curWeight);
        }

        public void onDefenceMotionEnter()
        {
            //inputSingnal.inputDisable = true;
            //lockPlanar = true;
            defenceLerp = 1f;
        }

        public void onDefenceMotionUpdate()
        {
            int curIndex = anim.GetLayerIndex("defence layer");
            float curWeight = anim.GetLayerWeight(curIndex);
            curWeight = Mathf.Lerp(curWeight, defenceLerp, 0.1f);
            anim.SetLayerWeight(curIndex, curWeight);
        }


        #endregion
    }
}
