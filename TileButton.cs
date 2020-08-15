using System.Drawing;
using System.Windows.Forms;

namespace AzulApp
{
    class TileButton : Button
    {
        private Color? color;

        public TileButton(Color? c = null) : base()
        {
            TileColor = c;
            BackColor = System.Drawing.Color.FromArgb(0);
            BackgroundImageLayout = ImageLayout.Zoom;
            FlatAppearance.BorderSize = 0;
            FlatStyle = FlatStyle.Flat;
            MinimumSize = Size = BaseSize;
            UseVisualStyleBackColor = false;
            TabStop = false;
        }

        protected override bool ShowFocusCues => false;

        public void ResetSize()
        {
            MinimumSize = Size = BaseSize;
        }

        public Color? TileColor
        {
            get
            {
                return color;
            }

            set
            {
                color = value;
                BackgroundImage = color == null ? null : ((Color)color).getImage();
                Enabled = (color != Color.WHITE && color != null);
            }
        }

        private static readonly Size BaseSize = new Size(60, 60);
    }
}
