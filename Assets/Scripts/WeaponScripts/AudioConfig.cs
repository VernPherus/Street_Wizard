using UnityEngine;
using WeaponsScripts;

[CreateAssetMenu(fileName = "Audio Config", menuName = "Weapons/AudioConfig")]
public class AudioConfig : ScriptableObject, System.ICloneable
{
    [Range(0, 1f)]
    public float Volume = 1f;
    public AudioClip[] FireClips;
    public AudioClip EmptyClip;

    public void PlayeShootingClip(AudioSource AudioSource)
    {
        AudioSource.PlayOneShot(FireClips[Random.Range(0, FireClips.Length)], Volume);
    }

    public void PlayeOutOfAmmoClip(AudioSource AudioSource)
    {
        if (EmptyClip != null)
        {
            AudioSource.PlayOneShot(EmptyClip, Volume);
        }
    }

    public object Clone()
    {
        AudioConfig config = CreateInstance<AudioConfig>();

        Utilities.CopyValues(this, config);

        return config;
    }


}
