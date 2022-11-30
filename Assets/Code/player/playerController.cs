using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace player {
    public class playerController : MonoBehaviour
    {

        Rigidbody rb;
        Transform cameraObj;
        GameObject normalCamera;
        Transform playerTransform;
        Animator anim;
        [Header("state list")]
        FSM curState = null;
        idleState idle = new idleState();
        moveState move = new moveState();

        #region Attribute_Indexer
        public Rigidbody _rb { get { return rb; } }
        public Transform _playerTransform { get { return playerTransform; } }
        public Vector2 _moveInput { get { return moveInput; } }
        public Transform _camerObj { get { return cameraObj; } }
        public moveState _move { get { return move; } }
        public idleState _idle { get { return idle; } }
        public Animator _anim { get { return anim; } }
        #endregion


        [Header("player attribute")]
        Vector2 moveInput;
        Vector2 cameraInput;
        Vector3 normalVector;
        Vector3 targetPos;
        [SerializeField]private float horiMove;
        [SerializeField]private float vertiMove;
        [SerializeField] private float moveAmount;
        [SerializeField] private float mouseX;
        [SerializeField] private float mouseY;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float rotatSpeed;
        [SerializeField] inputHandle input;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            playerTransform = this.transform;
            cameraObj = Camera.main.transform;
            anim = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            input.onMove += movement;

            input.onStopMove += stopMove;
        }

        private void Start()
        {
            input.enableGameplayInput();
        }

        private void OnDisable()
        {
            input.onMove -= movement;
            input.onStopMove -= stopMove;
        }

        private void FixedUpdate()
        {
            anim.SetFloat("speed", rb.velocity.magnitude);
            playerRotate();
            moveMethod();
        }

        private void Update()
        {

        }

        void movement(Vector2 MoveInput)
        {
            horiMove = MoveInput.x;
            vertiMove = MoveInput.y;
            moveAmount =  Mathf.Clamp01(Mathf.Abs(horiMove) + Mathf.Abs(vertiMove));
        }

        void stopMove() {
            horiMove = 0;
            vertiMove = 0;
        }

        public void switchState(FSM changeState) {
            curState = changeState;
            curState.enterState(this);
        }

        void playerRotate() {
            Vector3 targetDir = Vector3.zero;
            float moveOverride = moveAmount;

            targetDir = cameraObj.forward * vertiMove +
                cameraObj.right * horiMove;

            targetDir.Normalize();
            targetDir.y = 0;

            if (targetDir == Vector3.zero)
                targetDir = playerTransform.forward;

            Quaternion transRotate = Quaternion.LookRotation(targetDir);
            playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, transRotate, rotatSpeed * Time.time);
        }

        void moveMethod() {
            Vector3 movDir = cameraObj.forward * vertiMove + cameraObj.right * horiMove;
            movDir.Normalize();
            movDir *= moveSpeed;
            rb.velocity = Vector3.ProjectOnPlane(movDir, normalVector);
        }


    }

}

