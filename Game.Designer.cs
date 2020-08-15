namespace AzulApp
{
    partial class Game
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Game));
            this.pnlCenterArea = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // pnlCenterArea
            // 
            this.pnlCenterArea.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlCenterArea.Location = new System.Drawing.Point(490, 490);
            this.pnlCenterArea.Name = "pnlCenterArea";
            this.pnlCenterArea.Size = new System.Drawing.Size(60, 60);
            this.pnlCenterArea.TabIndex = 10;
            // 
            // Game
            // 
            this.ClientSize = new System.Drawing.Size(1040, 1040);
            this.Controls.Add(this.pnlCenterArea);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Game";
            this.Text = "Common Area";
            this.ResizeEnd += new System.EventHandler(this.Game_ResizeEnd);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel pnlCenterArea;
    }
}