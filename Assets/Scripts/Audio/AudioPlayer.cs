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
    
    public const string PIECE_DESTROY = "Audio/blink_disappear_0001";
    public const string HIGHLIGHT     = "Audio/low_subtle_click";
    public const string JUMP          = "Audio/quick_jump_lowfrequency";
    public const string SELECT        = "Audio/blipSelect_0001";

    public const string MIXER  = "Audio/Mixer";
    public const string SFX    = "SFX1";
    public const string BGM    = "BGM";

	void Awake()
	{
        var mixer = Resources.Load<AudioMixer>(MIXER);
        var sfxGroup = mixer.FindMatchingGroups(SFX)[0];
        var bgmGroup = mixer.FindMatchingGroups(BGM)[0];
        
        // List of audio clips and events that trigger them
        new SFXAudioClip(PIECE_DESTROY, 1, sfxGroup, Events.onGridPieceDestroy);
        new SFXAudioClip(HIGHLIGHT,     1, sfxGroup, Events.onHighlight);
        new SFXAudioClip(JUMP,          1, sfxGroup, Events.onGridPieceJump);
        new SFXAudioClip(SELECT,        1, sfxGroup, Events.onGridPieceSelect);


        new SFXAudioClip(SELECT,     1, sfxGroup, Events.onButtonPressed);
        new SFXAudioClip(SELECT,  0.9f, sfxGroup, Events.onFocusEntered);
        new SFXAudioClip(SELECT,  0.9f, sfxGroup, Events.onSettingsChanged);
	}

}
