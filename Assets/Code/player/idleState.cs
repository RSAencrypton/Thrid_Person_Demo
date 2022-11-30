using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace player {

    public class idleState : FSM
    {
        public override void Action()
        {
            
        }

        public override void enterState(playerController obj)
        {
            _obj = obj;
            _obj._anim.Play("idle");
        }

        public override void exitState()
        {
            if (_obj._moveInput != Vector2.zero)
                _obj.switchState(_obj._move);
        }
    }
}
