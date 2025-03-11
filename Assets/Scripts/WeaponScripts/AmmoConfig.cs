using UnityEngine;

namespace WeaponsScripts
{
    [CreateAssetMenu(fileName = "Ammo Configuration", menuName = "Weapons/Ammo Configuration")]
    public class AmmoConfig : ScriptableObject, System.ICloneable
    {
        public int MaxAmmo = 120;

        public int CurrentAmmo = 30;

        public bool IsInfinite = false;

        public void Replenish()
        {
            // add functionality of adding ammo from pickups
        }

        public bool CanReplenish()
        {
            if (CurrentAmmo == MaxAmmo)
            {
                return false;
            }

            return true;
        }

        public void AddAmmo(int Amount)
        {
            if (CurrentAmmo + Amount > MaxAmmo)
            {
                CurrentAmmo = MaxAmmo;
            }
            else
            {
                CurrentAmmo += Amount;
            }
        }

        // * ## ##
        public object Clone()
        {
            AmmoConfig config = CreateInstance<AmmoConfig>();

            Utilities.CopyValues(this, config);

            return config;
        }
    }
}

