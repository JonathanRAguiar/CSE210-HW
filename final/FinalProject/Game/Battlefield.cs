using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

using hash.Modules;
using hash.Interfaces;
using hash.Game.Dialogs;

namespace hash.Game
{
    public partial class Battlefield : Form, IOnAudioChange
    {
	
		private bool gameStarted;
		private String playerSymbol;
        private String IAsymbol;
		private Control[,] battleMatrix;


		
		private bool audioPaused;
		private AudioPlayer audioPlayer;
		private AudioPlayer audioPlayerTemp;




		
		public Battlefield()
		{
			InitializeComponent();

			OnPlayerStart();

			gameStarted = false;

			battleMatrix = new Control[3, 3];
		}




		
		private void btnStart_Click(object sender, EventArgs e)
		{
			if (!gameStarted)
			{
				OnSymbolSelect();

				btnStart.Text = "Reset";

				gameStarted = true;
			}
			else
			{
				OnGameReset();
			}
		}

		
		private void btnEsqSup_Click(object sender, EventArgs e)
		{
			OnButtonClick(btnEsqSup, 0, 0);
		}

		
		private void btnMidSup_Click(object sender, EventArgs e)
		{
			OnButtonClick(btnMidSup, 0 ,1);
		}

		
		private void btnDirSup_Click(object sender, EventArgs e)
		{
			OnButtonClick(btnDirSup, 0, 2);
		}

	
		private void btnEsqMid_Click(object sender, EventArgs e)
		{
			OnButtonClick(btnEsqMid, 1, 0);
		}

		
		private void btnMidMid_Click(object sender, EventArgs e)
		{
			OnButtonClick(btnMidMid, 1, 1);
		}

		
		private void btnDirMid_Click(object sender, EventArgs e)
		{
			OnButtonClick(btnDirMid, 1, 2);
		}

		
		private void btnEsqInf_Click(object sender, EventArgs e)
		{
			OnButtonClick(btnEsqInf, 2, 0);
		}

		
		private void btnMidInf_Click(object sender, EventArgs e)
		{
			OnButtonClick(btnMidInf, 2, 1);
		}

		
		private void btnDirInf_Click(object sender, EventArgs e)
		{
			OnButtonClick(btnDirInf, 2, 2);
		}




	
		private void OnGameStart(bool enabled, params Button[] gameButtons)
		{
            int bt = 0;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
					if (enabled)
					{
						battleMatrix[i, j] = gameButtons[bt];
						battleMatrix[i, j].Text = "";
						battleMatrix[i, j].Enabled = true;
					}
					else
					{
						battleMatrix[i, j].Enabled = false;
					}

                    bt++;
                }
            } 
		}

		private void OnSymbolSelect()
		{
			if (SymbolSelectDialog.Create() == DialogResult.Yes)
			{
				playerSymbol = "X";
                IAsymbol = "O";
			}
			else
			{
				playerSymbol = "O";
                IAsymbol = "X";
			}

			OnGameStart(true, btnEsqSup, btnMidSup, btnDirSup, btnEsqMid, btnMidMid, btnDirMid, btnEsqInf, btnMidInf, btnDirInf);
		}

		private void OnGameReset()
		{
			if (GameResetDialog.Create() == DialogResult.Yes)
			{
				OnSymbolSelect();
			}
			else
			{
				OnGameStart(false, btnEsqSup, btnMidSup, btnDirSup, btnEsqMid, btnMidMid, btnDirMid, btnEsqInf, btnMidInf, btnDirInf);
			}

			OnPlayerResumed();
		}

		private void OnGameTerminated(bool isVictory)
		{
			if (File.Exists("sounds/victory.mp3") || File.Exists("sounds/defeat.mp3"))
			{
				audioPlayer.Pause();

				audioPlayerTemp = new AudioPlayer();

				audioPlayerTemp.SetVolume(MediaPlayer_TrackBar_Volume.Value);

				if (isVictory)
				{
					audioPlayerTemp.Stream("sounds/victory.mp3");
				}
				else
				{
					audioPlayerTemp.Stream("sounds/defeat.mp3");
				}
			}
		}

		private void OnButtonClick(Button button, int posX, int posY)
		{
            if (button.Enabled)
			{
				button.Text = playerSymbol;
				
                battleMatrix[posX, posY] = button;

				button.Enabled = false;
			}

            
            
            
            if (GameLogic.CheckVictory(battleMatrix, playerSymbol))
            {
				Label_Victories_Value.Text = (int.Parse(Label_Victories_Value.Text) + 1).ToString();

				OnGameTerminated(true);

				VictoryDialog.Create();

				OnGameReset();

				return;
            }

            GameLogic.EnemyMovement(IAsymbol, battleMatrix);
            
            
            if (GameLogic.CheckVictory(battleMatrix, IAsymbol))
            {
				Label_Defeats_Value.Text = (int.Parse(Label_Defeats_Value.Text) + 1).ToString();

				OnGameTerminated(false);

				DefeatDialog.Create();

				OnGameReset();

				return;
            }
        }




		
		private void MediaPlayer_Button_PlayPause_Click(object sender, EventArgs e)
		{
			if (audioPaused)
			{
				audioPlayer.Play();

				MediaPlayer_Button_PlayPause.Text = "❚❚";

				audioPaused = false;
			}
			else
			{
				audioPlayer.Pause();

				MediaPlayer_Button_PlayPause.Text = "▶";

				audioPaused = true;
			}
		}

		private void MediaPlayer_Button_NextMusic_Click(object sender, EventArgs e)
		{
			audioPlayer.Next();

			MediaPlayer_Button_PlayPause.Text = "❚❚";
		}

		private void MediaPlayer_Button_LastMusic_Click(object sender, EventArgs e)
		{
			audioPlayer.Back();

			MediaPlayer_Button_PlayPause.Text = "❚❚";
		}

		private void MediaPlayer_TrackBar_Volume_Scroll(object sender, EventArgs e)
		{
			OnVolumeChange();
		}

		private void MediaPlayer_TrackBar_AudioTime_Scroll(object sender, EventArgs e)
		{
			audioPlayer.SetTime(MediaPlayer_TrackBar_AudioTime.Value);
		}

		private void MediaPlayer_TrackBar_AudioTime_MouseDown(object sender, MouseEventArgs e)
		{
			audioPlayer.SetTime(Convert.ToInt32(((double)e.X / (double)MediaPlayer_TrackBar_AudioTime.Width) * MediaPlayer_TrackBar_AudioTime.Maximum));
		}

		private void MediaPlayer_Timer_AudioTime_Tick(object sender, EventArgs e)
		{
			MediaPlayer_Label_AudioTime.Text = String.Format("{0} / {1}", audioPlayer.GetAudioCurrentTime().ToString("mm\\:ss"), audioPlayer.GetAudioTime().ToString("mm\\:ss"));

			MediaPlayer_TrackBar_AudioTime.Value = Math.Min(MediaPlayer_TrackBar_AudioTime.Maximum, (int)(100 * audioPlayer.GetAudioCurrentTime().TotalSeconds / audioPlayer.GetAudioTime().TotalSeconds));
		}




		
		private void OnPlayerStart()
		{
			try
			{
				if (!Directory.Exists("Music/"))
				{
					Directory.CreateDirectory("Music/");
				}

				audioPlayer = new AudioPlayer(this, Directory.GetFiles("Music/").ToList());

				if (!audioPlayer.Stream())
				{
					MediaPlayer_Label_AudioName.Text = "Nenhuma música encontrada na pasta 'Music'. Você pode adicionar músicas nessa pasta para escutar durante o jogo.";
				}
				else
				{
					OnAudioTimeChanged.Start();

					OnVolumeChange();
				}
			}
			catch (IOException e)
			{
				Console.WriteLine(e);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		private void OnPlayerResumed()
		{
			if (audioPlayerTemp != null)
			{
				audioPlayerTemp.Stop();
			}

			if (!audioPaused)
			{
				audioPlayer.Play();
			}
		}

		private void OnVolumeChange()
		{
			audioPlayer.SetVolume(MediaPlayer_TrackBar_Volume.Value);

			if (MediaPlayer_TrackBar_Volume.Value > 0)
			{
				MediaPlayer_Label_VolumeValue.Text = String.Format("{0}%", MediaPlayer_TrackBar_Volume.Value);
			}
			else
			{
				MediaPlayer_Label_VolumeValue.Text = "Muted";
			}
		}




		
		void IOnAudioChange.AudioChanged()
		{
			MediaPlayer_Label_AudioName.Text = audioPlayer.GetAudioName();
		}
	}
}