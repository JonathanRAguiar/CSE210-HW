using System.Drawing;
using System.Windows.Forms;

namespace hash.Components
{
	public partial class LineSeparator : UserControl
	{
		public LineSeparator()
		{
			InitializeComponent();

			Paint += new PaintEventHandler(LinePaint);

			MaximumSize = new Size(2000, 2);

			MinimumSize = new Size(0, 2);

			Width = 200;
		}

		private void LinePaint(object sender, PaintEventArgs e)
		{
			Graphics paintGraphics = e.Graphics;

			paintGraphics.DrawLine(Pens.DarkGray, new Point(0, 0), new Point(Width, 0));

			paintGraphics.DrawLine(Pens.White, new Point(0, 1), new Point(Width, 1));
		}
	}
}