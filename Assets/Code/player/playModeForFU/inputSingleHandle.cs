using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace anotherMethodForControl {
    public class inputSingleHandle : IUserInput
    {
        public keyProjection inputDevice;
        public float mouseSenstivityX;
        public float mouseSenstivityY;


        private void Update()
        {

            #region camera control
            //camerVertical = (Input.GetKey(inputDevice.CAMERAUP) ? 1.0f : 0f) - (Input.GetKey(inputDevice.CAMERADOWN) ? 1f : 0);
            //cameraHorizontal = (Input.GetKey(inputDevice.CAMERARIGHT) ? 1.0f : 0f) - (Input.GetKey(inputDevice.CAMERALEFT) ? 1f : 0);

            camerVertical = -Input.GetAxis("Mouse Y") * mouseSenstivityY;
            cameraHorizontal = Input.GetAxis("Mouse X") * mouseSenstivityX;
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

            defence = Input.GetKey(inputDevice.defence);

        }

    }
}
