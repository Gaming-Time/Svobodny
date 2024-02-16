using CodeBase.Modules.Enemies.Attack;
using UnityEngine;

namespace CodeBase.Modules.Enemies.Animation
{
    public class HumanoidAnimationEventsHandler : MonoBehaviour
    {
        private EnemyAttack _enemyAttack;

        public void Construct(EnemyAttack enemyAttack) => _enemyAttack = enemyAttack;

        public void OnAttackAnimationEvent() => _enemyAttack.Attack();
    }
}