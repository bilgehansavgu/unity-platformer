using UnityEngine;

namespace Core
{
    [System.Serializable]
    public struct PlayerInputData
    {
        public Vector2 MoveInputValue;
        public bool jumpTriggered;
        public bool attackSquareActionTriggered;
        public bool attackTriangleActionTriggered;
        public bool inputDirection;
        public bool dashTriggered;
    }
}
