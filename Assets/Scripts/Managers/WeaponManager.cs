using System.Collections.Generic;
using UnityEngine;
using WeaponsScripts;
using WeaponsScripts.Modifiers;

namespace Managers
{
    [DisallowMultipleComponent]
    public class WeaponManager : MonoBehaviour
    {
        public Camera Camera;
        [SerializeField] WeaponType Weapon;
        [SerializeField] private Transform weaponParent;
        [SerializeField] List<WeaponScriptableObject> weapons;

        private List<IModifier> activeModifiers = new();

        [Space]
        [Header("Runtime Filled")]
        public WeaponScriptableObject ActiveWeapon;
        [field: SerializeField] public WeaponScriptableObject ActiveBaseWeapon { get; private set; }

        private int BanisherAmmo = 0;
        private int GatlingWantAmmo = 0;
        private int BigBoreAmmo = 0;

        private void Awake()
        {
            WeaponScriptableObject weapon = weapons.Find(weapon => weapon.weaponType == Weapon);

            if (weapon == null)
            {
                Debug.LogError($"No ScriptableOb;ject found for WeaponType: {weapon}");
                return;
            }

            SetupWeapon(weapon);
        }

        // #############################################################################################################
        //* ## Weapon Setup Logic ##        
        // #############################################################################################################

        private void SetupWeapon(WeaponScriptableObject Weapon)
        {
            ActiveBaseWeapon = Weapon;
            ActiveWeapon = Weapon.Clone() as WeaponScriptableObject;
            ActiveWeapon.Spawn(weaponParent, this, Camera);
        }

        public void DespawActiveWeapon()
        {
            if (ActiveWeapon != null)
            {
                ActiveWeapon.Despawn();
            }

            Destroy(ActiveWeapon);
        }

        public void PickupGun(WeaponScriptableObject Weapon)
        {
            DespawActiveWeapon();
            this.Weapon = Weapon.weaponType;
            SetupWeapon(Weapon);
        }

        // #############################################################################################################
        //* ## Modifier Logic ##        
        // #############################################################################################################

        public void ApplyModifiers(IModifier[] Modifiers)
        {
            DespawActiveWeapon();
            SetupWeapon(ActiveBaseWeapon);

            foreach (IModifier modifier in Modifiers)
            {
                modifier.Apply(ActiveWeapon);
                activeModifiers.Add(modifier);
            }
        }

        public void RemoveModifier(IModifier modifier)
        {
            if (activeModifiers.Contains(modifier))
            {
                activeModifiers.Remove(modifier);
                ResetWeapon();
                ApplyModifiers(activeModifiers.ToArray());
            }
        }

        public void ResetWeapon()
        {
            ActiveWeapon = ActiveBaseWeapon.Clone() as WeaponScriptableObject;
            SetupWeapon(ActiveWeapon);
            Debug.Log("Weapon is reset.");
        }

        // #############################################################################################################
        //* ## Weapon Switching Logic ##        
        // #############################################################################################################

        public void SwitchWeapon(int direction)
        {
            int currentIndex = weapons.IndexOf(ActiveBaseWeapon);
            int nextIndex = (currentIndex + direction + weapons.Count) % weapons.Count;

            PickupGun(weapons[nextIndex]);
        }

        public void SwitchWeaponByIndex(int index)
        {
            if (index >= 0 && index < weapons.Count)
            {
                PickupGun(weapons[index]);
            }
        }

        // #############################################################################################################
        //* ##  Ammo Persistence Logic ##        
        // #############################################################################################################

        public void SaveCurrentWeaponAmmo() { }

    }
}
