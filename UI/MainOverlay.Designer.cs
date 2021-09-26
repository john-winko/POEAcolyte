
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
            this.labelTest = new System.Windows.Forms.Label();
            this.HomePanel = new System.Windows.Forms.FlowLayoutPanel();
            this.HideoutButton = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            this.HomePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.BackgroundImage = global::PoeAcolyte.UI.Icons.exit;
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnExit.Location = new System.Drawing.Point(41, 3);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(32, 32);
            this.btnExit.TabIndex = 0;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // buttonTest3
            // 
            this.buttonTest3.BackgroundImage = global::PoeAcolyte.UI.Icons.question_mark;
            this.buttonTest3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonTest3.ContextMenuStrip = this.contextMenuStrip1;
            this.buttonTest3.Location = new System.Drawing.Point(3, 3);
            this.buttonTest3.Name = "buttonTest3";
            this.buttonTest3.Size = new System.Drawing.Size(32, 32);
            this.buttonTest3.TabIndex = 4;
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
            this.editBoundsToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.editBoundsToolStripMenuItem.Text = "Edit Bounds";
            this.editBoundsToolStripMenuItem.Click += new System.EventHandler(this.editBoundsToolStripMenuItem_Click);
            // 
            // saveBoundsToolStripMenuItem
            // 
            this.saveBoundsToolStripMenuItem.Name = "saveBoundsToolStripMenuItem";
            this.saveBoundsToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.saveBoundsToolStripMenuItem.Text = "Save Bounds";
            this.saveBoundsToolStripMenuItem.Click += new System.EventHandler(this.saveBoundsToolStripMenuItem_Click);
            // 
            // labelTest
            // 
            this.labelTest.BackColor = System.Drawing.Color.Lime;
            this.labelTest.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelTest.Location = new System.Drawing.Point(305, 394);
            this.labelTest.Name = "labelTest";
            this.labelTest.Size = new System.Drawing.Size(178, 95);
            this.labelTest.TabIndex = 5;
            this.labelTest.Text = "label1";
            this.labelTest.Visible = false;
            // 
            // HomePanel
            // 
            this.HomePanel.Controls.Add(this.buttonTest3);
            this.HomePanel.Controls.Add(this.btnExit);
            this.HomePanel.Controls.Add(this.HideoutButton);
            this.HomePanel.Location = new System.Drawing.Point(1061, 616);
            this.HomePanel.Margin = new System.Windows.Forms.Padding(1);
            this.HomePanel.Name = "HomePanel";
            this.HomePanel.Size = new System.Drawing.Size(148, 150);
            this.HomePanel.TabIndex = 6;
            // 
            // HideoutButton
            // 
            this.HideoutButton.BackgroundImage = global::PoeAcolyte.UI.Icons.home;
            this.HideoutButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.HideoutButton.Location = new System.Drawing.Point(79, 3);
            this.HideoutButton.Name = "HideoutButton";
            this.HideoutButton.Size = new System.Drawing.Size(32, 32);
            this.HideoutButton.TabIndex = 5;
            this.HideoutButton.UseVisualStyleBackColor = true;
            this.HideoutButton.Click += new System.EventHandler(this.HideoutButton_Click);
            // 
            // MainOverlay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lime;
            this.ClientSize = new System.Drawing.Size(1546, 914);
            this.Controls.Add(this.HomePanel);
            this.Controls.Add(this.labelTest);
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
            this.HomePanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button buttonTest3;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem editBoundsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveBoundsToolStripMenuItem;
        private System.Windows.Forms.Label labelTest;
        private System.Windows.Forms.FlowLayoutPanel HomePanel;
        private System.Windows.Forms.Button HideoutButton;
    }
}