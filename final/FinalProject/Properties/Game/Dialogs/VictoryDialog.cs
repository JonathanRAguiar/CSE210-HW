using System.Windows.Forms;

namespace hash.Game.Dialogs
{
	class VictoryDialog
	{
		public static void Create()
		{
			MessageBoxManager.OK = "YEAH";

			MessageBoxManager.Register();

			MessageBox.Show
			(
				"          YOU WON!          ",
				"WIN", MessageBoxButtons.OK, MessageBoxIcon.Information
			);

			MessageBoxManager.Unregister();
		}
	}
} 