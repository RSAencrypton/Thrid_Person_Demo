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
        public float vertiValue = 0;
        public float horiValue = 0;
        public float camerVertical = 0;
        public float cameraHorizontal = 0;
        public Vector3 targetVector;
        public float targetMagtitue;

        #endregion

        #region Trigger Signal
        public bool inputDisable = false;
        public bool isRun;
        private bool lastJump;
        public bool jump;
        private bool lastAttack;
        public bool attack;
        #endregion


        private void Update()
        {

            #region camera control
            camerVertical = (Input.GetKey(inputDevice.CAMERAUP) ? 1.0f : 0f) - (Input.GetKey(inputDevice.CAMERADOWN) ? 1f : 0);
            cameraHorizontal = (Input.GetKey(inputDevice.CAMERARIGHT) ? 1.0f : 0f) - (Input.GetKey(inputDevice.CAMERALEFT) ? 1f : 0);
            #endregion

            #region move control
            vertiSignle = (Input.GetKey(inputDevice.UP) ? 1.0f : 0f) - (Input.GetKey(inputDevice.DOWN) ? 1f : 0);
            HoriSingle = (Input.GetKey(inputDevice.RIGHT) ? 1.0f : 0f) - (Input.GetKey(inputDevice.LEFT) ? 1f : 0);
            vertiValue = Mathf.SmoothDamp(vertiValue, vertiSignle, ref velocityVertical, 0.1f);
            horiValue = Mathf.SmoothDamp(horiValue, HoriSingle, ref velocityHorizon, 0.1f);
            #endregion


            if (inputDisable == true) {
                vertiValue = 0;
                horiValue = 0;
            }

            #region move projection
            Vector2 tmpVec = SquareToCircle(new Vector2(horiValue, vertiValue));
            targetMagtitue = Mathf.Sqrt(Mathf.Pow(tmpVec.y, 2) + Mathf.Pow(tmpVec.x, 2));
            targetVector = tmpVec.x * transform.right + tmpVec.y * transform.forward;
            #endregion

            isRun = Input.GetKey(inputDevice.RUN);


            #region jump control
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
            #endregion

            #region attack control
            bool tmpAttack = Input.GetKey(inputDevice.ATTACK);
            attack = tmpAttack;
            if (tmpAttack != lastAttack && tmpAttack == true)
            {
                attack = true;
            }
            else
            {
                attack = false;
            }
            lastAttack = tmpAttack;
            #endregion



        }

        public Vector2 SquareToCircle(Vector2 _input) {
            Vector2 output = Vector2.zero;

            output.x = _input.x * Mathf.Sqrt(1 - Mathf.Pow(_input.y, 2) / 2f);
            output.y = _input.y * Mathf.Sqrt(1 - Mathf.Pow(_input.x, 2) / 2f);

            return output;
        }

    }
}
