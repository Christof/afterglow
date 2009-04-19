using System;

namespace WysiwygEditor.Components
{
    partial class ListControl<T>
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public new String Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = this.mFrame.Text = value;
            }
        }

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
            this.mFrame = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.mRemoveButton = new System.Windows.Forms.Button();
            this.mAddButton = new System.Windows.Forms.Button();
            this.mListBox = new System.Windows.Forms.ListBox();
            this.mFrame.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mFrame
            // 
            this.mFrame.Controls.Add(this.panel1);
            this.mFrame.Controls.Add(this.mListBox);
            this.mFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mFrame.Location = new System.Drawing.Point(0, 0);
            this.mFrame.Name = "mFrame";
            this.mFrame.Size = new System.Drawing.Size(320, 308);
            this.mFrame.TabIndex = 0;
            this.mFrame.TabStop = false;
            this.mFrame.Text = "groupBox1";
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.mRemoveButton);
            this.panel1.Controls.Add(this.mAddButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(283, 16);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(34, 289);
            this.panel1.TabIndex = 3;
            // 
            // mRemoveButton
            // 
            this.mRemoveButton.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mRemoveButton.Location = new System.Drawing.Point(3, 32);
            this.mRemoveButton.Name = "mRemoveButton";
            this.mRemoveButton.Size = new System.Drawing.Size(28, 23);
            this.mRemoveButton.TabIndex = 4;
            this.mRemoveButton.Text = "-";
            this.mRemoveButton.UseVisualStyleBackColor = true;
            this.mRemoveButton.Click += new System.EventHandler(this.mRemoveButton_Click);
            // 
            // mAddButton
            // 
            this.mAddButton.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mAddButton.Location = new System.Drawing.Point(3, 3);
            this.mAddButton.Name = "mAddButton";
            this.mAddButton.Size = new System.Drawing.Size(28, 23);
            this.mAddButton.TabIndex = 3;
            this.mAddButton.Text = "+";
            this.mAddButton.UseVisualStyleBackColor = true;
            this.mAddButton.Click += new System.EventHandler(this.mAddButton_Click);
            // 
            // mListBox
            // 
            this.mListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mListBox.FormattingEnabled = true;
            this.mListBox.Location = new System.Drawing.Point(3, 16);
            this.mListBox.Name = "mListBox";
            this.mListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.mListBox.Size = new System.Drawing.Size(314, 277);
            this.mListBox.TabIndex = 0;
            // 
            // ListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mFrame);
            this.Name = "ListControl";
            this.Size = new System.Drawing.Size(320, 308);
            this.mFrame.ResumeLayout(false);
            this.mFrame.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox mFrame;
        private System.Windows.Forms.ListBox mListBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button mRemoveButton;
        private System.Windows.Forms.Button mAddButton;
    }
}
