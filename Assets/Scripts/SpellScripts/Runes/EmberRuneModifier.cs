using System.Collections.Generic;
using UnityEngine;
using WeaponsScripts.Modifiers;

namespace SpellScripts.Runes
{
    public class EmberRuneModifier : MonoBehaviour
    {
        public List<IModifier> Modifiers { get; private set; }

        public List<IModifier> returnModifiers()
        {
            Modifiers = new()
            {
                new DamageModifier()
                {
                    Amount = 100f,
                    AttributeName = "DamageConfig/DamageCurve"
                }
            };

            return Modifiers;
        }
    }
}