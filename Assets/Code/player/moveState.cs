using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace player {
    public class moveState : FSM
    {

        public override void Action()
        {
            _obj.moveMethod();
            _obj.playerRotate();
        }

        public override void enterState(playerController obj)
        {
            _obj = obj;
        }

        public override void exitState()
        {
            
        }

    }
}

