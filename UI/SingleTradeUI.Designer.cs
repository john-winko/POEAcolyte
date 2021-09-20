using System.ComponentModel;

namespace PoeAcolyte.UI
{
    partial class SingleTradeUI
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TestBox = new System.Windows.Forms.TextBox();
            this.CloseButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TestBox
            // 
            this.TestBox.Location = new System.Drawing.Point(68, 16);
            this.TestBox.Multiline = true;
            this.TestBox.Name = "TestBox";
            this.TestBox.Size = new System.Drawing.Size(163, 125);
            this.TestBox.TabIndex = 0;
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(9, 27);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(53, 23);
            this.CloseButton.TabIndex = 1;
            this.CloseButton.Text = "Exit";
            this.CloseButton.UseVisualStyleBackColor = true;
            // 
            // SingleTradeUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.TestBox);
            this.Name = "SingleTradeUI";
            this.Size = new System.Drawing.Size(250, 171);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox TestBox;
        public System.Windows.Forms.Button CloseButton;
    }
}