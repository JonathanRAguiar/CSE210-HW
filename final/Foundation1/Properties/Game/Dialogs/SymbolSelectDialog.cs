using System.Windows.Forms;

namespace hash.Game.Dialogs
{
	class SymbolSelectDialog
	{
		public static DialogResult Create()
		{
			MessageBoxManager.No = "O";
			MessageBoxManager.Yes = "X";

			MessageBoxManager.Register();

			DialogResult symbolDialog = MessageBox.Show
			(
				"SELECT X OR O",
				"HASH", MessageBoxButtons.YesNo, MessageBoxIcon.Information
			);

			MessageBoxManager.Unregister();

			return symbolDialog;
		}
	}
} 