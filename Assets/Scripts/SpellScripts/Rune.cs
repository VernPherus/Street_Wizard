
using System.Collections;
using UnityEngine;

public enum RuneType
{
    Ember,
    Frost,
    Crystal,
    Gas
}

[CreateAssetMenu(fileName = "NewRune", menuName = "Runes/Rune")]
public class Rune : ScriptableObject
{
    public string runeName;
    public string runeSequenceId;

    public RuneType runeType;
    public GameObject runePrefab;

    public Vector3 SpawnPoint;
    public Vector3 SpawnRotation;

    private GameObject RuneModel;

    private ParticleSystem RuneParticleSystem;

    public ManaConfig manaConfig;
    public SummonConfig summonConfig;

    private MonoBehaviour activeMonoBehavior;

    public void Spawn(Transform Parent, MonoBehaviour ActiveMonoBehavior)
    {
        activeMonoBehavior = ActiveMonoBehavior;

        RuneModel = Instantiate(runePrefab);
        RuneModel.transform.SetParent(Parent, false);
        RuneModel.transform.SetLocalPositionAndRotation(SpawnPoint, Quaternion.Euler(SpawnRotation));

        RuneParticleSystem = RuneModel.GetComponentInChildren<ParticleSystem>();
    }

}