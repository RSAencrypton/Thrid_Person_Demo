using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timer
{
    public enum STATE {
        IDLE,
        RUN,
        FINISHED
    }

    public STATE _state;
    public float duration = 1.0f;
    public float elapsedTime = 0f;

    public void onUpdate() {
        switch(_state) {
            case STATE.IDLE:
                break;
            case STATE.RUN:
                elapsedTime += Time.deltaTime;
                if (elapsedTime >= duration) {
                    _state = STATE.FINISHED;
                }
                break;
            case STATE.FINISHED:
                break;
            default:
                Debug.Log("Have Bug!!!");
                break;
        }
    }

    public void startTimer() {
        elapsedTime = 0;
        _state = STATE.RUN;
    }
}
