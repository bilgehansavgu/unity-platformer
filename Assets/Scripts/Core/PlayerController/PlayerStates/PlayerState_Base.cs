using Core.FSM;
using UnityEngine;

namespace Core.CharacterController
{
    public abstract class PlayerState_Base : StateBase<PlayerController.StateID>
    {
        protected PlayerController parent;

        protected PlayerState_Base(PlayerController parent)
        {
            this.parent = parent;
        }

        protected void PlayClip(string clipName)
        {
            parent.Animator.Play(clipName);
        }

        protected void PlayClip(string clipName, float index, int totalFramesInAnimation)
        {
            parent.Animator.Play(clipName, 0, (1f / totalFramesInAnimation) * index);
        }
        
        protected void HandleSpriteDirection(float xAxis)
        {
            if (xAxis != 0f)
                parent.SpriteRenderer.flipX = xAxis < 0f;

            //if (xAxis == 0f)
            //    return;
            //else if (xAxis > 0f)
            //    parent.transform.localScale = new Vector3(1, 1, 1);
            //else if (xAxis < 0f)
            //    parent.transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
