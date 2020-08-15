namespace AzulApp
{
    partial class PlayerBoard
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
            this.scoreMarker = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // scoreMarker
            // 
            this.scoreMarker.BackColor = System.Drawing.Color.Black;
            this.scoreMarker.ForeColor = System.Drawing.SystemColors.Highlight;
            this.scoreMarker.Location = new System.Drawing.Point(34, 0);
            this.scoreMarker.Name = "scoreMarker";
            this.scoreMarker.Size = new System.Drawing.Size(30, 30);
            this.scoreMarker.TabIndex = 0;
            // 
            // PlayerBoard
            // 
            this.ClientSize = new System.Drawing.Size(750, 750);
            this.Controls.Add(this.scoreMarker);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "PlayerBoard";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PlayerBoard_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label scoreMarker;
    }
}