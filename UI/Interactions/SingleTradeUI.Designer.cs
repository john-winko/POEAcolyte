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
            this.LabelInfo = new System.Windows.Forms.Label();
            this.PriceLabel = new System.Windows.Forms.Label();
            this.InviteButton = new System.Windows.Forms.Button();
            this.MenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolTipHistory = new System.Windows.Forms.ToolTip(this.components);
            this.HideoutButton = new System.Windows.Forms.Button();
            this.SearchButton = new System.Windows.Forms.Button();
            this.TradeButton = new System.Windows.Forms.Button();
            this.KickButton = new System.Windows.Forms.Button();
            this.CurrencyPicture = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.CurrencyPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // CloseButton
            // 
            this.CloseButton.BackgroundImage = global::PoeAcolyte.UI.Icons.exit;
            this.CloseButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.CloseButton.Location = new System.Drawing.Point(1, 50);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(32, 32);
            this.CloseButton.TabIndex = 1;
            this.ToolTipHistory.SetToolTip(this.CloseButton, "Exit (Close)");
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // LabelInfo
            // 
            this.LabelInfo.Location = new System.Drawing.Point(47, 0);
            this.LabelInfo.Name = "LabelInfo";
            this.LabelInfo.Size = new System.Drawing.Size(142, 48);
            this.LabelInfo.TabIndex = 3;
            this.LabelInfo.Text = "Information";
            this.LabelInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PriceLabel
            // 
            this.PriceLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.PriceLabel.Location = new System.Drawing.Point(0, 0);
            this.PriceLabel.Name = "PriceLabel";
            this.PriceLabel.Size = new System.Drawing.Size(48, 30);
            this.PriceLabel.TabIndex = 5;
            this.PriceLabel.Text = "Price";
            // 
            // InviteButton
            // 
            this.InviteButton.BackgroundImage = global::PoeAcolyte.UI.Icons.add;
            this.InviteButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.InviteButton.ContextMenuStrip = this.MenuStrip;
            this.InviteButton.Location = new System.Drawing.Point(94, 50);
            this.InviteButton.Name = "InviteButton";
            this.InviteButton.Size = new System.Drawing.Size(32, 32);
            this.InviteButton.TabIndex = 7;
            this.ToolTipHistory.SetToolTip(this.InviteButton, "Invite Player");
            this.InviteButton.UseVisualStyleBackColor = true;
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
            // HideoutButton
            // 
            this.HideoutButton.BackgroundImage = global::PoeAcolyte.UI.Icons.home;
            this.HideoutButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.HideoutButton.Location = new System.Drawing.Point(32, 50);
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
            this.SearchButton.Location = new System.Drawing.Point(63, 50);
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
            this.TradeButton.Location = new System.Drawing.Point(125, 50);
            this.TradeButton.Name = "TradeButton";
            this.TradeButton.Size = new System.Drawing.Size(32, 32);
            this.TradeButton.TabIndex = 11;
            this.ToolTipHistory.SetToolTip(this.TradeButton, "Trade with player");
            this.TradeButton.UseVisualStyleBackColor = true;
            // 
            // KickButton
            // 
            this.KickButton.BackgroundImage = global::PoeAcolyte.UI.Icons.tick;
            this.KickButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.KickButton.ContextMenuStrip = this.MenuStrip;
            this.KickButton.Location = new System.Drawing.Point(156, 50);
            this.KickButton.Name = "KickButton";
            this.KickButton.Size = new System.Drawing.Size(32, 32);
            this.KickButton.TabIndex = 12;
            this.ToolTipHistory.SetToolTip(this.KickButton, "Kick");
            this.KickButton.UseVisualStyleBackColor = true;
            // 
            // CurrencyPicture
            // 
            this.CurrencyPicture.Image = global::PoeAcolyte.UI.Icons.home;
            this.CurrencyPicture.Location = new System.Drawing.Point(0, 0);
            this.CurrencyPicture.Name = "CurrencyPicture";
            this.CurrencyPicture.Size = new System.Drawing.Size(48, 48);
            this.CurrencyPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.CurrencyPicture.TabIndex = 15;
            this.CurrencyPicture.TabStop = false;
            // 
            // SingleTradeUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.KickButton);
            this.Controls.Add(this.TradeButton);
            this.Controls.Add(this.SearchButton);
            this.Controls.Add(this.HideoutButton);
            this.Controls.Add(this.InviteButton);
            this.Controls.Add(this.PriceLabel);
            this.Controls.Add(this.LabelInfo);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.CurrencyPicture);
            this.Name = "SingleTradeUI";
            this.Size = new System.Drawing.Size(189, 84);
            this.ToolTipHistory.SetToolTip(this, "Test");
            ((System.ComponentModel.ISupportInitialize)(this.CurrencyPicture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.Button CloseButton;
        public System.Windows.Forms.Label LabelInfo;
        public System.Windows.Forms.Label PriceLabel;
        public System.Windows.Forms.Button InviteButton;
        public System.Windows.Forms.ToolTip ToolTipHistory;
        public System.Windows.Forms.ContextMenuStrip MenuStrip;
        private System.Windows.Forms.Button HideoutButton;
        private System.Windows.Forms.Button SearchButton;
        public System.Windows.Forms.Button TradeButton;
        public System.Windows.Forms.Button KickButton;
        public System.Windows.Forms.PictureBox CurrencyPicture;
    }
}