using UnityEngine;

namespace WeaponsScripts.ImpactEffects
{
    public class Explode : AbstractAreaOfEffect
    {
        public Explode(float Radius, AnimationCurve DamageFalloff, int BaseDamage, int MaxEnemiesAffected) :
            base(Radius, DamageFalloff, BaseDamage, MaxEnemiesAffected)
        { }
    }
}