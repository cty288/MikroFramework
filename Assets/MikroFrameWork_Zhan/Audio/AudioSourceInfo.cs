using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceInfo {

	private float playTime = 0;

	public AudioSource audioSource;
	public AudioState audioState = AudioState.Idle;

	public Action musicEnd;

	public AudioSourceInfo(GameObject go)
	{
		this.audioSource = go.GetComponent<AudioSource>();
	}

	public AudioClip Clip
	{
		get
		{
			return this.audioSource.clip;
		}
		set
		{
			this.audioSource.clip = value;
			playTime = 0;
		}
	}

	public void PlayFromStart()
	{
		if (this.audioSource == null)
		{
			return;
		}
		this.audioState = AudioState.IsPlaying;
		this.audioSource.Stop();
		this.audioSource.Play();
	}
	public void Play()
	{
		if(this.audioSource == null)
		{
			return;
		}
		this.audioState = AudioState.IsPlaying;
		this.audioSource.Play();
	}
	public void Pause()
	{
		if (this.audioSource == null)
		{
			return;
		}
		if (this.audioSource.isPlaying)
		{
			this.audioState = AudioState.Pause;
			this.audioSource.Pause();
		}
	}
	public void Stop()
	{
		if (this.audioSource == null)
		{
			return;
		}
		this.audioState = AudioState.Stop;
		this.audioSource.Stop();
		if(musicEnd != null)
		{
			musicEnd();
		}
	}

	private void Update()
	{
		if(this.audioSource != null && this.Clip != null && this.audioState == AudioState.IsPlaying)
		{
			playTime += Time.deltaTime;
			if(playTime > this.Clip.length)
			{
				playTime = 0;
				this.Stop();
			}
		}
	}
}

public enum AudioState
{
	Idle,
	IsPlaying,
	Pause,
	Stop
}
