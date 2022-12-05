using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonFunction
{
    public bool isPress = false;
    public bool onPress = false;
    public bool onRelease = false;
    public bool isExtend = false;
    public bool isDelay = false;

    public float extendingTime = 0.15f;
    public float delayDuration = 0.15f;

    private bool curState = false;
    private bool lastState = false;

    private timer myTimer = new timer();
    private timer delayTimer = new timer();

    public void onUpdate(bool input) {

        
        myTimer.onUpdate();
        delayTimer.onUpdate();

        curState = input;
        isPress = curState;

        onPress = false;
        onRelease = false;
        isDelay = false;

        if (curState != lastState) {
            if (curState == true)
            {
                onPress = true;
                initTimer(delayTimer, delayDuration);
            }
            else {
                onRelease = true;
                initTimer(myTimer, extendingTime);
            }
        }

        lastState = curState;
        isExtend = myTimer._state == timer.STATE.RUN ? true : false;
        isDelay = delayTimer._state == timer.STATE.RUN ? true : false;
    }

    private void initTimer(timer _timer, float _duration) {
        _timer.duration = _duration;
        _timer.startTimer();
    }
}
