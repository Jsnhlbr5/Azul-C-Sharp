using System;
using System.Drawing;
using System.Windows.Forms;

namespace AzulApp
{
    public partial class PlayerBoard : Form
    {
        private readonly bool[,] wall;
        private readonly BuildRow[] buildRows;
        private readonly TileCollection floorLine;
        private readonly Button[] buildRowButtons;
        private int score;

        private TileCollection selectedTiles;
        /**
         * The name for this player
         */
        internal readonly string player;

        private readonly Game model;
        public PlayerBoard(Game m, string name)
        {
            model = m;
            player = name;
            wall = new bool[5, 5];
            score = 0;
            buildRows = new BuildRow[5];
            for (int i = 0; i < 5; ++i)
            {
                buildRows[i] = new BuildRow(this, i);
            }
            floorLine = new TileCollection();
            InitializeComponent();
            SuspendLayout();
            Icon = Properties.Resources.icon;
            Text = name;
            BackgroundImage = Properties.Resources.PlayerBoard;
            //TODO controls setup

            // 5 build rows plus floor line ( TODO onClick events )
            buildRowButtons = new Button[6];
            for (int i = 0; i < 5; ++i)
            {
                buildRowButtons[i] = new Button()
                {
                    UseVisualStyleBackColor = false,
                    FlatStyle = FlatStyle.Flat,
                    BackColor = System.Drawing.Color.FromArgb(0)
                };
                buildRowButtons[i].FlatAppearance.BorderSize = 0;
                buildRowButtons[i].SetBounds(BUILD_ROW_X_POS + (BUILD_ROW_X_OFFSET * i),
                        BUILD_ROW_Y_POS + (BUILD_ROW_Y_OFFSET * i), 60 + (BUILD_ROW_X_OFFSET * -i), 60);
                Controls.Add(buildRowButtons[i]);
                buildRowButtons[i].BringToFront();
                buildRowButtons[i].Tag = i;
                buildRowButtons[i].Click += new EventHandler(addTilesToRow);
            }
            buildRowButtons[5] = new Button()
            {
                UseVisualStyleBackColor = false,
                FlatStyle = FlatStyle.Flat,
                BackColor = System.Drawing.Color.FromArgb(0)
            };
            buildRowButtons[5].FlatAppearance.BorderSize = 0;
            buildRowButtons[5].SetBounds(FLOOR_X_POS, FLOOR_Y_POS, 60 + (FLOOR_X_OFFSET * 6), 60);
            Controls.Add(buildRowButtons[5]);
            buildRowButtons[5].BringToFront();
            buildRowButtons[5].Tag = 5;
            buildRowButtons[5].Click += new EventHandler(addTilesToRow);
            updateButtons();
            ResumeLayout();
        }

        /**
         * Updates build row, floor line, and wall tiles to match logical model.
         */
        public void updateTiles()
        {
            // Remove old tiles (TODO)
            foreach (Control t in Controls.Find("buildTile", false))
            {
                Controls.Remove(t);
            }

            TileCollection tc;
            Bitmap tileImage;
            Label tile;
            int count;
            for (int r = 0; r < 5; ++r)
            {
                tc = getBuildRowTiles(r);
                count = tc.Count;
                if (count > 0)
                {
                    // Build rows with a count > 0 have a (1) color by definition
                    tileImage = ((Color)tc.getColor()).getImage();
                    for (int c = 0; c < count; ++c)
                    {
                        tile = new Label { Image = tileImage };
                        tile.Name = "buildTile";
                        tile.SetBounds(BUILD_ROW_X_POS + (BUILD_ROW_X_OFFSET * c),
                                BUILD_ROW_Y_POS + (BUILD_ROW_Y_OFFSET * r), 60, 60);
                        Controls.Add(tile);
                        tile.BringToFront();
                    }
                }
            }

            tc = floorLine;
            count = Math.Min(tc.Count, 7);
            for (int i = 0; i < count; ++i)
            {
                tileImage = tc[i].getImage();
                tile = new Label { Image = tileImage };
                tile.Name = "buildTile";
                tile.SetBounds(FLOOR_X_POS + (FLOOR_X_OFFSET * i), FLOOR_Y_POS, 60, 60);
                Controls.Add(tile);
                tile.BringToFront();
            }

            for (int r = 0; r < 5; ++r)
            {
                for (int c = 0; c < 5; ++c)
                {
                    if (wall[r, c])
                    {
                        tileImage = getColorForWallPos(r, c).getImage();
                        tile = new Label { Image = tileImage };
                        tile.SetBounds(WALL_ROW_X_POS + (WALL_ROW_X_OFFSET * c), WALL_ROW_Y_POS + (WALL_ROW_Y_OFFSET * r),
                                60, 60);
                        Controls.Add(tile);
                        tile.BringToFront();
                    }
                }
            }
        }

        /**
         * Updates the score indicator to match the player's current score.
         */
        public void updateScore()
        {
            scoreMarker.SetBounds(SCORE_X_POS + (int)Math.Round(SCORE_X_OFFSET * Math.Max(score % 20 - 1, 0)),
                    SCORE_Y_POS + SCORE_Y_OFFSETS[(score + 19) / 20], 30, 30);
            scoreMarker.Text = score.ToString();
        }

        /**
         * Updates build row and floor line buttons based on state.
         */
        public void updateButtons()
        {
            if (!hasSelectedTiles())
            {
                for (int i = 0; i < 6; ++i)
                {
                    buildRowButtons[i].Enabled = false;
                }
            }
            else
            {
                for (int i = 0; i < 5; ++i)
                {
                    buildRowButtons[i].Enabled = canAddTilesToRow(i);
                }
                buildRowButtons[5].Enabled = true;
            }
        }

        public void updateTitle(bool currentPlayer)
        {
            if (currentPlayer)
                Text = player + " (Active Player)";
            else
                Text = player;
        }

        /**
         * Used to determine tile color for wall positions
         *
         * @param row
         *            row number
         * @param col
         *            column number
         * @return the color name
         */
        private Color getColorForWallPos(int row, int col)
        {
            // plus 5 because we want it to always be positive
            return (Color)((col - row + 5) % 5);
        }

        // ---- Get/check state methods ----

        /**
         * Returns a TileCollection representing the tiles currently on the given build row
         *
         * @param row
         *            the build row
         * @return a TileCollection representing the tiles currently on the given build row
         */
        public TileCollection getBuildRowTiles(int row)
        {
            TileCollection tc = new TileCollection();
            BuildRow br = buildRows[row];
            tc.addTiles(br.color, br.count);
            return tc;
        }

        public bool canAddTilesToRow(int row)
        {
            Color? c = selectedTiles.getColorIgnoreWhite();
            if (c == null)
                throw new ArgumentException("Selected tiles are mixed colors or empty set.  This indicates a logic error (bug).");
            return buildRows[row].canAddTiles((Color)c);
        }

        /**
         * Returns true if this player has at least one row of their wall completed
         *
         * @return true if this player has at least one row of their wall completed
         */
        public bool hasCompleteRow()
        {
            bool complete;
            for (int row = 0; row < 5; ++row)
            {
                complete = true;
                for (int col = 0; col < 5; ++col)
                {
                    if (!wall[row, col])
                    {
                        // This row is not complete, check the next one.
                        complete = false;
                        break;
                    }
                }
                if (complete)
                    return true;
            }
            return false;
        }

        /**
         * Returns true if this player currently has selected tiles that they have not placed on their board
         *
         * @return true if this player currently has selected tiles that they have not placed on their board
         */
        public bool hasSelectedTiles()
        {
            return selectedTiles != null;
        }

        // ---- Mutator methods ----

        /**
         * Sets this player's selected tiles buffer to the given TileCollection
         *
         * @param tc
         *            the TileCollection representing the tiles this player selected
         */
        public void setSelectedTiles(TileCollection tc)
        {
            selectedTiles = tc;
            updateButtons();
        }

        /**
         * Adds tiles (from the selectedTiles buffer) to the given build row, overflowing to the floor line
         *
         * @param row
         *            the 0-indexed row number (>4 places tiles directly on the floor line)
         */
        public void addTilesToRow(object sender, EventArgs e)//(int row)
        {
            if (selectedTiles == null)
                throw new InvalidOperationException("Tiles must be selected before they can be added to a row.");
            int row = (int)((Button)sender).Tag;
            if (row > 4)
            {
                floorLine.AddRange(selectedTiles);
            }
            else
            {
                if (selectedTiles.Contains(Color.WHITE))
                {
                    floorLine.AddRange(selectedTiles.removeTilesOfColor(Color.WHITE));
                }
                floorLine.AddRange(buildRows[row].addTiles(selectedTiles));
            }
            selectedTiles = null;
            updateButtons();
            updateTiles();
            model.endTurn();
        }

        /**
         * Invokes tileRow() for each build row and scoreFloor(), collecting the discard tiles into a single collection, and
         * triggers UI updates
         *
         * @return a TileCollection representing all of the tiles discarded by this player
         */
        public TileCollection finishRound()
        {
            TileCollection discard = new TileCollection();
            for (int i = 0; i < 5; ++i)
            {
                discard.AddRange(tileRow(i));
            }
            discard.AddRange(scoreFloor());
            updateTiles();
            updateScore();
            return discard;
        }

        /**
         * Calculates end-of-game bonuses and returns this player's final score
         *
         * @return this player's final score
         */
        public int finishGame()
        {
            rowBonus();
            colBonus();
            colorBonus();
            updateScore();
            return score;
        }

        // ---- Private methods ----

        /**
         * Tiles a build row onto the wall (if it's complete) and returns the discarded tiles from doing so
         *
         * @param row
         *            the row index to evaluate
         * @return any discarded tiles
         */
        private TileCollection tileRow(int row)
        {
            BuildRow br = buildRows[row];
            if (br.isFull())
            {
                wall[row, br.column()] = true;
                scoreTile(row, br.column());
                return br.getDiscard();
            }
            return new TileCollection();
        }

        /**
         * Calculates the score for a single tile, by counting the contiguous row and/or column that it is part of
         *
         * @param row
         *            the row of the tile to score
         * @param col
         *            the column of the tile to score
         */
        private void scoreTile(int row, int col)
        {
            bool inARow = ((col + 1 < 5) && wall[row, col + 1]) || ((col - 1 > -1) && wall[row, col - 1]);
            bool inACol = ((row + 1 < 5) && wall[row + 1, col]) || ((row - 1 > -1) && wall[row - 1, col]);
            if (!inARow && !inACol)
            {
                ++score;
            }
            else
            {
                if (inARow)
                {
                    int rowLen = 1;
                    int i = col;
                    while (++i < 5 && wall[row, i])
                        ++rowLen;
                    i = col;
                    while (--i > -1 && wall[row, i])
                        ++rowLen;
                    score += rowLen;
                }
                if (inACol)
                {
                    int colLen = 1;
                    int i = row;
                    while (++i < 5 && wall[i, col])
                        ++colLen;
                    i = row;
                    while (--i > -1 && wall[i, col])
                        ++colLen;
                    score += colLen;
                }
            }
        }

        /**
         * Calculates the score penalty based on the floor line and returns all of the tiles that were placed there
         *
         * @return a TileCollection representing all the tiles discarded from the floor line
         */
        private TileCollection scoreFloor()
        {
            int numTiles = floorLine.Count;
            if (numTiles > 7)
                numTiles = 7;
            score += floorLineScores[numTiles];
            if (score < 0)
                score = 0;
            TileCollection discard = new TileCollection(floorLine);
            floorLine.Clear();
            return discard;
        }

        /**
         * Adds 2 to this player's score for every complete row
         */
        private void rowBonus()
        {
            bool complete;
            for (int row = 0; row < 5; ++row)
            {
                complete = true;
                for (int col = 0; col < 5; ++col)
                {
                    if (!wall[row, col])
                    {
                        // This row is not complete, check the next one.
                        complete = false;
                        break;
                    }
                }
                if (complete)
                    score += 2;
            }
        }

        /**
         * Adds 7 to this player's score for every complete column
         */
        private void colBonus()
        {
            bool complete;
            for (int col = 0; col < 5; ++col)
            {
                complete = true;
                for (int row = 0; row < 5; ++row)
                {
                    if (!wall[row, col])
                    {
                        // This column is not complete, check the next one.
                        complete = false;
                        break;
                    }
                }
                if (complete)
                    score += 7;
            }
        }

        /**
         * Adds 10 to this player's score for every complete color (all 5 tiles of one color)
         */
        private void colorBonus()
        {
            bool complete;
            for (int color = 0; color < 5; ++color)
            {
                complete = true;
                for (int row = 0; row < 5; ++row)
                {
                    if (!wall[row, (row + color) % 5])
                    {
                        // This color is not complete, check the next one.
                        complete = false;
                        break;
                    }
                }
                if (complete)
                    score += 10;
            }
        }

        /**
     * A representation of a single build row (to centralize some of the related logic)
     *
     * @author jsnhlbr5
     */
        private class BuildRow
        {
            // The row this BuildRow represents (0-indexed)
            private readonly int row;
            private readonly PlayerBoard o_;

            protected internal Color? color;
            protected internal int count;

            public BuildRow(PlayerBoard o, int number)
            {
                o_ = o;
                row = number;
                color = null;
                count = 0;
            }

            public bool isFull()
            {
                // The row number is 0-indexed, so +1
                return count == row + 1;
            }

            /**
             * Checks if tiles of the given color can be added to this build row
             *
             * @param color
             *            the color of tiles to be checked
             * @return true if one or more tiles of the given Color can be added, false otherwise
             */
            public bool canAddTiles(Color color)
            {
                // Not already tiled in this row, matching existing tiles (if any), and there is space left
                return !o_.wall[row, column(color)] && (this.color == null || this.color == color) && !isFull();
            }

            /**
             * Adds the given tiles to this row, returning any overflow
             *
             * @param tc
             *            the set of tiles to add
             * @return a TileCollection representing the overflow tiles (is an empty collection if no overflow)
             */
            public TileCollection addTiles(TileCollection tc)
            {
                if (!tc.isAllOneColor())
                    throw new ArgumentException("Invalid tile collection for build row: not all one color");
                if (tc.getColor() != color && color != null) // Shouldn't happen, check canAddTiles() first
                    return tc;
                color = tc.getColor();
                count += tc.Count;
                int overflow = Math.Max(count - (row + 1), 0); // Can't have negative overflow
                count -= overflow; // 'overflow' extra tiles were added; remove them
                return tc.drawTiles(overflow); // Simplest way to get the appropriate TileCollection
            }

            /**
             * Gets the discard tiles from tiling this row
             *
             * @return the discard tiles from tiling this row
             */
            public TileCollection getDiscard()
            {
                TileCollection discard = new TileCollection();
                // Equal to the row number because one is kept for the wall
                discard.addTiles(color, row);
                count = 0;
                color = null;
                return discard;
            }

            /**
             * Returns the column this row will tile to, based on its current color
             *
             * @return the column this row will tile to, based on its current color
             */
            public int column()
            {
                if (color == null)
                    return -1;
                return column((Color)color);
            }

            /**
             * Returns the column for this row and the given color
             *
             * @param color
             *            the color to determine the column for
             * @return the column for this row and the given color
             */
            private int column(Color color)
            {
                return (row + (int)color) % 5;
            }
        }

        /**
         * The total penalty for having the given quantity of tiles on your floor line (0 tiles = 0 penalty).
         */
        private static readonly int[] floorLineScores = { 0, -1, -2, -4, -6, -8, -11, -14 };

        private static readonly int BUILD_ROW_X_POS = 298;
        private static readonly int BUILD_ROW_Y_POS = 264;
        private static readonly int BUILD_ROW_X_OFFSET = -66;
        private static readonly int BUILD_ROW_Y_OFFSET = 66;

        private static readonly int WALL_ROW_X_POS = 393;
        private static readonly int WALL_ROW_Y_POS = 265;
        private static readonly int WALL_ROW_X_OFFSET = 66;
        private static readonly int WALL_ROW_Y_OFFSET = 65;

        private static readonly int FLOOR_X_POS = 33;
        private static readonly int FLOOR_Y_POS = 645;
        private static readonly int FLOOR_X_OFFSET = 72;

        private static readonly int SCORE_X_POS = 34;
        private static readonly int SCORE_Y_POS = 0;
        private static readonly double SCORE_X_OFFSET = 34.35;
        private static readonly int[] SCORE_Y_OFFSETS = { 0, 40, 80, 120, 164, 208 };

        private void PlayerBoard_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (model.Enabled)
            {
                e.Cancel = true;
            }
            else
            {
                // message?
            }
        }
    }
}
