
namespace PoeAcolyte.UI
{
    partial class MainOverlay
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
            this.btnExit = new System.Windows.Forms.Button();
            this._interactionPanel = new PoeAcolyte.UI.InteractionPanel();
            this._btnTest = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(81, 123);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 0;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // _interactionPanel
            // 
            this._interactionPanel.Location = new System.Drawing.Point(348, 196);
            this._interactionPanel.Name = "_interactionPanel";
            this._interactionPanel.Size = new System.Drawing.Size(1108, 490);
            this._interactionPanel.TabIndex = 1;
            // 
            // _btnTest
            // 
            this._btnTest.Location = new System.Drawing.Point(96, 185);
            this._btnTest.Name = "_btnTest";
            this._btnTest.Size = new System.Drawing.Size(75, 23);
            this._btnTest.TabIndex = 2;
            this._btnTest.Text = "Test";
            this._btnTest.UseVisualStyleBackColor = true;
            this._btnTest.Click += new System.EventHandler(this._btnTest_Click);
            // 
            // MainOverlay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lime;
            this.ClientSize = new System.Drawing.Size(1546, 914);
            this.Controls.Add(this._btnTest);
            this.Controls.Add(this._interactionPanel);
            this.Controls.Add(this.btnExit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainOverlay";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "MainOverlay";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.Lime;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnExit;
        private InteractionPanel _interactionPanel;
        private System.Windows.Forms.Button _btnTest;
    }
}