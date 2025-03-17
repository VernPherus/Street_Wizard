using SpellScripts;
using UnityEngine;

[CreateAssetMenu(fileName = "Imbue Configuration", menuName = "Weapons/Imbue Configuration")]
public class ImbueConfig : ScriptableObject
{
    [SerializeField] public GameObject[] sfxPrefabs;
    [SerializeField] public ParticleSystem WeaponEffect;
    [SerializeField] public bool isImbued;
    [SerializeField] public float DamageMultiplier;
    public RuneType currentRune;
    public GameObject ChangeProjectile()
    {
        switch (currentRune)
        {
            case RuneType.Ember:
                return sfxPrefabs[1];
            case RuneType.Frost:
                return sfxPrefabs[2];
            case RuneType.Crystal:
                return sfxPrefabs[3];
            case RuneType.Gas:
                return sfxPrefabs[4];
            default:
                break;
        }
        return sfxPrefabs[0];
    }
    public void SpawnParticlesOnImbue() { }

}