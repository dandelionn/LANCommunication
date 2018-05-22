namespace MessageBox
{
    partial class MessageBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MessageBox));
            this.richTextBox = new System.Windows.Forms.RichTextBox();
            this.advancedVScrollbar = new ChatBoxScrollbar.ChatBoxScrollbar();
            this.SuspendLayout();
            // 
            // richTextBox
            // 
            this.richTextBox.AcceptsTab = true;
            this.richTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.richTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.richTextBox.Location = new System.Drawing.Point(0, 0);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.ReadOnly = true;
            this.richTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.richTextBox.Size = new System.Drawing.Size(425, 371);
            this.richTextBox.TabIndex = 2;
            this.richTextBox.Text = "";
            this.richTextBox.VScroll += new System.EventHandler(this.richTextBox1_VScroll);
            this.richTextBox.SizeChanged += new System.EventHandler(this.richTextBox_SizeChanged);
            this.richTextBox.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // advancedVScrollbar
            // 
            this.advancedVScrollbar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.advancedVScrollbar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.advancedVScrollbar.ChannelBorderColor = System.Drawing.Color.Empty;
            this.advancedVScrollbar.ChannelColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.advancedVScrollbar.DownArrow = ((System.Drawing.Image)(resources.GetObject("advancedVScrollbar.DownArrow")));
            this.advancedVScrollbar.Location = new System.Drawing.Point(406, 0);
            this.advancedVScrollbar.Maximum = 0;
            this.advancedVScrollbar.Minimum = 0;
            this.advancedVScrollbar.Name = "advancedVScrollbar";
            this.advancedVScrollbar.Size = new System.Drawing.Size(19, 371);
            this.advancedVScrollbar.TabIndex = 3;
            this.advancedVScrollbar.ThumbBottom = null;
            this.advancedVScrollbar.ThumbBottomSpan = null;
            this.advancedVScrollbar.ThumbColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.advancedVScrollbar.ThumbMiddle = null;
            this.advancedVScrollbar.ThumbPosition = 0;
            this.advancedVScrollbar.ThumbTop = null;
            this.advancedVScrollbar.ThumbTopSpan = null;
            this.advancedVScrollbar.UpArrow = ((System.Drawing.Image)(resources.GetObject("advancedVScrollbar.UpArrow")));
            // 
            // MessageBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.advancedVScrollbar);
            this.Controls.Add(this.richTextBox);
            this.Name = "MessageBox";
            this.Size = new System.Drawing.Size(425, 371);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.RichTextBox richTextBox;
        private ChatBoxScrollbar.ChatBoxScrollbar advancedVScrollbar;
    }
}
