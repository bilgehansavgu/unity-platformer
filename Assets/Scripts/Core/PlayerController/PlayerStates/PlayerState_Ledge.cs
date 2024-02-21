using UnityEngine;
using Core.StateMachine;

namespace Core.CharacterController
{
    public class PlayerState_Ledge : PlayerState_Base
    {
        const string ledgeClip = "LedgeClimb";

        public PlayerState_Ledge(PlayerController parent) : base(parent)
        {
        }

        public override PlayerController.StateID GetID() => PlayerController.StateID.Ledge;

        public override void Enter(StateMachine<PlayerController.StateID> machine)
        {
            PlayClip(ledgeClip);
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
    }
}