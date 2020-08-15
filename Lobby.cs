//using GameServer;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace AzulApp
{
    public partial class Lobby : Form
    {
        private readonly TextBox[] txtName;

        public Lobby()
        {
            InitializeComponent();
            host = new BackgroundWorker();
            host.DoWork += backgroundWorker1_DoWork;

            if (!pnlGameType.Enabled)
                pnlGameType.Text = "Not yet implemented.";

            // Load textboxes into an array for easier access as a "list"
            txtName = new TextBox[4];
            txtName[0] = txtName0;
            txtName[1] = txtName1;
            txtName[2] = txtName2;
            txtName[3] = txtName3;
        }

        // Start a new game with the current settings
        private void btnStart_Click(object sender, EventArgs e)
        {
            int numPlayers = (int)spnrNumPlayers.Value;
            string[] names = new string[numPlayers];
            for (int i = 0; i < numPlayers; i++)
            {
                names[i] = txtName[i].Text.Trim().Length > 0 ? txtName[i].Text.Trim() : defaultNames[i];
            }
            Enabled = false;
            Game g = new Game(numPlayers, names);
            g.FormClosed += OnGameOver;
            g.Show();
        }

        // Only enough name boxes for the number of players are enabled
        private void NumPlayersChanged(object sender, EventArgs e)
        {
            int count = (int)spnrNumPlayers.Value;
            int i = 2;
            while (i < count)
            {
                txtName[i++].Enabled = true;
            }
            while (i < 4)
            {
                txtName[i++].Enabled = false;
            }
        }

        // Can only open lobby if hosting a network game
        private void rbHost_CheckedChanged(object sender, EventArgs e)
        {
            btnOpen.Enabled = rbHost.Checked;
        }

        // Starts the server proccess in another thread
        private void btnOpen_Click(object sender, EventArgs e)
        {
            host.RunWorkerAsync();
        }

        // Can only join a lobby if joining a network game
        private void rbJoin_CheckedChanged(object sender, EventArgs e)
        {
            btnJoin.Enabled = rbJoin.Checked;
        }

        private void btnJoin_Click(object sender, EventArgs e)
        {
            // IDK, some kind of search/address input dialog?
        }

        // Game is over (someone won or game abandoned, either way game windows have closed)
        public void OnGameOver(object sender, EventArgs e)
        {
            Enabled = true;
            Focus();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //new Server();
        }

        private static readonly string[] defaultNames = { "Player 1", "Player 2", "Player 3", "Player 4" };
    }
}
