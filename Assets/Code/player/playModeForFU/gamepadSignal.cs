using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gamepadSignal : IUserInput
{
    public gamepadProjection inputDevice;
    // Update is called once per frame
    void Update()
    {
        #region camera control
        camerVertical = (Input.GetAxis(inputDevice.cameraVerti));
        cameraHorizontal = (Input.GetAxis(inputDevice.cameraHorizon));
        #endregion

        #region move control
        vertiSignle = (Input.GetAxis(inputDevice.verticalMove));
        HoriSingle = (Input.GetAxis(inputDevice.horizontalMove));
        vertiValue = Mathf.SmoothDamp(vertiValue, vertiSignle, ref velocityVertical, 0.1f);
        horiValue = Mathf.SmoothDamp(horiValue, HoriSingle, ref velocityHorizon, 0.1f);
        #endregion

        if (inputDisable == true)
        {
            vertiValue = 0;
            horiValue = 0;
        }

        #region move projection
        Vector2 tmpVec = SquareToCircle(new Vector2(horiValue, vertiValue));
        targetMagtitue = Mathf.Sqrt(Mathf.Pow(tmpVec.y, 2) + Mathf.Pow(tmpVec.x, 2));
        targetVector = tmpVec.x * transform.right + tmpVec.y * transform.forward;
        #endregion

        isRun = Input.GetButton(inputDevice.run);

        #region jump control
        bool tmpJump = Input.GetButton(inputDevice.jump);
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
        bool tmpAttack = Input.GetButton(inputDevice.attack);
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

}
