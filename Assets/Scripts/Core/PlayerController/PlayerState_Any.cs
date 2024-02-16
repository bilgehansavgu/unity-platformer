using Core.StateMachine;

namespace Core.CharacterController
{
    public class PlayerState_Any : AnyState<PlayerController.StateID>
    {
        public PlayerState_Any(StateMachine<PlayerController.StateID> machine) : base(machine)
        {
        }
    }
}
