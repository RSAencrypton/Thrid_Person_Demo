using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IUserInput : MonoBehaviour
{
    #region Attribute Area
    protected float vertiSignle;
    protected float HoriSingle;
    protected float velocityVertical;
    protected float velocityHorizon;
    public bool fall;
    public bool defence;
    public bool isRoll;
    public bool isLockOn;
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
    protected bool lastJump;
    public bool jump;
    public bool isLeftClick;
    public bool isRightClick;
    #endregion

    protected Vector2 SquareToCircle(Vector2 _input)
    {
        Vector2 output = Vector2.zero;

        output.x = _input.x * Mathf.Sqrt(1 - Mathf.Pow(_input.y, 2) / 2f);
        output.y = _input.y * Mathf.Sqrt(1 - Mathf.Pow(_input.x, 2) / 2f);

        return output;
    }

    protected void calTargetmagAndTargetvec(float vecX, float vecY) {
        targetMagtitue = Mathf.Sqrt(Mathf.Pow(vecY, 2) + Mathf.Pow(vecX, 2));
        targetVector = vecX * transform.right + vecY * transform.forward;

    }
}
