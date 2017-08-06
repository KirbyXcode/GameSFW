using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AI.FSM
{
    /// <summary>
    /// 待机状态
    /// </summary>
	public class IdleState : FSMState 
	{

        public override void Init()
        {
            stateId = FSMStateID.Idle;
        }

        public override void EnterState(BaseFSM fsm)
        {
        }

        public override void ExitState(BaseFSM fsm)
        {
        }

        public override void Action(BaseFSM fsm)
        {
            //播待机动画
            fsm.PlayAnimation(fsm.animParams.Idle);
        }

    }
}
