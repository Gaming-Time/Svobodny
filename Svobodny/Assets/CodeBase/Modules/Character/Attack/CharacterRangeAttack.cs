using CodeBase.Infrastructure.Services.EnemyDetection;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Modules.Character.Audio;
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

        [SerializeField] private Transform arm;
        [SerializeField] private Transform shootPoint;
        [SerializeField] private LayerMask shootMask;
        [SerializeField] private float delay;
        [SerializeField] private int pistolDamage;

        private Vector3 _worldMousePosition;
        private Plane _plane;
        private float _lastShootTime;

        public void Construct(IInputService inputService, IEnemyDetectionService enemyDetectionService,
            CharacterVFXController vfxController,
            CharacterAudioController audioController, Camera camera)
        {
            _inputService = inputService;
            _enemyDetectionService = enemyDetectionService;
            _vfxController = vfxController;
            _audioController = audioController;
            _camera = camera;

            _plane = new Plane(Vector3.up, 0);
            _lastShootTime = -delay;
        }

        public void Shoot()
        {
            if (_lastShootTime + delay > Time.time)
                return;

            _lastShootTime = Time.time;
            _vfxController.PlayFlash();
            _audioController.PlayShoot();
            _enemyDetectionService.RegisterShot(transform.position);
            var ray = _camera.ScreenPointToRay(_inputService.MousePosition);
            if (_plane.Raycast(ray, out var distance))
                _worldMousePosition = ray.GetPoint(distance);

            _worldMousePosition.y = shootPoint.position.y;
            var direction = _worldMousePosition - shootPoint.position;
            var shootRay = new Ray(shootPoint.position, direction);

            if (!Physics.Raycast(shootRay, out var hit, direction.magnitude, shootMask)) return;

            Debug.LogWarning(hit.transform.gameObject.name);
            var health = hit.transform.GetComponent<IHealth>();
            health ??= hit.transform.GetComponentInParent<IHealth>();
            health?.DoDamage(DamageType.Shot, pistolDamage, shootPoint.position);
        }
    }
}