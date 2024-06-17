using CodeBase.Infrastructure.Services.Input;
using CodeBase.Modules.Enemies.Ai.Entity;
using UnityEngine;

namespace CodeBase.Modules.Character.Ai
{
    public class PlayerEntity : MonoBehaviour, IEntity
    {
        private IInputService _inputService;
        
        public EntityType Type => EntityType.Player;
        public GameObject GameObject => gameObject;
        public Vector3 Position => transform.position;
        public bool IsSneaking { get; set; }
        

        public void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        private void Update()
        {
            
        }
    }
}