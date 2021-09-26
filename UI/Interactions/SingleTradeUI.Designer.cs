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
            this.InfoLabel = new System.Windows.Forms.Label();
            this.MenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.PriceLabel = new System.Windows.Forms.Label();
            this.InviteButton = new System.Windows.Forms.Button();
            this.ToolTipHistory = new System.Windows.Forms.ToolTip(this.components);
            this.HideoutButton = new System.Windows.Forms.Button();
            this.SearchButton = new System.Windows.Forms.Button();
            this.TradeButton = new System.Windows.Forms.Button();
            this.KickButton = new System.Windows.Forms.Button();
            this.CurrencyPicture = new System.Windows.Forms.PictureBox();
            this.LocationLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.CurrencyPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // CloseButton
            // 
            this.CloseButton.BackgroundImage = global::PoeAcolyte.UI.Icons.exit;
            this.CloseButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.CloseButton.Location = new System.Drawing.Point(49, 63);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(32, 32);
            this.CloseButton.TabIndex = 1;
            this.ToolTipHistory.SetToolTip(this.CloseButton, "Exit (Close)");
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // InfoLabel
            // 
            this.InfoLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.InfoLabel.Location = new System.Drawing.Point(49, 1);
            this.InfoLabel.Name = "InfoLabel";
            this.InfoLabel.Size = new System.Drawing.Size(185, 59);
            this.InfoLabel.TabIndex = 3;
            this.InfoLabel.Text = "Information";
            this.InfoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MenuStrip
            // 
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.Size = new System.Drawing.Size(61, 4);
            // 
            // PriceLabel
            // 
            this.PriceLabel.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.PriceLabel.Location = new System.Drawing.Point(0, 50);
            this.PriceLabel.Name = "PriceLabel";
            this.PriceLabel.Size = new System.Drawing.Size(48, 30);
            this.PriceLabel.TabIndex = 5;
            this.PriceLabel.Text = "Price";
            this.PriceLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // InviteButton
            // 
            this.InviteButton.BackgroundImage = global::PoeAcolyte.UI.Icons.add;
            this.InviteButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.InviteButton.ContextMenuStrip = this.MenuStrip;
            this.InviteButton.Location = new System.Drawing.Point(142, 63);
            this.InviteButton.Name = "InviteButton";
            this.InviteButton.Size = new System.Drawing.Size(32, 32);
            this.InviteButton.TabIndex = 7;
            this.ToolTipHistory.SetToolTip(this.InviteButton, "Invite Player");
            this.InviteButton.UseVisualStyleBackColor = true;
            this.InviteButton.Click += new System.EventHandler(this.InviteButton_Click);
            // 
            // ToolTipHistory
            // 
            this.ToolTipHistory.ShowAlways = true;
            // 
            // HideoutButton
            // 
            this.HideoutButton.BackgroundImage = global::PoeAcolyte.UI.Icons.home;
            this.HideoutButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.HideoutButton.Location = new System.Drawing.Point(80, 63);
            this.HideoutButton.Name = "HideoutButton";
            this.HideoutButton.Size = new System.Drawing.Size(32, 32);
            this.HideoutButton.TabIndex = 9;
            this.ToolTipHistory.SetToolTip(this.HideoutButton, "Go to their hideout");
            this.HideoutButton.UseVisualStyleBackColor = true;
            this.HideoutButton.Click += new System.EventHandler(this.HideoutButton_Click);
            // 
            // SearchButton
            // 
            this.SearchButton.BackgroundImage = global::PoeAcolyte.UI.Icons.search;
            this.SearchButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.SearchButton.Location = new System.Drawing.Point(111, 63);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(32, 32);
            this.SearchButton.TabIndex = 10;
            this.ToolTipHistory.SetToolTip(this.SearchButton, "Show item location overlay");
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // TradeButton
            // 
            this.TradeButton.BackgroundImage = global::PoeAcolyte.UI.Icons.money_turnover;
            this.TradeButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.TradeButton.ContextMenuStrip = this.MenuStrip;
            this.TradeButton.Location = new System.Drawing.Point(173, 63);
            this.TradeButton.Name = "TradeButton";
            this.TradeButton.Size = new System.Drawing.Size(32, 32);
            this.TradeButton.TabIndex = 11;
            this.ToolTipHistory.SetToolTip(this.TradeButton, "Trade with player");
            this.TradeButton.UseVisualStyleBackColor = true;
            this.TradeButton.Click += new System.EventHandler(this.TradeButton_Click);
            // 
            // KickButton
            // 
            this.KickButton.BackgroundImage = global::PoeAcolyte.UI.Icons.tick;
            this.KickButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.KickButton.ContextMenuStrip = this.MenuStrip;
            this.KickButton.Location = new System.Drawing.Point(204, 63);
            this.KickButton.Name = "KickButton";
            this.KickButton.Size = new System.Drawing.Size(32, 32);
            this.KickButton.TabIndex = 12;
            this.ToolTipHistory.SetToolTip(this.KickButton, "Kick");
            this.KickButton.UseVisualStyleBackColor = true;
            this.KickButton.Click += new System.EventHandler(this.KickButton_Click);
            // 
            // CurrencyPicture
            // 
            this.CurrencyPicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CurrencyPicture.Location = new System.Drawing.Point(0, 1);
            this.CurrencyPicture.Name = "CurrencyPicture";
            this.CurrencyPicture.Size = new System.Drawing.Size(48, 48);
            this.CurrencyPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.CurrencyPicture.TabIndex = 15;
            this.CurrencyPicture.TabStop = false;
            // 
            // LocationLabel
            // 
            this.LocationLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LocationLabel.Location = new System.Drawing.Point(0, 76);
            this.LocationLabel.Name = "LocationLabel";
            this.LocationLabel.Size = new System.Drawing.Size(48, 16);
            this.LocationLabel.TabIndex = 16;
            this.LocationLabel.Text = "Price";
            this.LocationLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // SingleTradeUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.LocationLabel);
            this.Controls.Add(this.PriceLabel);
            this.Controls.Add(this.KickButton);
            this.Controls.Add(this.TradeButton);
            this.Controls.Add(this.SearchButton);
            this.Controls.Add(this.HideoutButton);
            this.Controls.Add(this.InviteButton);
            this.Controls.Add(this.InfoLabel);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.CurrencyPicture);
            this.Name = "SingleTradeUI";
            this.Size = new System.Drawing.Size(236, 96);
            this.ToolTipHistory.SetToolTip(this, "Test");
            ((System.ComponentModel.ISupportInitialize)(this.CurrencyPicture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.Button CloseButton;
        public System.Windows.Forms.Label InfoLabel;
        public System.Windows.Forms.Label PriceLabel;
        public System.Windows.Forms.Button InviteButton;
        public System.Windows.Forms.ToolTip ToolTipHistory;
        public System.Windows.Forms.ContextMenuStrip MenuStrip;
        private System.Windows.Forms.Button HideoutButton;
        private System.Windows.Forms.Button SearchButton;
        public System.Windows.Forms.Button TradeButton;
        public System.Windows.Forms.Button KickButton;
        public System.Windows.Forms.PictureBox CurrencyPicture;
        public System.Windows.Forms.Label LocationLabel;
    }
}