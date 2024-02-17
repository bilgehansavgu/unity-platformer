using Core.StateMachine;

namespace Core.CharacterController
{
    public class PlayerState_Landing : PlayerState_Base
    {
        const string landingClip = "landing_animation";
        public PlayerState_Landing(PlayerController parent) : base(parent)
        {
        }
        public override PlayerController.StateID GetID() => PlayerController.StateID.Landing;

        public override void Enter(StateMachine<PlayerController.StateID> machine)
        {
            PlayClip(landingClip);
        }

        public override void Exit(StateMachine<PlayerController.StateID> machine)
        {

        }


        protected override void Act(StateMachine<PlayerController.StateID> machine)
        {
        }
        protected override void Decide(StateMachine<PlayerController.StateID> machine)
        {
        }

        public override void InvokeState(StateMachine<PlayerController.StateID> machine)
        {
            if (parent.IsMoving)
                machine.ChangeStateImmediate(PlayerController.StateID.Move);
            else
                machine.ChangeStateImmediate(PlayerController.StateID.Idle);
        }
    }
}
