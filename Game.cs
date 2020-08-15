using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace AzulApp
{
    public partial class Game : Form
    {
        private readonly int numPlayers;
        private readonly Point[] myLocs;
        private readonly PlayerBoard[] playerBoards;
        private readonly TileCollection bag;
        private readonly TileCollection[] factories;
        private readonly Factory[] factoryViews;
        private readonly TileCollection centerArea;
        private readonly TileCollection boxLid;

        private int curPlayer;
        private string winner;
        private float oldSize;

        //private bool netGame;
        //private bool host;
        //HubConnection connection;

        public Game(int players, string[] names)//, HubConnection conn = null, bool host = false)
        {
            //netGame = (conn != null);
            //if (netGame)
            //{
            //    connection = conn;
            //}
            //this.host = host;

            locs[2] = loc5;
            locs[3] = loc7;
            locs[4] = loc9;

            InitializeComponent();
            SuspendLayout();
            ClientSize = new Size(1040, 1040);
            oldSize = BaseSize;
            FormClosing += OnFormClosing;

            if (players < 2 || players > 4)
                throw new ArgumentException("Invalid number of players, must be 2-4.");
            numPlayers = players;
            playerBoards = new PlayerBoard[numPlayers];
            if (names.Length < numPlayers)
                throw new ArgumentException("Not enough names given for the number of players");
            for (int i = 0; i < numPlayers; ++i)
            {
                playerBoards[i] = new PlayerBoard(this, names[i]);
            }

            bag = new TileCollection();
            bag.addTiles(Color.BLUE, 20);
            bag.addTiles(Color.YELLOW, 20);
            bag.addTiles(Color.RED, 20);
            bag.addTiles(Color.BLACK, 20);
            bag.addTiles(Color.TEAL, 20);

            myLocs = locs[numPlayers];
            factories = new TileCollection[myLocs.Length];
            factoryViews = new Factory[myLocs.Length];
            for (int i = 0; i < myLocs.Length; ++i)
            {
                factories[i] = new TileCollection();
                factoryViews[i] = new Factory(this, i) { Location = myLocs[i], TabIndex = i };
                Controls.Add(factoryViews[i]);
            }

            centerArea = new TileCollection();

            boxLid = new TileCollection();

            winner = "none";
            // Randomize first player
            curPlayer = (int)(Utils.rand() * numPlayers);
            playerBoards[curPlayer].updateTitle(true);
            for (int i = 0; i < numPlayers; ++i)
            {
                playerBoards[i].Show();
            }
            resetCenter();

            // Make common area window ~70% of screen height
            int height = (int)(Screen.PrimaryScreen.Bounds.Height*0.7);
            ClientSize = new Size(height, height);
            UpdateScale(height);

            ResumeLayout();
        }

        private void resetCenter()
        {
            for (int i = 0; i < factories.Length; ++i)
            {
                factories[i] = bag.drawTiles(4);
                if (factories[i].Count < 4)
                {
                    if (!boxLid.Empty)
                    {
                        bag.AddRange(boxLid);
                        boxLid.Clear();
                        factories[i].AddRange(bag.drawTiles(4 - factories[i].Count));
                    }
                    else
                    {
                        // No more tiles to draw, remaining factories will be empty.
                        break;
                    }
                }
            }
            centerArea.Add(Color.WHITE);
            UpdateTiles();
        }

        private void UpdateTiles()
        {
            foreach (Factory f in factoryViews)
            {
                f.UpdateTiles();
            }
            UpdateCenter();
        }

        private void UpdateCenter(bool tiles = true)
        {
            pnlCenterArea.SuspendLayout();
            // Sort tiles
            centerArea.Sort();
            // Calculate appropriate size (in tiles) based on tile count
            int size = (int)Math.Ceiling(Math.Sqrt(centerArea.Count));
            int pixels = 67 * size;
            pnlCenterArea.Size = new Size(pixels, pixels);

            if (tiles)
            {
                pnlCenterArea.Controls.Clear();

                // Create new tiles and buttons
                foreach (Color tile in centerArea)
                {
                    TileButton tileButton = new TileButton(tile);
                    tileButton.MinimumSize = tileButton.Size;
                    tileButton.Click += new EventHandler(PickTiles);
                    pnlCenterArea.Controls.Add(tileButton);
                }
            }

            int winSize = ClientSize.Width;
            pnlCenterArea.Scale(new SizeF(winSize / oldSize, winSize / oldSize));
            int loc = winSize / 2 - pnlCenterArea.Size.Width / 2;
            pnlCenterArea.Location = new Point(loc, loc);
            pnlCenterArea.ResumeLayout();
        }

        /**
         * Ends a player's turn. If the round is over (all tiles have been picked), performs end-of-round activities
         * (tiling, scoring, and discard). If the game is not over, resets the common area for the next round; otherwise
         * tallies final bonuses and declares the winner before exiting.
         */
        public void endTurn()
        {
            playerBoards[curPlayer].updateTitle(false);
            if (roundOver())
            {
                TileCollection discard;
                for (int i = 0; i < numPlayers; ++i)
                {
                    discard = playerBoards[i].finishRound();
                    if (discard.Contains(Color.WHITE))
                    {
                        curPlayer = i;
                        discard.removeTilesOfColor(Color.WHITE);
                    }
                    boxLid.AddRange(discard);
                }
                if (!gameOver())
                {
                    resetCenter();
                }
                else
                {
                    int score = -1;
                    winner = "";
                    foreach (PlayerBoard pb in playerBoards)
                    {
                        int s = pb.finishGame();
                        if (s > score)
                        {
                            score = s;
                            winner = pb.player;
                        }
                    }
                    DialogResult result = MessageBox.Show(winner + " wins!", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (result == DialogResult.OK)
                    {
                        Close();
                    }
                }
            }
            else
            {
                curPlayer = (curPlayer + 1) % numPlayers;
            }
            playerBoards[curPlayer].updateTitle(true);
        }

        internal void PickTiles(object sender, EventArgs e)
        {
            int factory = -1;
            TileButton btn = (TileButton)sender;
            if (btn.Parent.GetType() == typeof(Factory))
            {
                factory = ((Factory)btn.Parent).Index;
            }
            Color color = (Color)btn.TileColor;

            TileCollection picked;
            // TODO the actual move stuff
            if (factory < 0)
            {
                picked = centerArea.removeTilesOfColor(color);
                if (centerArea.Contains(Color.WHITE))
                    picked.AddRange(centerArea.removeTilesOfColor(Color.WHITE));
            }
            else
            {
                picked = factories[factory].removeTilesOfColor(color);
                centerArea.AddRange(factories[factory]);
                factories[factory].Clear();
            }
            UpdateTiles();
            playerBoards[curPlayer].setSelectedTiles(picked);

            //if (netGame)
            //{
            //    connection.InvokeAsync("SendPick", curPlayer, factory, color);
            //}
        }

        public TileCollection getFactoryTiles(int index)
        {
            return factories[index];
        }

        /**
         * Returns true if the round is over (all tiles have been drawn)
         *
         * @return true if the round is over (all tiles have been drawn)
         */
        private bool roundOver()
        {
            if (!centerArea.Empty)
                return false;
            for (int i = 0; i < factories.Length; ++i)
            {
                if (!factories[i].Empty)
                    return false;
            }
            return true;
        }

        /**
         * Returns true if the game is over (at least one player has at least one completed row)
         *
         * @return true if the game is over (at least one player has at least one completed row)
         */
        private bool gameOver()
        {
            for (int i = 0; i < numPlayers; ++i)
            {
                if (playerBoards[i].hasCompleteRow())
                    return true;
            }
            return false;
        }

        private void UpdateScale(int size)
        {
            SuspendLayout();
            float scale = size / oldSize;
            SizeF factor = new SizeF(scale, scale);
            for (int i = 0; i < myLocs.Length; ++i)
            {
                factoryViews[i].Scale(factor);
            }
            UpdateCenter(false);
            oldSize = size;
            ResumeLayout();
        }

        private void Game_ResizeEnd(object sender, EventArgs e)
        {
            int dim = Math.Max(Math.Min(ClientSize.Width, ClientSize.Height), 300);
            ClientSize = new Size(dim, dim);
            UpdateScale(dim);
        }

        /**
         * Disposes of all player windows
         */
        public void DisposePlayerBoards()
        {
            foreach (PlayerBoard pb in playerBoards)
            {
                pb.Close();
            }
        }

        public void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to quit to the lobby?", "Quit Game?", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                Enabled = false;
                DisposePlayerBoards();
            }
        }

        private static readonly Dictionary<int, Point[]> locs = new Dictionary<int, Point[]>(3);

        private static readonly Point[] loc5 = {
            new Point(400, 0),
            new Point(780, 276),
            new Point(635, 724),
            new Point(165, 724),
            new Point(20, 276)
        };
        private static readonly Point[] loc7 = {
            new Point(400, 0),
            new Point(713, 151),
            new Point(790, 489),
            new Point(574, 760),
            new Point(226, 760),
            new Point(10, 489),
            new Point(87, 151)
        };
        private static readonly Point[] loc9 = {
            new Point(400, 0),
            new Point(657, 94),
            new Point(794, 331),
            new Point(746, 600),
            new Point(537, 776),
            new Point(263, 776),
            new Point(54, 600),
            new Point(6, 331),
            new Point(143, 94)
        };

        private const float BaseSize = 1040f;
    }
}
