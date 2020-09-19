using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //array of sounds which is found in SoundScript script
    public SoundScript[] sounds;

	// Awake is similar to start but it is called right before something happens
	void Awake()
	{
        //loop sound
		foreach (SoundScript s in sounds)
		{
            //loop audiosource
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;

			s.source.volume = s.volume;
			s.source.pitch = s.pitch;
			s.source.loop = s.loop;
		}
    }

    
    public void Play(string name)
    {
        //find a specific sound in the array of sounds
		SoundScript s = Array.Find(sounds, sound => sound.name == name);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}
        //play sound
		s.source.Play();
    }

    public void Stop(string name)
	{
		//find a specific sound in the array of sounds
		SoundScript s = Array.Find(sounds, sound => sound.name == name);
        //stop sound
		s.source.Stop();
	}
}
