using CodeBase.Extensions;
using CodeBase.Infrastructure.Services.EnemyDetection;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Logic.UsableObjects.Essentials;
using CodeBase.Modules.Character.Audio;
using CodeBase.Modules.Character.UI;
using CodeBase.Modules.Character.VFX;
using CodeBase.Modules.Common.Health;
using UnityEngine;

namespace CodeBase.Modules.Character.Attack
{
    public class CharacterRangeAttack : MonoBehaviour
    {
        private Camera _camera;
        private IInputService _inputService;
        private IEnemyDetectionService _enemyDetectionService;
        private CharacterVFXController _vfxController;
        private CharacterAudioController _audioController;
        private InventoryHandler _inventoryHandler;
        
        [SerializeField] private LayerMask shootMask;
        [SerializeField] private LayerMask blockMask;
        [SerializeField] private float delay;
        [SerializeField] private int pistolDamage;
        [SerializeField] private Transform eyesPosition;
        
        private float _lastShootTime;

        public void Construct(IInputService inputService, IEnemyDetectionService enemyDetectionService,
            CharacterVFXController vfxController,
            CharacterAudioController audioController, InventoryHandler inventoryHandler, Camera camera)
        {
            _inputService = inputService;
            _enemyDetectionService = enemyDetectionService;
            _vfxController = vfxController;
            _audioController = audioController;
            _inventoryHandler = inventoryHandler;
            _camera = camera;
            
            _lastShootTime = -delay;
        }

        public void Shoot()
        {
            if (_lastShootTime + delay > Time.time)
                return;

            _inventoryHandler.RemoveEssential(EssentialType.Bullet, 1);
            _lastShootTime = Time.time;
            _vfxController.PlayFlash();
            _audioController.PlayShoot();
            _enemyDetectionService.RegisterShot(transform.position);
            var ray = _camera.ScreenPointToRay(_inputService.MousePosition);
            if (!Physics.Raycast(ray, out var hitInfo, 100f, shootMask))
                return;

            var direction = hitInfo.transform.position - eyesPosition.position;
            var blockRay = new Ray(eyesPosition.position, direction);
            IHealth health;

            if (Physics.Raycast(blockRay, out var blockHit, direction.magnitude, blockMask))
            {
                var blockHitGameObject = blockHit.transform.gameObject;
                if (!shootMask.IsLayerInMask(blockHitGameObject.layer))
                    return;

                health = blockHitGameObject.GetComponent<IHealth>();
                health ??= blockHitGameObject.GetComponentInParent<IHealth>();
            }
            else
            {
                var hitGameobject = hitInfo.transform.gameObject;
                health = hitGameobject.GetComponent<IHealth>();
                health ??= hitGameobject.GetComponentInParent<IHealth>();
            }

            health?.DoDamage(DamageType.Shot, pistolDamage, eyesPosition.position);
        }
    }
}