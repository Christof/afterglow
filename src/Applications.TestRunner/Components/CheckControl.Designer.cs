namespace Afterglow.Applications.TestRunner.Components
{
    partial class CheckControl
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.undoButton = new System.Windows.Forms.Button();
            this.invertButton = new System.Windows.Forms.Button();
            this.selectNoneButton = new System.Windows.Forms.Button();
            this.selectAllButton = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.filterTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.elementList = new System.Windows.Forms.CheckedListBox();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.undoButton);
            this.groupBox1.Controls.Add(this.invertButton);
            this.groupBox1.Controls.Add(this.selectNoneButton);
            this.groupBox1.Controls.Add(this.selectAllButton);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox1.Location = new System.Drawing.Point(495, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(62, 291);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "check";
            // 
            // undoButton
            // 
            this.undoButton.Location = new System.Drawing.Point(8, 114);
            this.undoButton.Name = "undoButton";
            this.undoButton.Size = new System.Drawing.Size(48, 23);
            this.undoButton.TabIndex = 3;
            this.undoButton.Text = "undo";
            this.undoButton.UseVisualStyleBackColor = true;
            this.undoButton.Click += new System.EventHandler(this.undoButton_Click);
            // 
            // invertButton
            // 
            this.invertButton.Location = new System.Drawing.Point(8, 84);
            this.invertButton.Name = "invertButton";
            this.invertButton.Size = new System.Drawing.Size(48, 23);
            this.invertButton.TabIndex = 2;
            this.invertButton.Text = "invert";
            this.invertButton.UseVisualStyleBackColor = true;
            this.invertButton.Click += new System.EventHandler(this.invertButton_Click);
            // 
            // selectNoneButton
            // 
            this.selectNoneButton.Location = new System.Drawing.Point(8, 55);
            this.selectNoneButton.Name = "selectNoneButton";
            this.selectNoneButton.Size = new System.Drawing.Size(48, 23);
            this.selectNoneButton.TabIndex = 1;
            this.selectNoneButton.Text = "none";
            this.selectNoneButton.UseVisualStyleBackColor = true;
            this.selectNoneButton.Click += new System.EventHandler(this.selectNoneButton_Click);
            // 
            // selectAllButton
            // 
            this.selectAllButton.Location = new System.Drawing.Point(8, 26);
            this.selectAllButton.Name = "selectAllButton";
            this.selectAllButton.Size = new System.Drawing.Size(48, 23);
            this.selectAllButton.TabIndex = 0;
            this.selectAllButton.Text = "all";
            this.selectAllButton.UseVisualStyleBackColor = true;
            this.selectAllButton.Click += new System.EventHandler(this.selectAllButton_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.elementList);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(495, 291);
            this.panel2.TabIndex = 6;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.filterTextBox);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(495, 32);
            this.panel1.TabIndex = 6;
            // 
            // filterTextBox
            // 
            this.filterTextBox.Location = new System.Drawing.Point(41, 6);
            this.filterTextBox.Name = "filterTextBox";
            this.filterTextBox.Size = new System.Drawing.Size(448, 20);
            this.filterTextBox.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Filter:";
            // 
            // elementList
            // 
            this.elementList.CheckOnClick = true;
            this.elementList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.elementList.FormattingEnabled = true;
            this.elementList.Location = new System.Drawing.Point(0, 32);
            this.elementList.Name = "elementList";
            this.elementList.Size = new System.Drawing.Size(495, 259);
            this.elementList.TabIndex = 7;
            this.elementList.SelectedIndexChanged += new System.EventHandler(this.elementList_SelectedIndexChanged);
            // 
            // CheckControl
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.groupBox1);
            this.Name = "CheckControl";
            this.Size = new System.Drawing.Size(557, 291);
            this.groupBox1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button selectNoneButton;
        private System.Windows.Forms.Button selectAllButton;
        private System.Windows.Forms.Button invertButton;
        private System.Windows.Forms.Button undoButton;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckedListBox elementList;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox filterTextBox;
        private System.Windows.Forms.Label label1;
    }
}
