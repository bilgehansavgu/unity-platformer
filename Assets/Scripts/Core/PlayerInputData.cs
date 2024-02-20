using UnityEngine;

namespace Core
{
    [System.Serializable]
    public struct PlayerInputData
    {
        public Vector2 MoveInputValue;
        public bool JumpTriggered;
        public bool AttackSquareActionTriggered;
        public bool AttackTriangleActionTriggered;
        public bool InputDirection;
        public bool DashTriggered;
    }
}
