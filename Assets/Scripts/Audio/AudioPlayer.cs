using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SFXAudioClip
{
	public SFXAudioClip(string path, float volume, AudioMixerGroup mixerGroup, CustomEvent trigger)
	{
        var soundObj = new GameObject("SFXAudioClip");
        var audioSource = soundObj.AddComponent(typeof(AudioSource)) as AudioSource;
        audioSource.clip = Resources.Load<AudioClip>(path);
        audioSource.volume = volume;
        audioSource.outputAudioMixerGroup = mixerGroup;
        audioSource.playOnAwake = false;
        audioSource.loop = false;
        trigger.Subscribe((n, o) => audioSource.Play());
	}

}

public class AudioPlayer : MonoBehaviour
{
    
    public const string BUTTON = "Audio/Menu__006";
	public const string GOAL   = "Audio/Explosion2__006";
	public const string BALL   = "Audio/Menu__004";

    public const string MIXER  = "Audio/Mixer";
    public const string SFX    = "SFX1";
    public const string BGM    = "BGM";

	void Awake()
	{
        var mixer = Resources.Load<AudioMixer>(MIXER);
        var sfxGroup = mixer.FindMatchingGroups(SFX)[0];
        var bgmGroup = mixer.FindMatchingGroups(BGM)[0];
        
        // List of audio clips and events that trigger them
        // List<SFXAudioClip> Clips = new List<SFXAudioClip>() {
        new SFXAudioClip(BUTTON,     1, sfxGroup, Events.onButtonPressed);
        new SFXAudioClip(BUTTON,  0.9f, sfxGroup, Events.onFocusEntered);
        new SFXAudioClip(BUTTON,  0.9f, sfxGroup, Events.onSettingsChanged);
        new SFXAudioClip(GOAL,   0.25f, sfxGroup, Events.onGoalEnter);
        new SFXAudioClip(BALL,    0.9f, sfxGroup, Events.onBallCollided);
        new SFXAudioClip(BALL,   0.25f, sfxGroup, Events.onBallKicked);
        // };
	}

}
