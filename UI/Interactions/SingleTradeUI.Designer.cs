using System.ComponentModel;

namespace PoeAcolyte.UI.Interactions
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
            this.components = new System.ComponentModel.Container();
            this.CloseButton = new System.Windows.Forms.Button();
            this.IncomingLabel = new System.Windows.Forms.Label();
            this.PlayerLabel = new System.Windows.Forms.Label();
            this.LocationLabel = new System.Windows.Forms.Label();
            this.PriceLabel = new System.Windows.Forms.Label();
            this.QuickButton = new System.Windows.Forms.Button();
            this.MenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolTipHistory = new System.Windows.Forms.ToolTip(this.components);
            this.LabelStatus = new System.Windows.Forms.Label();
            this.HideoutButton = new System.Windows.Forms.Button();
            this.SearchButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CloseButton
            // 
            this.CloseButton.Image = global::PoeAcolyte.UI.Icons.exit;
            this.CloseButton.Location = new System.Drawing.Point(1, 78);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(32, 32);
            this.CloseButton.TabIndex = 1;
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // IncomingLabel
            // 
            this.IncomingLabel.Location = new System.Drawing.Point(0, 0);
            this.IncomingLabel.Name = "IncomingLabel";
            this.IncomingLabel.Size = new System.Drawing.Size(58, 23);
            this.IncomingLabel.TabIndex = 2;
            this.IncomingLabel.Text = "Incoming";
            this.IncomingLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PlayerLabel
            // 
            this.PlayerLabel.BackColor = System.Drawing.SystemColors.Control;
            this.PlayerLabel.Location = new System.Drawing.Point(64, 0);
            this.PlayerLabel.Name = "PlayerLabel";
            this.PlayerLabel.Size = new System.Drawing.Size(130, 23);
            this.PlayerLabel.TabIndex = 3;
            this.PlayerLabel.Text = "TradeInteraction";
            this.PlayerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LocationLabel
            // 
            this.LocationLabel.Location = new System.Drawing.Point(64, 23);
            this.LocationLabel.Name = "LocationLabel";
            this.LocationLabel.Size = new System.Drawing.Size(130, 23);
            this.LocationLabel.TabIndex = 4;
            this.LocationLabel.Text = "Location";
            this.LocationLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PriceLabel
            // 
            this.PriceLabel.Location = new System.Drawing.Point(0, 23);
            this.PriceLabel.Name = "PriceLabel";
            this.PriceLabel.Size = new System.Drawing.Size(58, 23);
            this.PriceLabel.TabIndex = 5;
            this.PriceLabel.Text = "Price";
            this.PriceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // QuickButton
            // 
            this.QuickButton.ContextMenuStrip = this.MenuStrip;
            this.QuickButton.Location = new System.Drawing.Point(100, 49);
            this.QuickButton.Name = "QuickButton";
            this.QuickButton.Size = new System.Drawing.Size(94, 58);
            this.QuickButton.TabIndex = 7;
            this.QuickButton.Text = "Quick";
            this.QuickButton.UseVisualStyleBackColor = true;
            this.QuickButton.Click += new System.EventHandler(this.QuickButton_Click);
            // 
            // MenuStrip
            // 
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.Size = new System.Drawing.Size(61, 4);
            // 
            // ToolTipHistory
            // 
            this.ToolTipHistory.ShowAlways = true;
            // 
            // LabelStatus
            // 
            this.LabelStatus.Location = new System.Drawing.Point(0, 49);
            this.LabelStatus.Name = "LabelStatus";
            this.LabelStatus.Size = new System.Drawing.Size(99, 23);
            this.LabelStatus.TabIndex = 8;
            this.LabelStatus.Text = "Status";
            this.LabelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // HideoutButton
            // 
            this.HideoutButton.Image = global::PoeAcolyte.UI.Icons.home;
            this.HideoutButton.Location = new System.Drawing.Point(34, 78);
            this.HideoutButton.Name = "HideoutButton";
            this.HideoutButton.Size = new System.Drawing.Size(32, 32);
            this.HideoutButton.TabIndex = 9;
            this.HideoutButton.UseVisualStyleBackColor = true;
            this.HideoutButton.Click += new System.EventHandler(this.HideoutButton_Click);
            // 
            // SearchButton
            // 
            this.SearchButton.Image = global::PoeAcolyte.UI.Icons.search;
            this.SearchButton.Location = new System.Drawing.Point(67, 78);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(32, 32);
            this.SearchButton.TabIndex = 10;
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // SingleTradeUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.SearchButton);
            this.Controls.Add(this.HideoutButton);
            this.Controls.Add(this.LabelStatus);
            this.Controls.Add(this.QuickButton);
            this.Controls.Add(this.PriceLabel);
            this.Controls.Add(this.LocationLabel);
            this.Controls.Add(this.PlayerLabel);
            this.Controls.Add(this.IncomingLabel);
            this.Controls.Add(this.CloseButton);
            this.Name = "SingleTradeUI";
            this.Size = new System.Drawing.Size(197, 107);
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.Button CloseButton;
        public System.Windows.Forms.Label IncomingLabel;
        public System.Windows.Forms.Label PlayerLabel;
        public System.Windows.Forms.Label LocationLabel;
        public System.Windows.Forms.Label PriceLabel;
        public System.Windows.Forms.Button QuickButton;
        public System.Windows.Forms.ToolTip ToolTipHistory;
        public System.Windows.Forms.ContextMenuStrip MenuStrip;
        public System.Windows.Forms.Label LabelStatus;
        private System.Windows.Forms.Button HideoutButton;
        private System.Windows.Forms.Button SearchButton;
    }
}