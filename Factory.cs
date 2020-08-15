using System;
using System.Drawing;
using System.Windows.Forms;

namespace AzulApp
{
    class Factory : Panel
    {
        private readonly TileButton[] buttons;
        private readonly Game model;

        public int Index { get; }

        public Factory(Game m, int ndx)
        {
            model = m;
            Index = ndx;
            SuspendLayout();

            BackgroundImageLayout = ImageLayout.Zoom;
            BackgroundImage = Properties.Resources.factory;
            Name = "Factory" + Index;
            Size = new Size(240, 240);

            buttons = new TileButton[4];

            for (int i = 0; i < 4; ++i)
            {
                buttons[i] = new TileButton();
                Controls.Add(buttons[i]);

                int x = (i % 2 == 0) ? FIRST_POSITION : SECOND_POSITION;
                int y = (i < 2) ? FIRST_POSITION : SECOND_POSITION;
                buttons[i].Location = new Point(x, y);

                buttons[i].Click += new EventHandler(model.PickTiles);
            }

            ResumeLayout();
        }

        /**
         * Removes old tiles (if any) and creates new ones; updates buttons to match.
         */
        public void UpdateTiles()
        {
            SuspendLayout();
            // Disable buttons
            foreach (TileButton b in buttons)
            {
                b.Enabled = false;
                b.TileColor = null;
            }

            // Get new tiles and enable buttons (as appropriate)
            TileCollection tc = model.getFactoryTiles(Index);
            for (int i = 0; i < tc.Count; ++i)
            {
                buttons[i].TileColor = tc[i];
            }
            ResumeLayout();
        }

        private static readonly int FIRST_POSITION = 57, SECOND_POSITION = 123;
    }
}
