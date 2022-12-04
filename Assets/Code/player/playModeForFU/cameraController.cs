using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace anotherMethodForControl {
    public class cameraController : MonoBehaviour
    {
        private gamepadSignal camerasignal;
        public float cameraHorzonSpeed = 20f;
        public float cameraVertiSpeed = 80f;
        public Transform player;
        private float tmpEular = 20f;
        private GameObject camera;
        GameObject playerHandle;
        GameObject cameraHandle;



        private void Awake()
        {
            cameraHandle = transform.parent.gameObject;
            playerHandle = cameraHandle.transform.parent.gameObject;
            camera = Camera.main.gameObject;
            camerasignal = playerHandle.GetComponent<newPlayerController>().inputSingnal;
        }

        private void Update()
        {


            Vector3 playerEularAngle = player.eulerAngles;



            playerHandle.transform.Rotate(Vector3.up,camerasignal.cameraHorizontal * cameraHorzonSpeed * Time.deltaTime);
            tmpEular -= -camerasignal.camerVertical * cameraVertiSpeed * Time.deltaTime;
            tmpEular = Mathf.Clamp(tmpEular, -40, 30);
            cameraHandle.transform.localEulerAngles = new Vector3(
                tmpEular, 0, 0);

            player.eulerAngles = playerEularAngle;

        }

        private void LateUpdate()
        {
            camera.transform.position = transform.position;
            camera.transform.eulerAngles = transform.eulerAngles;
        }
    }
}
