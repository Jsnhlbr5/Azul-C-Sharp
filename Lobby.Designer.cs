namespace AzulApp
{
    partial class Lobby
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Lobby));
            this.pnlGameType = new System.Windows.Forms.GroupBox();
            this.rbJoin = new System.Windows.Forms.RadioButton();
            this.rbHost = new System.Windows.Forms.RadioButton();
            this.rbLocal = new System.Windows.Forms.RadioButton();
            this.btnStart = new System.Windows.Forms.Button();
            this.spnrNumPlayers = new System.Windows.Forms.NumericUpDown();
            this.lblNumPlayers = new System.Windows.Forms.Label();
            this.txtName0 = new System.Windows.Forms.TextBox();
            this.txtName1 = new System.Windows.Forms.TextBox();
            this.txtName2 = new System.Windows.Forms.TextBox();
            this.txtName3 = new System.Windows.Forms.TextBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnJoin = new System.Windows.Forms.Button();
            this.pnlGameType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spnrNumPlayers)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlGameType
            // 
            this.pnlGameType.Controls.Add(this.rbJoin);
            this.pnlGameType.Controls.Add(this.rbHost);
            this.pnlGameType.Controls.Add(this.rbLocal);
            this.pnlGameType.Enabled = false;
            this.pnlGameType.Location = new System.Drawing.Point(13, 13);
            this.pnlGameType.Name = "pnlGameType";
            this.pnlGameType.Size = new System.Drawing.Size(161, 100);
            this.pnlGameType.TabIndex = 0;
            this.pnlGameType.TabStop = false;
            this.pnlGameType.Text = "How do you want to play?";
            // 
            // rbJoin
            // 
            this.rbJoin.AutoSize = true;
            this.rbJoin.Location = new System.Drawing.Point(7, 73);
            this.rbJoin.Name = "rbJoin";
            this.rbJoin.Size = new System.Drawing.Size(127, 19);
            this.rbJoin.TabIndex = 2;
            this.rbJoin.Text = "Join Network game";
            this.rbJoin.UseVisualStyleBackColor = true;
            this.rbJoin.CheckedChanged += new System.EventHandler(this.rbJoin_CheckedChanged);
            // 
            // rbHost
            // 
            this.rbHost.AutoSize = true;
            this.rbHost.Location = new System.Drawing.Point(7, 48);
            this.rbHost.Name = "rbHost";
            this.rbHost.Size = new System.Drawing.Size(131, 19);
            this.rbHost.TabIndex = 1;
            this.rbHost.Text = "Host Network game";
            this.rbHost.UseVisualStyleBackColor = true;
            this.rbHost.CheckedChanged += new System.EventHandler(this.rbHost_CheckedChanged);
            // 
            // rbLocal
            // 
            this.rbLocal.AutoSize = true;
            this.rbLocal.Checked = true;
            this.rbLocal.Location = new System.Drawing.Point(7, 23);
            this.rbLocal.Name = "rbLocal";
            this.rbLocal.Size = new System.Drawing.Size(111, 19);
            this.rbLocal.TabIndex = 0;
            this.rbLocal.TabStop = true;
            this.rbLocal.Text = "Play Local game";
            this.rbLocal.UseVisualStyleBackColor = true;
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.Location = new System.Drawing.Point(290, 153);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 7;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // spnrNumPlayers
            // 
            this.spnrNumPlayers.Location = new System.Drawing.Point(70, 119);
            this.spnrNumPlayers.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.spnrNumPlayers.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.spnrNumPlayers.Name = "spnrNumPlayers";
            this.spnrNumPlayers.Size = new System.Drawing.Size(77, 23);
            this.spnrNumPlayers.TabIndex = 1;
            this.spnrNumPlayers.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.spnrNumPlayers.ValueChanged += new System.EventHandler(this.NumPlayersChanged);
            // 
            // lblNumPlayers
            // 
            this.lblNumPlayers.AutoSize = true;
            this.lblNumPlayers.Location = new System.Drawing.Point(17, 121);
            this.lblNumPlayers.Name = "lblNumPlayers";
            this.lblNumPlayers.Size = new System.Drawing.Size(47, 15);
            this.lblNumPlayers.TabIndex = 3;
            this.lblNumPlayers.Text = "Players:";
            // 
            // txtName0
            // 
            this.txtName0.Location = new System.Drawing.Point(192, 26);
            this.txtName0.Name = "txtName0";
            this.txtName0.PlaceholderText = "Player 1";
            this.txtName0.Size = new System.Drawing.Size(172, 23);
            this.txtName0.TabIndex = 2;
            // 
            // txtName1
            // 
            this.txtName1.Location = new System.Drawing.Point(192, 55);
            this.txtName1.Name = "txtName1";
            this.txtName1.PlaceholderText = "Player 2";
            this.txtName1.Size = new System.Drawing.Size(172, 23);
            this.txtName1.TabIndex = 3;
            // 
            // txtName2
            // 
            this.txtName2.Enabled = false;
            this.txtName2.Location = new System.Drawing.Point(192, 84);
            this.txtName2.Name = "txtName2";
            this.txtName2.PlaceholderText = "Player 3";
            this.txtName2.Size = new System.Drawing.Size(172, 23);
            this.txtName2.TabIndex = 4;
            // 
            // txtName3
            // 
            this.txtName3.Enabled = false;
            this.txtName3.Location = new System.Drawing.Point(192, 113);
            this.txtName3.Name = "txtName3";
            this.txtName3.PlaceholderText = "Player 4";
            this.txtName3.Size = new System.Drawing.Size(172, 23);
            this.txtName3.TabIndex = 5;
            // 
            // btnOpen
            // 
            this.btnOpen.Enabled = false;
            this.btnOpen.Location = new System.Drawing.Point(13, 153);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(148, 23);
            this.btnOpen.TabIndex = 6;
            this.btnOpen.Text = "Open Lobby to Network";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnJoin
            // 
            this.btnJoin.Enabled = false;
            this.btnJoin.Location = new System.Drawing.Point(167, 153);
            this.btnJoin.Name = "btnJoin";
            this.btnJoin.Size = new System.Drawing.Size(80, 23);
            this.btnJoin.TabIndex = 6;
            this.btnJoin.Text = "Join Lobby";
            this.btnJoin.UseVisualStyleBackColor = true;
            this.btnJoin.Click += new System.EventHandler(this.btnJoin_Click);
            // 
            // Lobby
            // 
            this.AcceptButton = this.btnStart;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(377, 188);
            this.Controls.Add(this.btnJoin);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.txtName3);
            this.Controls.Add(this.txtName2);
            this.Controls.Add(this.txtName1);
            this.Controls.Add(this.txtName0);
            this.Controls.Add(this.lblNumPlayers);
            this.Controls.Add(this.spnrNumPlayers);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.pnlGameType);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Lobby";
            this.Text = "Azul";
            this.pnlGameType.ResumeLayout(false);
            this.pnlGameType.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spnrNumPlayers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox pnlGameType;
        private System.Windows.Forms.RadioButton rbJoin;
        private System.Windows.Forms.RadioButton rbHost;
        private System.Windows.Forms.RadioButton rbLocal;
        private System.Windows.Forms.Label lblNumPlayers;
        private System.Windows.Forms.NumericUpDown spnrNumPlayers;
        private System.Windows.Forms.TextBox txtName0;
        private System.Windows.Forms.TextBox txtName1;
        private System.Windows.Forms.TextBox txtName2;
        private System.Windows.Forms.TextBox txtName3;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnStart;
        private System.ComponentModel.BackgroundWorker host;
        private System.Windows.Forms.Button btnJoin;
    }
}

