using CodeBase.Modules.Character.Animation;
using CodeBase.Modules.Character.StateMachine;
using UnityEngine;

namespace CodeBase.Modules.Character.Interaction
{
    public class CharacterWardrobeInteraction : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        
        private CharacterAnimatorController _animatorController;
        private CharacterController _characterController;
        private CharacterMove _characterMove;
        private CharacterStateMachine _stateMachine;


        public void Construct(CharacterAnimatorController animatorController, CharacterController characterController,
            CharacterMove characterMove)
        {
            _animatorController = animatorController;
            _characterController = characterController;
            _characterMove = characterMove;
            _stateMachine = GetComponent<CharacterStateMachine>();
        }

        public void Enter(Vector3 at)
        {
            _stateMachine.enabled = false;
            _characterMove.enabled = false;
            _characterController.enabled = false;
            transform.position = at;
            _animatorController.EnterWardrobe();
        }

        public void Exit()
        {
            spriteRenderer.enabled = true;
            _animatorController.ExitWardrobe();
        }

        public void OnEnterAnimationFinished()
        {
            spriteRenderer.enabled = false;
        }

        public void OnExitAnimationFinished()
        {
            _stateMachine.enabled = true;
            _characterController.enabled = true;
            _characterMove.enabled = true;
        }
    }
}