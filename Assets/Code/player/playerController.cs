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
        PlayerInput _playerInput;
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

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            playerTransform = this.transform;
            _playerInput = new PlayerInput();
            cameraObj = Camera.main.transform;
            switchState(idle);
        }

        private void OnEnable()
        {
            _playerInput.Enable();
/*            _playerInput.Gameplay.movement.performed +=
                _playerInput => moveInput = _playerInput.ReadValue<Vector2>();
            _playerInput.Gameplay.Camera.performed +=
                i => cameraInput = i.ReadValue<Vector2>();*/
        }

        private void OnDisable()
        {
            _playerInput.Disable();
        }

        private void FixedUpdate()
        {
            curState.Action();
        }

        private void Update()
        {
            curState.exitState();
            movement();
        }

        public void movement()
        {
            moveInput = _playerInput.Gameplay.movement.ReadValue<Vector2>();
            cameraInput = _playerInput.Gameplay.Camera.ReadValue<Vector2>();
            horiMove = moveInput.x;
            vertiMove = moveInput.y;
            mouseX = cameraInput.x;
            mouseY = cameraInput.y;

            moveAmount =  Mathf.Clamp01(Mathf.Abs(horiMove) + Mathf.Abs(vertiMove));
        }

        public void switchState(FSM changeState) {
            curState = changeState;
            curState.enterState(this);
        }

        public void playerRotate() {
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

        public void moveMethod() {
            Vector3 movDir = cameraObj.forward * vertiMove + cameraObj.right * horiMove;
            movDir.Normalize();

            movDir *= moveSpeed;
            rb.velocity = Vector3.ProjectOnPlane(movDir, normalVector);


        }


    }

}

