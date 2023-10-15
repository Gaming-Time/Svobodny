using System;
using Cinemachine;
using CodeBase.Infrastructure.Services.Input;
using UnityEngine;

namespace CodeBase.Modules.Character.FOV
{
    public class RotateWithMouse : MonoBehaviour
    {
        private IInputService _inputService;
        private Camera _camera;
        private Plane _plane;
        private Vector3 _worldPosition;

        public void Construct(Camera mainCamera, IInputService inputService)
        {
            _camera = mainCamera;
            _inputService = inputService;
         
            _plane = new Plane(Vector3.up, 0);
        }

        private void Update()
        {
            float distance;
            var ray = _camera.ScreenPointToRay(_inputService.MousePosition);
            if (_plane.Raycast(ray, out distance))
            {
                _worldPosition = ray.GetPoint(distance);
            }
            transform.LookAt(_worldPosition);
        }
    }
}