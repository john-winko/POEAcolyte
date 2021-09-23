
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
            this.components = new System.ComponentModel.Container();
            this.btnExit = new System.Windows.Forms.Button();
            this.buttonTest3 = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editBoundsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveBoundsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
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
            // buttonTest3
            // 
            this.buttonTest3.ContextMenuStrip = this.contextMenuStrip1;
            this.buttonTest3.Location = new System.Drawing.Point(81, 152);
            this.buttonTest3.Name = "buttonTest3";
            this.buttonTest3.Size = new System.Drawing.Size(75, 23);
            this.buttonTest3.TabIndex = 4;
            this.buttonTest3.Text = "Test";
            this.buttonTest3.UseVisualStyleBackColor = true;
            this.buttonTest3.Click += new System.EventHandler(this.buttonTest3_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editBoundsToolStripMenuItem,
            this.saveBoundsToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(142, 48);
            // 
            // editBoundsToolStripMenuItem
            // 
            this.editBoundsToolStripMenuItem.Name = "editBoundsToolStripMenuItem";
            this.editBoundsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.editBoundsToolStripMenuItem.Text = "Edit Bounds";
            this.editBoundsToolStripMenuItem.Click += new System.EventHandler(this.editBoundsToolStripMenuItem_Click);
            // 
            // saveBoundsToolStripMenuItem
            // 
            this.saveBoundsToolStripMenuItem.Name = "saveBoundsToolStripMenuItem";
            this.saveBoundsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveBoundsToolStripMenuItem.Text = "Save Bounds";
            this.saveBoundsToolStripMenuItem.Click += new System.EventHandler(this.saveBoundsToolStripMenuItem_Click);
            // 
            // MainOverlay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lime;
            this.ClientSize = new System.Drawing.Size(1546, 914);
            this.Controls.Add(this.buttonTest3);
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
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button buttonTest3;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem editBoundsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveBoundsToolStripMenuItem;
    }
}