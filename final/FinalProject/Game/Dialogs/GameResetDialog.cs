using System.Windows.Forms;

namespace hash.Game.Dialogs
{
	class GameResetDialog
	{
		public static DialogResult Create()
		{
			MessageBoxManager.No = "NO";
			MessageBoxManager.Yes = "YES";

			MessageBoxManager.Register();

			DialogResult resetDialog = MessageBox.Show
			(
				"START A NEW GAME?",
				"HASH", MessageBoxButtons.YesNo, MessageBoxIcon.Question
			);

			MessageBoxManager.Unregister();

			return resetDialog;
		}
	}
}