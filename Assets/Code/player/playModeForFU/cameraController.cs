using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace anotherMethodForControl {
    public class cameraController : MonoBehaviour
    {
        private IUserInput camerasignal;
        public Image lockOnUI;
        public bool isLock;
        public float cameraHorzonSpeed = 20f;
        public float cameraVertiSpeed = 80f;
        public Transform player;
        private float tmpEular = 20f;
        private GameObject camera;
        public lockTarget _lockTarget;
        public bool isAI;
        GameObject playerHandle;
        GameObject cameraHandle;



        private void Awake()
        {
            cameraHandle = transform.parent.gameObject;
            _lockTarget = new lockTarget();
            playerHandle = cameraHandle.transform.parent.gameObject;
            camera = Camera.main.gameObject;
            camerasignal = playerHandle.GetComponent<newPlayerController>().inputSingnal;
            if (!isAI)
            {
                lockOnUI.enabled = false;
                isLock = false;
                Cursor.lockState = CursorLockMode.Locked;
            }

            Cursor.visible = false;
        }

        private void Update()
        {

            if (_lockTarget.obj == null)
            {
                Vector3 playerEularAngle = player.eulerAngles;
                playerHandle.transform.Rotate(Vector3.up, camerasignal.cameraHorizontal * cameraHorzonSpeed * Time.deltaTime);
                tmpEular -= -camerasignal.camerVertical * cameraVertiSpeed * Time.deltaTime;
                tmpEular = Mathf.Clamp(tmpEular, -40, 30);
                cameraHandle.transform.localEulerAngles = new Vector3(
                    tmpEular, 0, 0);
                player.eulerAngles = playerEularAngle;
            }
            else {
                Vector3 nextForward = _lockTarget.obj.transform.position - player.transform.position;
                nextForward.y = 0;
                playerHandle.transform.forward = nextForward;

                if (!isAI) {
                    lockOnUI.rectTransform.position = Camera.main.WorldToScreenPoint(_lockTarget.obj.transform.position +
    new Vector3(0, _lockTarget.halfHeight, 0));
                    cameraHandle.transform.LookAt(_lockTarget.obj.transform);
                }

                if (Vector3.Distance(_lockTarget.obj.transform.position, player.transform.position) >= 10.5f) {
                    _lockTarget.unlock();
                    lockOnUI.enabled = false;
                    isLock = false;
                }
            }

        }

        private void LateUpdate()
        {
            if (!isAI) {
                camera.transform.position = transform.position;
                camera.transform.eulerAngles = transform.eulerAngles;
            }
        }

        public void unlockOn() {
            Vector3 playerView = player.transform.position + Vector3.up;
            Vector3 viewCenter = playerView + player.transform.forward * 5f;
            Collider[] colList = Physics.OverlapBox(viewCenter, new Vector3(0.5f, 0.5f, 5f),
                player.transform.rotation, LayerMask.GetMask("enemy"));

            if (colList.Length == 0)
            {
                _lockTarget.obj = null;
                lockOnUI.enabled = false;
                isLock = false;
            }
            else {
                foreach (var item in colList)
                {
                    if (_lockTarget.obj == item.gameObject) {
                        _lockTarget.obj = null;
                        lockOnUI.enabled = false;
                        isLock = false;
                        break;
                    }
                    
                    _lockTarget = new lockTarget(item.gameObject, item.bounds.extents.y);
                    lockOnUI.enabled = true;
                    isLock = true;
                    break;
                }
            }
        }
    }

    public class lockTarget
    {
        public GameObject obj;
        public float halfHeight;

        public lockTarget() {
            obj = null;
            halfHeight = 0;
        }

        public lockTarget(GameObject _obj, float _halfHeight) {
            obj = _obj;
            halfHeight = _halfHeight;
        }

        public void unlock() {
            obj = null;
            halfHeight = 0;
        }
    }


}
