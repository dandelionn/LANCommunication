namespace ContactList
{
    partial class ContactList
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ContactList));
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.contactListScrollbar = new LinkScrollbar.ContactListScrollbar();
            this.SuspendLayout();
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel.AutoScroll = true;
            this.flowLayoutPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.flowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Size = new System.Drawing.Size(350, 373);
            this.flowLayoutPanel.TabIndex = 0;
            this.flowLayoutPanel.WrapContents = false;
            // 
            // contactListScrollbar
            // 
            this.contactListScrollbar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.contactListScrollbar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.contactListScrollbar.ChannelBorderColor = System.Drawing.Color.Empty;
            this.contactListScrollbar.ChannelColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.contactListScrollbar.DownArrow = ((System.Drawing.Image)(resources.GetObject("contactListScrollbar.DownArrow")));
            this.contactListScrollbar.Location = new System.Drawing.Point(329, 0);
            this.contactListScrollbar.Maximum = 0;
            this.contactListScrollbar.Minimum = 0;
            this.contactListScrollbar.Name = "contactListScrollbar";
            this.contactListScrollbar.Size = new System.Drawing.Size(19, 373);
            this.contactListScrollbar.TabIndex = 0;
            this.contactListScrollbar.ThumbBottom = null;
            this.contactListScrollbar.ThumbBottomSpan = null;
            this.contactListScrollbar.ThumbColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.contactListScrollbar.ThumbMiddle = null;
            this.contactListScrollbar.ThumbPosition = 0;
            this.contactListScrollbar.ThumbTop = null;
            this.contactListScrollbar.ThumbTopSpan = null;
            this.contactListScrollbar.UpArrow = ((System.Drawing.Image)(resources.GetObject("contactListScrollbar.UpArrow")));
            // 
            // ContactList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.Controls.Add(this.contactListScrollbar);
            this.Controls.Add(this.flowLayoutPanel);
            this.Name = "ContactList";
            this.Size = new System.Drawing.Size(350, 373);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
        private LinkScrollbar.ContactListScrollbar contactListScrollbar;
    }
}
