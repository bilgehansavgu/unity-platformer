using Core.StateMachine;

namespace Core.CharacterController
{
    public class PlayerState_AnticipateJump : PlayerState_Base
    {
        const string anticipateJumpClip = "AnticipateJump";
        public PlayerState_AnticipateJump(PlayerController parent) : base(parent) { }
        public override PlayerController.StateID GetID() => PlayerController.StateID.AnticipateJump;
        public override void Enter(StateMachine<PlayerController.StateID> machine)
        {
            PlayClip(anticipateJumpClip);
        }
        public override void Exit(StateMachine<PlayerController.StateID> machine)
        {
            throw new System.NotImplementedException();
        }
        protected override void Act(StateMachine<PlayerController.StateID> machine)
        {
            throw new System.NotImplementedException();
        }
        protected override void Decide(StateMachine<PlayerController.StateID> machine)
        {
            throw new System.NotImplementedException();
        }
    }
}
