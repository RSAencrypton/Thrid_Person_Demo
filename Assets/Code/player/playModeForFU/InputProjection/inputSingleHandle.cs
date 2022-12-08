using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace anotherMethodForControl {
    public class inputSingleHandle : IUserInput
    {
        public keyProjection inputDevice;
        public float mouseSenstivityX;
        public float mouseSenstivityY;
        public buttonFunction btFuncLeftClick = new buttonFunction();
        public buttonFunction btFuncRoll = new buttonFunction();
        public buttonFunction btFuncRun = new buttonFunction();
        public buttonFunction btFuncRightClick = new buttonFunction();
        public buttonFunction btFuncLockOn = new buttonFunction();


        private void Update()
        {
            #region listen function
            //btFuncAttack.onUpdate(Input.GetKey(inputDevice.ATTACK));
            btFuncRoll.onUpdate(Input.GetKey(inputDevice.ROLL));
            btFuncRun.onUpdate(Input.GetKey(inputDevice.RUN));
            btFuncRightClick.onUpdate(Input.GetKey(inputDevice.RIGHTCLICK));
            btFuncLeftClick.onUpdate(Input.GetKey(inputDevice.LEFTCLICK));
            btFuncLockOn.onUpdate(Input.GetKey(inputDevice.LOCKON));
            #endregion

            #region camera control

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
            calTargetmagAndTargetvec(tmpVec.x, tmpVec.y);
            #endregion

            #region signal control
            isRun = (btFuncRun.isPress && !btFuncRun.isDelay) || btFuncRun.isExtend;
            isRoll = btFuncRoll.isPress;
            jump = btFuncRun.onPress && btFuncRun.isExtend;
            isLeftClick = btFuncLeftClick.onPress;
            isRightClick = btFuncRightClick.onPress;
            isLockOn = btFuncLockOn.onPress;
            #endregion


        }

    }
}
