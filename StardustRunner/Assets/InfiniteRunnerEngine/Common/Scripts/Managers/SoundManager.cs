using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
using System;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

namespace MoreMountains.InfiniteRunnerEngine
{	
	[Serializable]
	public class SoundSettings
	{
		public bool MusicOn = true;
		public bool SfxOn = true;
		public float MusicLevel;
		public float SfxLevel;
	}

	public struct IRESfxEvent
	{
		public delegate void Delegate(AudioClip clipToPlay, AudioMixerGroup audioGroup = null, float volume = 1f, float pitch = 1f);
		static private event Delegate OnEvent;

		static public void Register(Delegate callback)
		{
			OnEvent += callback;
		}

		static public void Unregister(Delegate callback)
		{
			OnEvent -= callback;
		}

		static public void Trigger(AudioClip clipToPlay, AudioMixerGroup audioGroup = null, float volume = 1f, float pitch = 1f)
		{
			OnEvent?.Invoke(clipToPlay, audioGroup, volume, pitch);
		}
	}


	/// <summary>
	/// This persistent singleton handles sound playing
	/// </summary>
	[AddComponentMenu("Infinite Runner Engine/Managers/Sound Manager")]
	public class SoundManager : MMPersistentSingleton<SoundManager>, MMEventListener<MMGameEvent>
	{	
		[Header("Settings")]
		public SoundSettings Settings;

		[Header("Music")]
		/// true if the music is enabled	
		//public bool MusicOn=true;
		/// the music volume
		[Range(0,1)]
		public float MusicVolume;

		[Header("Sound Effects")]
		/// true if the sound fx are enabled
		//public bool SfxOn=true;
		/// the sound fx volume
		[Range(0,1)]
		public float SfxVolume=1f;

		[Header("Pause")]
		public bool MuteSfxOnPause = true;

		protected const string _saveFolderName = "CorgiEngine/";
		protected const string _saveFileName = "sound.settings";

		public AudioMixer mainMixer;

		protected AudioSource _backgroundMusic;

		public AudioSource bikeAudioSource;

		public List<AudioSource> _loopingSounds;

		private void Start()
		{
			MusicVolume = Settings.MusicLevel;
			SfxVolume = Settings.SfxLevel;

			if (SceneManager.GetActiveScene().name != "MainMenuScene")
				bikeAudioSource = LevelManager.Instance.CurrentPlayableCharacters[0].GetComponent<AudioSource>();
		}

		/// <summary>
		/// Plays a background music.
		/// Only one background music can be active at a time.
		/// </summary>
		/// <param name="Clip">Your audio clip.</param>
		public virtual void PlayBackgroundMusic(AudioSource Music)
		{
			// if the music's been turned off, we do nothing and exit
			if (!Settings.MusicOn)
				return;
			// if we already had a background music playing, we stop it
			if (_backgroundMusic!=null)
				_backgroundMusic.Stop();
			// we set the background music clip
			_backgroundMusic=Music;
			// we set the music's volume
			_backgroundMusic.volume= MusicVolume;
			// we set the loop setting to true, the music will loop forever
			_backgroundMusic.loop=true;
			//add to main mixer
			_backgroundMusic.outputAudioMixerGroup = mainMixer.FindMatchingGroups("Master")[0];
			// we start playing the background music
			_backgroundMusic.Play();		
		}	
		
		/// <summary>
		/// Plays a sound
		/// </summary>
		/// <returns>An audiosource</returns>
		/// <param name="sfx">The sound clip you want to play.</param>
		/// <param name="location">The location of the sound.</param>
		/// <param name="loop">If set to true, the sound will loop.</param>
		public virtual AudioSource PlaySound(AudioClip sfx, Vector3 location, bool loop=false)
		{
			if (!Settings.SfxOn)
				return null;
			// we create a temporary game object to host our audio source
			GameObject temporaryAudioHost = new GameObject("TempAudio");
			// we set the temp audio's position
			temporaryAudioHost.transform.position = location;
			// we add an audio source to that host
			AudioSource audioSource = temporaryAudioHost.AddComponent<AudioSource>() as AudioSource; 
			// we set that audio source clip to the one in paramaters
			audioSource.clip = sfx; 
			// we set the audio source volume to the one in parameters
			audioSource.volume = SfxVolume;
			// we set our loop setting
			audioSource.loop = loop;
			//add to main mixer
			audioSource.outputAudioMixerGroup = mainMixer.FindMatchingGroups("Master")[0];
			// we start playing the sound
			audioSource.Play(); 

			if (!loop)
			{
				// we destroy the host after the clip has played
				Destroy(temporaryAudioHost, sfx.length);
			}
			else
			{
				_loopingSounds.Add (audioSource);
			}

			// we return the audiosource reference
			return audioSource;
		}

		public virtual AudioSource PlaySoundSource(AudioSource sfx, Vector3 location, bool loop = false, bool followParent = false)
		{
			if (!Settings.SfxOn)
				return null;
			// we create a temporary game object to host our audio source
			GameObject temporaryAudioHost = new GameObject();
			// we set the temp audio's position
			temporaryAudioHost.transform.position = location;
			if (followParent)
				temporaryAudioHost.transform.SetParent(sfx.GetComponentInParent<Transform>());
			// we add an audio source to that host
			AudioSource audioSource = temporaryAudioHost.AddComponent<AudioSource>(sfx);
			temporaryAudioHost.name = "TempAudio (" + sfx.clip.name + ")";
			// we set the audio source volume to the one in parameters
			audioSource.volume = sfx.volume*SfxVolume;
			// we set our loop setting
			audioSource.loop = loop;
			//add to main mixer
			audioSource.outputAudioMixerGroup = mainMixer.FindMatchingGroups("Master")[0];
			// we start playing the sound
			audioSource.Play();

			if (!loop)
			{
				// we destroy the host after the clip has played
				Destroy(temporaryAudioHost, sfx.clip.length);
			}
			else
			{
				_loopingSounds.Add(audioSource);
			}

			// we return the audiosource reference
			return audioSource;
		}

		public virtual void ChangeVolumeLevel(float newValue,string soundType)
		{
			switch (soundType)
			{
				case "BackgroundMusic":
					MusicVolume = newValue;
					_backgroundMusic.volume = newValue;
					break;
				case "SoundEffect":
					SfxVolume = newValue;
					bikeAudioSource.volume = newValue*0.1f;
					break;
			}
			Settings.MusicLevel = MusicVolume;
			Settings.SfxLevel = SfxVolume;
		}

		/// <summary>
		/// Stops the looping sounds if there are any
		/// </summary>
		/// <param name="source">Source.</param>
		public virtual void StopLoopingSound(string source)
		{
			foreach(AudioSource loopSound in _loopingSounds)
			{
				if(loopSound.clip.name == source)
				{
					_loopingSounds.Remove(loopSound);
					Destroy(loopSound);
					return;
				}
			}
		}
		public virtual void MuteMixer(bool setMute)
        {
			switch(setMute)
			{
				case true:
					mainMixer.SetFloat("MasterVolume", -80);
					break;
				case false:
					mainMixer.SetFloat("MasterVolume",0);
					break;
			}
        }
		public virtual void OnMMEvent(MMGameEvent gameEvent)
		{
			if (MuteSfxOnPause)
			{
				switch (gameEvent.EventName)
				{
					case "PauseOn":
						PlayLoop(false);
						break;
					case "PauseGenericOn":
						PlayLoop(false);
						break;
					case "PauseOff":
						PlayLoop(true);
						break;
					case "GameStart":
						PlayLoop(true);
						break;
					case "GameOver":
						ClearLoop();
						break;
				}
			}
		}
		public virtual void PlayLoop(bool state)
		{
			foreach (AudioSource aS in _loopingSounds)
			{
				if(aS != null)
				if (state)
					aS.Play();
				else
					aS.Stop();
			}
		}

		protected virtual void SetMusic(bool status)
		{
			Settings.MusicOn = status;
			SaveSoundSettings ();
		}

		protected virtual void SetSfx(bool status)
		{
			Settings.SfxOn = status;
			SaveSoundSettings ();
		}

		public virtual void MusicOn() { SetMusic (true); }
		public virtual void MusicOff() { SetMusic (false); }
		public virtual void SfxOn() { SetSfx (true); }
		public virtual void SfxOff() { SetSfx (false); }

		public virtual void SaveSoundSettings()
		{
			MMSaveLoadManager.Save(Settings, _saveFileName, _saveFolderName);
		}

		protected virtual void LoadSoundSettings()
		{
			SoundSettings settings = (SoundSettings)MMSaveLoadManager.Load(typeof(SoundSettings), _saveFileName, _saveFolderName);
			if (settings != null)
			{
				Settings = settings;
			}
			else
			{
				Settings.MusicLevel = 0.5f;
				Settings.SfxLevel = 0.5f;
			}
		}

		public virtual AudioSource GetBackgroundMusic()
		{
			return _backgroundMusic;
		}

		protected virtual void ResetSoundSettings()
		{
			MMSaveLoadManager.DeleteSave(_saveFileName, _saveFolderName);
		}

		public virtual void OnMMSfxEvent(AudioClip clipToPlay, AudioMixerGroup audioGroup = null, float volume = 1f, float pitch = 1f)
		{
			PlaySound(clipToPlay, this.transform.position);
		}

		protected virtual void MuteAllSfx()
		{
			foreach(AudioSource source in _loopingSounds)
			{
				if (source != null)
				{
					source.mute = true;	
				}
			}
		}

		protected virtual void UnmuteAllSfx()
		{
			foreach(AudioSource source in _loopingSounds)
			{
				if (source != null)
				{
					source.mute = false;	
				}
			}
		}

		public virtual void ClearLoop()
		{
			foreach (AudioSource loopSound in _loopingSounds)
			{
					Destroy(loopSound);
			}
			_loopingSounds.Clear();
		}

		protected virtual void OnEnable()
		{
			IRESfxEvent.Register(OnMMSfxEvent);
			this.MMEventStartListening<MMGameEvent>();
			LoadSoundSettings ();

			_loopingSounds = new List<AudioSource> ();
		}

		protected virtual void OnDisable()
		{
			if (_enabled)
			{
				IRESfxEvent.Unregister(OnMMSfxEvent);
			}
			this.MMEventStopListening<MMGameEvent>();
		}
	}
}