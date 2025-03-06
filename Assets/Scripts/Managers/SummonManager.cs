
using System.Collections.Generic;
using UnityEngine;
using WeaponsScripts;
using SpellScripts;
using WeaponsScripts.Modifiers;
using System.Collections;


namespace Managers
{
    [DisallowMultipleComponent]
    public class SummonManager : MonoBehaviour
    {

        [SerializeField] RuneType Rune;
        [SerializeField] private Transform runeParent;
        [SerializeField] private List<RuneScriptableObject> runeList;
        [SerializeField] private List<MonoBehaviour> runeModifierList;
        [SerializeField] Dictionary<string, RuneType> RuneDictionary;
        private WeaponManager weaponManager;
        [SerializeField] private Transform target;

        [Space]
        [Header("Runtime Filled")]
        public RuneScriptableObject activeRune;

        public bool HasSummoned { get; set; }


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

            DamageModifier damageModifierA = new() { Amount = 100f, AttributeName = "DamageConfig/DamageCurve" },

            tempList = new()
            {


            };

        }

        // #############################################################################################################
        //* ## Spawn Logic ##        
        // #############################################################################################################

        public void SpawnFromSequence(string id)
        {
            Debug.Log($"Sequence: {id}");

            try
            {
                rune = runeList.Find(rune => RuneDictionary[id] == Rune);
            }
            catch (System.Exception)
            {
                Debug.LogError($"No ScriptableObject found for RuneType: {rune}");
                throw;
            }


            activeRune = rune;
            rune.Spawn(runeParent, this);
            ActiveRuneObject = GameObject.FindGameObjectWithTag("Rune");

        }

        public void DespawnRune()
        {
            Destroy(ActiveRuneObject);
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
        }

        private IEnumerator RemoveImbueAfterTime(float duration)
        {
            yield return new WaitForSeconds(duration);

            Debug.Log($"Removing rune effects of {activeRune.name}.");

            // Remove the rune's modifiers from the weapon
            foreach (var modifier in activeRune.summonConfig.modifiers)
            {
                weaponManager.RemoveModifier(modifier);
            }

        }

        // #############################################################################################################
        //* ## Effects Logic ##        
        // #############################################################################################################

        // IEnumerator MoveSpell(Vector3 targetPosition)
        // {
        //     Vector3 startPosition = activeRune.SpawnPoint;

        //     float moveDuration = 5;
        //     float timeElapsed = 0;
        //     while (timeElapsed < moveDuration)
        //     {
        //         ActiveRuneObject.transform.position = Vector3.Lerp(startPosition, targetPosition, timeElapsed / moveDuration);
        //         timeElapsed += Time.deltaTime;
        //         yield return null;
        //     }

        //     ActiveRuneObject.transform.position = targetPosition;
        // }




    }
}


