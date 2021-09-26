using System.ComponentModel;

namespace PoeAcolyte.UI.Interactions
{
    partial class BulkTradeUI
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
            this.InfoLabel = new System.Windows.Forms.Label();
            this.PriceInLabel = new System.Windows.Forms.Label();
            this.ToolTipHistory = new System.Windows.Forms.ToolTip(this.components);
            this.KickButton = new System.Windows.Forms.Button();
            this.MenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.TradeButton = new System.Windows.Forms.Button();
            this.SearchButton = new System.Windows.Forms.Button();
            this.HideoutButton = new System.Windows.Forms.Button();
            this.InviteButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.PriceInPicture = new System.Windows.Forms.PictureBox();
            this.PriceOutPicture = new System.Windows.Forms.PictureBox();
            this.PriceOutLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PriceInPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PriceOutPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // InfoLabel
            // 
            this.InfoLabel.Location = new System.Drawing.Point(95, 0);
            this.InfoLabel.Name = "InfoLabel";
            this.InfoLabel.Size = new System.Drawing.Size(99, 48);
            this.InfoLabel.TabIndex = 3;
            this.InfoLabel.Text = "TradeInteraction";
            this.InfoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PriceInLabel
            // 
            this.PriceInLabel.Location = new System.Drawing.Point(0, 0);
            this.PriceInLabel.Name = "PriceInLabel";
            this.PriceInLabel.Size = new System.Drawing.Size(48, 35);
            this.PriceInLabel.TabIndex = 5;
            this.PriceInLabel.Text = "Price In";
            this.PriceInLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ToolTipHistory
            // 
            this.ToolTipHistory.ShowAlways = true;
            // 
            // KickButton
            // 
            this.KickButton.BackgroundImage = global::PoeAcolyte.UI.Icons.tick;
            this.KickButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.KickButton.ContextMenuStrip = this.MenuStrip;
            this.KickButton.Location = new System.Drawing.Point(157, 50);
            this.KickButton.Name = "KickButton";
            this.KickButton.Size = new System.Drawing.Size(32, 32);
            this.KickButton.TabIndex = 18;
            this.ToolTipHistory.SetToolTip(this.KickButton, "Kick");
            this.KickButton.UseVisualStyleBackColor = true;
            this.KickButton.Click += new System.EventHandler(this.KickButton_Click);
            // 
            // MenuStrip
            // 
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.Size = new System.Drawing.Size(61, 4);
            // 
            // TradeButton
            // 
            this.TradeButton.BackgroundImage = global::PoeAcolyte.UI.Icons.money_turnover;
            this.TradeButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.TradeButton.ContextMenuStrip = this.MenuStrip;
            this.TradeButton.Location = new System.Drawing.Point(126, 50);
            this.TradeButton.Name = "TradeButton";
            this.TradeButton.Size = new System.Drawing.Size(32, 32);
            this.TradeButton.TabIndex = 17;
            this.ToolTipHistory.SetToolTip(this.TradeButton, "Trade with player");
            this.TradeButton.UseVisualStyleBackColor = true;
            this.TradeButton.Click += new System.EventHandler(this.TradeButton_Click);
            // 
            // SearchButton
            // 
            this.SearchButton.BackgroundImage = global::PoeAcolyte.UI.Icons.search;
            this.SearchButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.SearchButton.Location = new System.Drawing.Point(64, 50);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(32, 32);
            this.SearchButton.TabIndex = 16;
            this.ToolTipHistory.SetToolTip(this.SearchButton, "Show item location overlay");
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // HideoutButton
            // 
            this.HideoutButton.BackgroundImage = global::PoeAcolyte.UI.Icons.home;
            this.HideoutButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.HideoutButton.Location = new System.Drawing.Point(33, 50);
            this.HideoutButton.Name = "HideoutButton";
            this.HideoutButton.Size = new System.Drawing.Size(32, 32);
            this.HideoutButton.TabIndex = 15;
            this.ToolTipHistory.SetToolTip(this.HideoutButton, "Go to their hideout");
            this.HideoutButton.UseVisualStyleBackColor = true;
            this.HideoutButton.Click += new System.EventHandler(this.HideoutButton_Click);
            // 
            // InviteButton
            // 
            this.InviteButton.BackgroundImage = global::PoeAcolyte.UI.Icons.add;
            this.InviteButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.InviteButton.ContextMenuStrip = this.MenuStrip;
            this.InviteButton.Location = new System.Drawing.Point(95, 50);
            this.InviteButton.Name = "InviteButton";
            this.InviteButton.Size = new System.Drawing.Size(32, 32);
            this.InviteButton.TabIndex = 14;
            this.ToolTipHistory.SetToolTip(this.InviteButton, "Invite Player");
            this.InviteButton.UseVisualStyleBackColor = true;
            this.InviteButton.Click += new System.EventHandler(this.InviteButton_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.BackgroundImage = global::PoeAcolyte.UI.Icons.exit;
            this.CloseButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.CloseButton.Location = new System.Drawing.Point(2, 50);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(32, 32);
            this.CloseButton.TabIndex = 13;
            this.ToolTipHistory.SetToolTip(this.CloseButton, "Exit (Close)");
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // PriceInPicture
            // 
            this.PriceInPicture.Location = new System.Drawing.Point(0, 0);
            this.PriceInPicture.Name = "PriceInPicture";
            this.PriceInPicture.Size = new System.Drawing.Size(48, 48);
            this.PriceInPicture.TabIndex = 19;
            this.PriceInPicture.TabStop = false;
            // 
            // PriceOutPicture
            // 
            this.PriceOutPicture.Location = new System.Drawing.Point(48, 0);
            this.PriceOutPicture.Name = "PriceOutPicture";
            this.PriceOutPicture.Size = new System.Drawing.Size(48, 48);
            this.PriceOutPicture.TabIndex = 20;
            this.PriceOutPicture.TabStop = false;
            // 
            // PriceOutLabel
            // 
            this.PriceOutLabel.Location = new System.Drawing.Point(48, 0);
            this.PriceOutLabel.Name = "PriceOutLabel";
            this.PriceOutLabel.Size = new System.Drawing.Size(48, 35);
            this.PriceOutLabel.TabIndex = 21;
            this.PriceOutLabel.Text = "Price Out";
            this.PriceOutLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BulkTradeUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PriceOutLabel);
            this.Controls.Add(this.PriceOutPicture);
            this.Controls.Add(this.KickButton);
            this.Controls.Add(this.TradeButton);
            this.Controls.Add(this.SearchButton);
            this.Controls.Add(this.HideoutButton);
            this.Controls.Add(this.InviteButton);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.PriceInLabel);
            this.Controls.Add(this.InfoLabel);
            this.Controls.Add(this.PriceInPicture);
            this.Name = "BulkTradeUI";
            this.Size = new System.Drawing.Size(189, 84);
            ((System.ComponentModel.ISupportInitialize)(this.PriceInPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PriceOutPicture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.Label InfoLabel;
        public System.Windows.Forms.Label PriceInLabel;
        public System.Windows.Forms.ToolTip ToolTipHistory;
        public System.Windows.Forms.ContextMenuStrip MenuStrip;
        public System.Windows.Forms.Button KickButton;
        public System.Windows.Forms.Button TradeButton;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.Button HideoutButton;
        public System.Windows.Forms.Button InviteButton;
        public System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.PictureBox PriceInPicture;
        private System.Windows.Forms.PictureBox PriceOutPicture;
        public System.Windows.Forms.Label PriceOutLabel;
    }
}