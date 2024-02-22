using UnityEngine;

namespace CodeBase.Modules.Common.Health
{
    public interface IHealth
    {
        int Health { get; }
        void DoDamage(int damage);
        void DoDamage(DamageType damageType, int damage);
        void DoDamage(DamageType damageType, int damage, Vector3 direction);
        void PlayDeathAnimation();
    }

    public enum DamageType
    {
        Melee,
        Shot
    }
}