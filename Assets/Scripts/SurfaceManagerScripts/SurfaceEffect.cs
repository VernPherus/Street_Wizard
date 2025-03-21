using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Impact System/Surface Effect", fileName = "SurfaceEffect")]
public class SurfaceEffect : ScriptableObject
{
    public List<SpawnObjectEffect> SpawnObjectEffects = new();
    public List<PlayAudioEffect> PlayAudioEffects = new();
}