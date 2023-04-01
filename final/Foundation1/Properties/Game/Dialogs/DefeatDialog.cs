using System.Windows.Forms;

namespace hash.Game.Dialogs
{
	class DefeatDialog
	{
		public static void Create()
		{
			MessageBoxManager.OK = "SO SAD... :(";

			MessageBoxManager.Register();

			MessageBox.Show
			(
				"          YOU LOSE!!          ",
				"DEFEATED", MessageBoxButtons.OK, MessageBoxIcon.Information
			);

			MessageBoxManager.Unregister();
		}
	}
}