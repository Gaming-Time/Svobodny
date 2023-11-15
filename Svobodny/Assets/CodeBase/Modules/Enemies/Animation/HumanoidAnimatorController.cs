using System;
using CodeBase.Modules.Enemies.Movement;
using UnityEngine;

public class HumanoidAnimatorController : MonoBehaviour {
    private Animator _animator;
    private IMove _mover;

    private Vector3 _direction;
    private float _speed;
    

    public void Construct(Animator animator, IMove mover){
        _animator = animator;
        _mover = mover;
    }

    private void Update() {
        ReadInformation();
        UpdateVariables();
    }
    private void ReadInformation()
    {
        _direction = _mover.Velocity.normalized;
        _speed = _mover.Velocity.magnitude;
    }

    private void UpdateVariables()
    {
        _animator.SetFloat(HumanoidAnimationVariables.DirectionXHash, _direction.x);
        _animator.SetFloat(HumanoidAnimationVariables.DirectionYHash, _direction.z);
        _animator.SetFloat(HumanoidAnimationVariables.SpeedHash, _speed);
    }

}