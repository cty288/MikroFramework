using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoSingleton<AudioManager> {

	public GameObject go_Music;
	public GameObject go_UI;
    public AudioMixer mixer;
	private AudioSourceInfo asi_BGM;
	private AudioSourceInfo asi_UI;

	public void Init()
	{
		asi_BGM = new AudioSourceInfo(go_Music);
		asi_UI = new AudioSourceInfo(go_UI);
	}

	public void PlaySFX(AudioClip clip)
	{
		asi_UI.Clip = clip;
		asi_UI.Play();
	}
	
	public void PlayMusic(MusicAudio music)
	{
		AudioClip musicToPlay = Resources.Load<AudioClip>("Audio/Music/" + music.ToString());
		asi_BGM.Clip = musicToPlay;
		asi_BGM.PlayFromStart();
	}

	public void PlaySFX(SFXAudio SFXName)
	{
		AudioClip SFXToPlay = Resources.Load<AudioClip>("Audio/SFX/" + SFXName.ToString());
		asi_UI.Clip = SFXToPlay;
		asi_UI.Play();
	}

    public void StopMusic()
    {
        asi_BGM.Stop();
    }

	public void MuteAudio(bool mute)
    {
        asi_BGM.audioSource.mute = mute;
        asi_UI.audioSource.mute = mute;
    }

    public void SetMusicVolume(float value)
    {
        mixer.SetFloat("musicVolume", value);
    }

    public void SetSFXVolume(float value)
    {
        mixer.SetFloat("sfxVolume", value);
    }

    public void TogglePauseMusic()
	{
		if (asi_BGM.audioState == AudioState.IsPlaying)
		{
			asi_BGM.Pause();
		}
		else if(asi_BGM.audioState == AudioState.Pause)
		{
			asi_BGM.Play();
		}
	}

}

public enum MusicAudio
{
    FailureAudio,
    VictoryAudio,
    InGameAudio,
    MainMenuAudio
}

public enum SFXAudio
{
    SFX_BtnClick,
    SFX_GameEnd,
    SFX_BuyItem,
    SFX_ClickRight,
    SFX_Miss,
    SFX_Dialogue,
    SFX_CoinNotEnough
}
