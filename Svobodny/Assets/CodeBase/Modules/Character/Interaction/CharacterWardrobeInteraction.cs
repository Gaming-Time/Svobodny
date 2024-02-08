using CodeBase.Modules.Character.Animation;
using UnityEngine;

namespace CodeBase.Modules.Character.Interaction
{
    public class CharacterWardrobeInteraction : MonoBehaviour
    {
        private CharacterAnimatorController _animatorController;
        private CharacterController _characterController;
        private CharacterMove _characterMove;


        public void Construct(CharacterAnimatorController animatorController, CharacterController characterController,
            CharacterMove characterMove)
        {
            _animatorController = animatorController;
            _characterController = characterController;
            _characterMove = characterMove;
        }

        public void Enter(Vector3 at)
        {
            _characterMove.enabled = false;
            _characterController.enabled = false;
            transform.position = at;
            _animatorController.EnterWardrobe();
        }

        public void Exit()
        {
            _characterController.enabled = true;
            _characterMove.enabled = true;
            gameObject.SetActive(true);
        }

        public void OnEnterAnimationFinished() => gameObject.SetActive(false);
    }
}