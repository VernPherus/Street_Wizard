
using System.Collections.Generic;
using UnityEngine;
using WeaponsScripts;
using SpellScripts;
using System.Collections;



namespace Managers
{
    [DisallowMultipleComponent]
    public class SummonManager : MonoBehaviour
    {

        [SerializeField] RuneType SelectedRune;
        [SerializeField] private Transform runeParent;
        [SerializeField] private List<RuneScriptableObject> runeList;
        [SerializeField] Dictionary<string, RuneType> RuneDictionary;
        private WeaponManager weaponManager;
        [SerializeField] private Transform target;

        [Space]
        [Header("Runtime Filled")]
        public RuneScriptableObject activeRune;

        [Header("Player Mana")]
        [SerializeField]
        private PlayerMana playerMana;

        public bool CanSummon { get; set; }

        private GameObject ActiveRuneObject;
        private RuneScriptableObject rune;

        private void Awake()
        {
            weaponManager = GetComponent<WeaponManager>();


            RuneDictionary = new();
            foreach (var rune in runeList)
            {
                RuneDictionary[rune.runeSequenceId] = rune.runeType;
            }

            foreach (KeyValuePair<string, RuneType> rune in RuneDictionary)
            {
                Debug.Log($"Key:{rune.Key}, Value:{rune.Value}");
            }

            CanSummon = true;

        }

        public bool RuneIsActive()
        {
            if (ActiveRuneObject != null)
            {
                return true;
            }
            return false;
        }

        private bool CheckManaIfCanSummon()
        {
            if (playerMana.CurrentMana >= rune.summonConfig.ManaConsumption)
            {
                return true;
            }
            return false;
        }

        // #############################################################################################################
        //* ## Spawn Logic ##        
        // #############################################################################################################

        public void SpawnFromSequence(string id)
        {
            Debug.Log($"Sequence: {id}");

            try
            {
                rune = runeList.Find(rune => rune.runeSequenceId == id);
            }
            catch (System.Exception)
            {
                Debug.LogError($"No ScriptableObject found for RuneType: {rune}");
                throw;
            }

            if (CanSummon && CheckManaIfCanSummon())
            {
                playerMana.CurrentMana -= rune.summonConfig.ManaConsumption;

                activeRune = rune;
                rune.Spawn(runeParent, this);
                ActiveRuneObject = GameObject.FindGameObjectWithTag("Rune");
                CanSummon = false;
            }
            else
            {
                Debug.Log("Can't summon");
            }
        }

        public void DespawnRune()
        {
            Destroy(ActiveRuneObject);
            CanSummon = true;
        }

        // #############################################################################################################
        //* ## Imbuing Logic ##        
        // #############################################################################################################

        public void Imbue()
        {
            // Take active weapon
            WeaponScriptableObject ActiveWeapon = weaponManager.ActiveWeapon;
            Debug.Log($"{ActiveWeapon.name}");

            // Play animation of combination

            // change weapon properties

            weaponManager.ApplyModifiers(activeRune.summonConfig.ReturnModifiers());

            //Start timer before removing effects

            StartCoroutine(RemoveImbueAfterTime(activeRune.summonConfig.ImbueDuration));
            DespawnRune();
        }

        private IEnumerator RemoveImbueAfterTime(float duration)
        {
            yield return new WaitForSeconds(duration);
            CanSummon = true;

            Debug.Log($"Removing rune effects of {activeRune.name}.");

            // Remove the rune's modifiers from the weapon
            foreach (var modifier in activeRune.summonConfig.modifiers)
            {
                weaponManager.RemoveModifier(modifier);
            }

        }
    }
}


