using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace player {
    public class moveState : FSM
    {

        public override void Action()
        {
/*
            _obj.moveMethod();
            _obj.playerRotate();*/
        }

        public override void enterState(playerController obj)
        {
            _obj = obj;
            _obj._anim.Play("running");
        }

        public override void exitState()
        {
            if (_obj._moveInput == Vector2.zero)
                _obj.switchState(_obj._idle);
        }

    }
}

