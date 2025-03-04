// INFO:
/**
* This script handles:
* Summon Mechanics
*/
using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using WeaponsScripts;


[DisallowMultipleComponent]
public class SummonManager : MonoBehaviour
{

    [SerializeField] RuneType Rune;
    [SerializeField] private Transform runeParent;
    [SerializeField] private List<Rune> runeList;
    [SerializeField] Dictionary<String, RuneType> RuneDictionary;
    private WeaponManager weaponManager;
    [SerializeField] private Transform target;

    [Space]
    [Header("Runtime Filled")]
    public Rune activeRune;

    private GameObject ActiveRuneObject;

    float speed = 3;

    private void Awake()
    {
        weaponManager = GetComponent<WeaponManager>();


        RuneDictionary = new();
        foreach (var rune in runeList)
        {
            RuneDictionary[rune.runeSequenceId] = rune.runeType;
        }

        foreach (KeyValuePair<String, RuneType> rune in RuneDictionary)
        {
            Debug.Log($"Key:{rune.Key}, Value:{rune.Value}");
        }

    }

    //Spawn weapon from sequence
    public void SpawnFromSequence(String id)
    {
        Debug.Log($"Sequence: {id}");

        Rune rune = runeList.Find(rune => RuneDictionary[id] == Rune);

        if (rune == null)
        {
            Debug.LogError($"No ScriptableObject found for RuneType: {rune}");
            return;
        }

        activeRune = rune;
        rune.Spawn(runeParent, this);
        ActiveRuneObject = GameObject.FindGameObjectWithTag("Rune");

    }

    public void DespawnRune()
    {
        Destroy(ActiveRuneObject);
    }

    // Imbue weapon
    public void Imbue()
    {
        // Take active weapon
        WeaponScriptableObject ActiveWeapon = weaponManager.ActiveWeapon;
        Debug.Log($"{ActiveWeapon.name}");

        // Play animation of combination

        // change weapon properties


    }

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
