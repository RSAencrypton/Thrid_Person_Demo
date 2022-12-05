using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace anotherMethodForControl {
    public class inputSingleHandle : IUserInput
    {
        public keyProjection inputDevice;
        public float mouseSenstivityX;
        public float mouseSenstivityY;
        public buttonFunction btFuncAttack = new buttonFunction();
        public buttonFunction btFuncRoll = new buttonFunction();
        public buttonFunction btFuncRun = new buttonFunction();
        public buttonFunction btFuncDefence = new buttonFunction();
        public buttonFunction btFuncLockOn = new buttonFunction();


        private void Update()
        {
            #region listen function
            btFuncAttack.onUpdate(Input.GetKey(inputDevice.ATTACK));
            btFuncRoll.onUpdate(Input.GetKey(inputDevice.ROLL));
            btFuncRun.onUpdate(Input.GetKey(inputDevice.RUN));
            btFuncDefence.onUpdate(Input.GetKey(inputDevice.defence));
            btFuncLockOn.onUpdate(Input.GetKey(inputDevice.LOCKON));
            #endregion

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

            #region signal control
            isRun = (btFuncRun.isPress && !btFuncRun.isDelay) || btFuncRun.isExtend;
            isRoll = btFuncRoll.isPress;
            defence = btFuncDefence.isPress;
            jump = btFuncRun.onPress && btFuncRun.isExtend;
            attack = btFuncAttack.onPress;
            isLockOn = btFuncLockOn.onPress;
            #endregion


        }

    }
}
