using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace anotherMethodForControl {
    public class inputSingleHandle : MonoBehaviour
    {
        #region Attribute Area
        public keyProjection inputDevice;
        private float vertiSignle;
        private float HoriSingle;
        private float velocityVertical;
        private float velocityHorizon;
        public bool fall;
        public bool isRoll;
        public float vertiValue = 0;
        public float horiValue = 0;
        public Vector3 targetVector;
        public float targetMagtitue;
        public Transform detectPosition;
        public float detectRadius;
        public LayerMask checkLayer;

        #endregion

        #region Trigger Signal
        public bool jump;
        public bool inputDisable = false;
        public bool isRun;
        private bool lastJump;
        #endregion

        private void Start()
        {
        }

        private void Update()
        {
            vertiSignle = (Input.GetKey(inputDevice.UP) ? 1.0f : 0f) - (Input.GetKey(inputDevice.DOWN) ? 1f : 0);
            HoriSingle = (Input.GetKey(inputDevice.RIGHT) ? 1.0f : 0f) - (Input.GetKey(inputDevice.LEFT) ? 1f : 0);

            vertiValue = Mathf.SmoothDamp(vertiValue, vertiSignle, ref velocityVertical, 0.1f);
            horiValue = Mathf.SmoothDamp(horiValue, HoriSingle, ref velocityHorizon, 0.1f);


            if (inputDisable == true) {
                vertiValue = 0;
                horiValue = 0;
            }

            Vector2 tmpVec = SquareToCircle(new Vector2(horiValue, vertiValue));

            targetMagtitue = Mathf.Sqrt(Mathf.Pow(tmpVec.y, 2) + Mathf.Pow(tmpVec.x, 2));
            targetVector = tmpVec.x * transform.right + tmpVec.y * transform.forward;

            isRun = Input.GetKey(inputDevice.RUN);


            bool tmpJump = Input.GetKey(inputDevice.JUMP);
            jump = tmpJump;

            if (tmpJump != lastJump && tmpJump == true)
            {
                jump = true;
            }
            else
            {
                jump = false;
            }
            lastJump = tmpJump;

        }

        public Vector2 SquareToCircle(Vector2 _input) {
            Vector2 output = Vector2.zero;

            output.x = _input.x * Mathf.Sqrt(1 - Mathf.Pow(_input.y, 2) / 2f);
            output.y = _input.y * Mathf.Sqrt(1 - Mathf.Pow(_input.x, 2) / 2f);

            return output;
        }



        private void rollFinish() {
            Debug.Log("get");
            isRoll = false;
        }
    }
}
