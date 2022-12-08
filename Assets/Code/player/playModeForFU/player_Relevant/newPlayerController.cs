using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Connection;
using FishNet.Object;

namespace anotherMethodForControl {
    public class newPlayerController : NetworkBehaviour
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
        private float defenceLerp;
        public PhysicMaterial frictionOne;
        public PhysicMaterial frictionZero;
        private Vector3 deltaPos;
        public bool isAI;
        public GameObject camera;
        #endregion

        #region character component
        Animator anim;
        public IUserInput inputSingnal;
        Rigidbody rb;
        CapsuleCollider capcol;
        Vector3 planarVec;
        Vector3 thrusVec;
        private bool lockPlanar = false;
        public bool leftHandShield = true;
        #endregion

        public override void OnStartClient()
        {
            base.OnStartClient();

            if (base.IsOwner) {
                camera.SetActive(true);
            }
            else if (!base.IsOwner)
            {
                gameObject.GetComponent<newPlayerController>().enabled = false;
            }
        }

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

            if ( isAI == false&&camCon.isLock == false)
            {
                anim.SetFloat("speed", inputSingnal.targetMagtitue * (inputSingnal.isRun ? runIncremental : 1f));
                anim.SetFloat("right", 0);
            }
            else {
                anim.SetFloat("speed", inputSingnal.targetVector.z * (inputSingnal.isRun ? runIncremental : 1f));
                anim.SetFloat("right", inputSingnal.targetVector.x * (inputSingnal.isRun ? runIncremental : 1f));
            }

            //anim.SetBool("defence", inputSingnal.defence);
            if (inputSingnal.jump)
            {
                anim.SetTrigger("jump");
                canAttack = false;
            }

            if ((inputSingnal.isRightClick) &&(isInThisAnimation("BasicMove") || isInThisTage("attack")) && canAttack) {

                if (inputSingnal.isLeftClick) {
                    anim.SetBool("isMirror", true);
                } else if (inputSingnal.isRightClick) {
                    anim.SetBool("isMirror", false);
                }
                anim.SetTrigger("attack");
            }

            if (inputSingnal.isLeftClick)
                anim.SetTrigger("isBlock");

            if (inputSingnal.isRoll || rb.velocity.magnitude >= 7f) {
                anim.SetTrigger("roll");
                canAttack = false;
            }


            if (isAI == false && camCon.isLock == false)
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


        public bool isInThisTage(string animTage, string layerName = "Base Layer")
        {
            return anim.GetCurrentAnimatorStateInfo(anim.GetLayerIndex(layerName)).IsTag(animTage);
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



        public void onOtherMotionEnter() {
            inputSingnal.inputDisable = true;
            lockPlanar = false;
        }

        public void onOtherMotionUpdate() {
            rb.AddForce(player.transform.forward * anim.GetFloat("attackVelocity"), ForceMode.Impulse);

        }

        public void onRootMotionUpdate(object _deltaPos) {
            if (isInThisAnimation("final_light_slash"))
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

        public void animTriggerSetting(string triggerName) {
            anim.SetTrigger(triggerName);
        }


        #endregion
    }
}
