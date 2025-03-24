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
        [SerializeField] private WeaponScriptableObject DefaultWeapon;
        private Dictionary<WeaponType, WeaponScriptableObject> ActiveWeapons = new();

        [SerializeField]
        private List<IModifier> activeModifiers = new();
        [SerializeField] private PlayerStats playerStats;

        [Space]
        [Header("Runtime Filled")]
        public WeaponScriptableObject ActiveWeapon;
        [field: SerializeField] public WeaponScriptableObject ActiveBaseWeapon { get; private set; }

        private void Awake()
        {
            foreach (var weaponDef in weapons)
            {
                WeaponScriptableObject weaponInstance = Instantiate(weaponDef);
                ActiveWeapons.Add(weaponDef.weaponType, weaponInstance);
            }

            // WeaponScriptableObject weapon = weapons.Find(weapon => weapon.weaponType == Weapon);

            // if (weapon == null)
            // {
            //     Debug.LogError($"No ScriptableOb;ject found for WeaponType: {weapon}");
            //     return;
            // }

            if (ActiveWeapons.TryGetValue(DefaultWeapon.weaponType, out var defaultWeapon))
            {
                SetupWeapon(defaultWeapon);
            }
            else
            {
                Debug.Log("Default weapon not found in active weapons list");
            }
        }

        // #############################################################################################################
        //* ## Weapon Setup Logic ##        
        // #############################################################################################################

        private void SetupWeapon(WeaponScriptableObject weapon)
        {
            if (ActiveWeapon != null)
            {
                ActiveWeapon.Despawn();
            }

            ActiveBaseWeapon = weapon;
            ActiveWeapon = weapon;
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

        public void PickupWeapon(WeaponType weaponType)
        {
            if (!playerStats.CheckForWeaponUnlock(weaponType))
            {
                Debug.Log($"Weapon: {weaponType} is locked");
                return;
            }

            if (ActiveWeapons.TryGetValue(weaponType, out var newWeapon))
            {
                SetupWeapon(newWeapon);
            }
            else
            {
                Debug.Log($"{weaponType} not found");
            }
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
            List<WeaponType> unlockedWeaponTypes = playerStats.unlockedWeapons;

            if (unlockedWeaponTypes.Count == 0)
            {
                return;
            }

            int currentIndex = unlockedWeaponTypes.IndexOf(ActiveBaseWeapon.weaponType);
            int nextIndex = (currentIndex + direction + unlockedWeaponTypes.Count) % unlockedWeaponTypes.Count;

            PickupWeapon(unlockedWeaponTypes[nextIndex]);
        }

        public void SwitchWeaponByIndex(int index)
        {
            List<WeaponType> unlockedWeaponTypes = playerStats.unlockedWeapons;

            if (index >= 0 && index < unlockedWeaponTypes.Count)
            {
                PickupWeapon(unlockedWeaponTypes[index]);
            }
        }

    }
}
