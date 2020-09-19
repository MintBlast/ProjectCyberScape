using UnityEngine.Audio;
using UnityEngine;
//custom class so serializable makes it available in the inspector
[System.Serializable]
public class SoundScript 
{
	public string name;

	public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;

	public bool loop;

    //this method is hidden in the inspector
    [HideInInspector]
	public AudioSource source;
}
