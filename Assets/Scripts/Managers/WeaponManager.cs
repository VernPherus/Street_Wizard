
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


        [Space]
        [Header("Runtime Filled")]
        public WeaponScriptableObject ActiveWeapon;
        [field: SerializeField] public WeaponScriptableObject ActiveBaseWeapon { get; private set; }

        //public Weapon ActiveWeapon { get; private set; }

        private void Start()
        {
            WeaponScriptableObject weapon = weapons.Find(weapon => weapon.weaponType == Weapon);

            if (weapon == null)
            {
                Debug.LogError($"No ScriptableObject found for WeaponType: {weapon}");
                return;
            }

            SetupWeapon(weapon);
        }

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

        public void ApplyModifiers(IModifier[] Modifiers)
        {
            DespawActiveWeapon();
            SetupWeapon(ActiveBaseWeapon);

            foreach (IModifier modifier in Modifiers)
            {
                modifier.Apply(ActiveWeapon);
            }
        }

    }
}
