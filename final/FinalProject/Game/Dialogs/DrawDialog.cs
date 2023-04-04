using System.Windows.Forms;

namespace hash.Game.Dialogs
{
	class DrawDialog
	{
		public static void Create()
		{
			MessageBoxManager.OK = "CONTINUE";

			MessageBoxManager.Register();

			MessageBox.Show
			(
				"          WE GOT DRAW!          ",
				"DRAW", MessageBoxButtons.OK, MessageBoxIcon.Information
			);

			MessageBoxManager.Unregister();
		}
	}
}