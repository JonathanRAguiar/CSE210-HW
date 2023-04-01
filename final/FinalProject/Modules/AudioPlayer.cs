using System;
using System.IO;
using NAudio.Wave;
using hash.Interfaces;
using System.Collections.Generic;

namespace hash.Modules
{
	class AudioPlayer
	{
		
		private WaveStream waveStream;
		private IWavePlayer audioPlayer;
		private List<string> audioPlaylist;
		private IOnAudioChange onAudioChange;


		
		private int playlistPosition = -1;
		private bool playlistManualState = false;




		
		public AudioPlayer()
		{

		}

		
		public AudioPlayer(IOnAudioChange onAudioChange, List<string> audioPlaylist)
		{
			this.onAudioChange = onAudioChange;

			this.audioPlaylist = new List<string>(audioPlaylist);
		}




		public bool Stream()
		{
			if (waveStream != null)
			{
				waveStream.Dispose();
			}

			if (audioPlayer != null)
			{
				audioPlayer.Dispose();
			}

			if (audioPlaylist.Count < 1)
			{
				return false;
			}

			if (!playlistManualState)
			{
				if (playlistPosition != audioPlaylist.Count - 1)
				{
					playlistPosition++;
				}
				else
				{
					playlistPosition = 0;
				}
			}
			else
			{
				playlistManualState = false;
			}

			audioPlayer = new WaveOutEvent();
			waveStream = new AudioFileReader(audioPlaylist[playlistPosition]);
			audioPlayer.PlaybackStopped += (sender, evn) => { Stream(); };

			audioPlayer.Init(waveStream);
			audioPlayer.Play();
			onAudioChange.AudioChanged();

			return true;
		}

		public void Stream(string sound)
		{
			audioPlayer = new WaveOutEvent();
			waveStream = new AudioFileReader(sound);

			audioPlayer.PlaybackStopped += (sender, evn) =>
			{
				waveStream.Dispose();
				audioPlayer.Dispose();
			};

			audioPlayer.Init(waveStream);
			audioPlayer.Play();
		}

		public void Play()
		{
			if (audioPlayer != null)
			{
				audioPlayer.Play();
			}
		}

		public void Pause()
		{
			if (audioPlayer != null)
			{
				audioPlayer.Pause();
			}
		}

		public void Stop()
		{
			if (audioPlayer != null)
			{
				audioPlayer.Stop();
			}
		}

		public void SetVolume(int volume)
		{
			if (audioPlayer != null)
			{
				audioPlayer.Volume = volume / 100f;
			}
		}

		public void SetTime(int time)
		{
			if (audioPlayer != null)
			{
				waveStream.CurrentTime = TimeSpan.FromSeconds((waveStream.TotalTime.TotalSeconds * time) / 100);
			}
		}

		public void Next()
		{
			if (audioPlayer != null)
			{
				if (playlistPosition != audioPlaylist.Count - 1)
				{
					playlistPosition++;
				}
				else
				{
					playlistPosition = 0;
				}

				audioPlayer.Stop();

				playlistManualState = true;
			}
		}

		public void Back()
		{
			if (audioPlayer != null)
			{
				if (playlistPosition != 0)
				{
					playlistPosition--;
				}
				else
				{
					playlistPosition = audioPlaylist.Count - 1;
				}

				audioPlayer.Stop();

				playlistManualState = true;
			}
		}

		public string GetAudioName()
		{
			if (audioPlayer != null)
			{
				return Path.GetFileNameWithoutExtension(audioPlaylist[playlistPosition]);
			}

			return null;
		}

		public PlaybackState GetAudioState()
		{
			return audioPlayer.PlaybackState;
		}

		public TimeSpan GetAudioTime()
		{
			if (audioPlayer != null)
			{
				
				return waveStream.TotalTime;
			}

			return TimeSpan.Zero;
		}

		public TimeSpan GetAudioCurrentTime()
		{
			if (audioPlayer != null)
			{
				
				return waveStream.CurrentTime;
			}

			return TimeSpan.Zero;
		}
	}
}