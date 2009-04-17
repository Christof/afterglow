namespace Afterglow.Applications.TestRunner.Components
{
    partial class SimpleRunBuilderForm
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
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Assembly = new System.Windows.Forms.GroupBox();
            this.mSelectAssemblyButton = new System.Windows.Forms.Button();
            this.mAssemblyTextBox = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.mRunButton = new System.Windows.Forms.Button();
            this.mTraceListenerControl = new Afterglow.Applications.TestRunner.Components.TraceListenerControl();
            this.panel2.SuspendLayout();
            this.Assembly.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            this.openFileDialog.Filter = "Assembly File|*.dll";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Controls.Add(this.Assembly);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(609, 100);
            this.panel2.TabIndex = 8;
            // 
            // Assembly
            // 
            this.Assembly.Controls.Add(this.mSelectAssemblyButton);
            this.Assembly.Controls.Add(this.mAssemblyTextBox);
            this.Assembly.Dock = System.Windows.Forms.DockStyle.Top;
            this.Assembly.Location = new System.Drawing.Point(0, 0);
            this.Assembly.Name = "Assembly";
            this.Assembly.Size = new System.Drawing.Size(609, 43);
            this.Assembly.TabIndex = 8;
            this.Assembly.TabStop = false;
            this.Assembly.Text = "Assembly";
            // 
            // mSelectAssemblyButton
            // 
            this.mSelectAssemblyButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.mSelectAssemblyButton.Location = new System.Drawing.Point(576, 16);
            this.mSelectAssemblyButton.Name = "mSelectAssemblyButton";
            this.mSelectAssemblyButton.Size = new System.Drawing.Size(27, 23);
            this.mSelectAssemblyButton.TabIndex = 1;
            this.mSelectAssemblyButton.Text = "...";
            this.mSelectAssemblyButton.UseVisualStyleBackColor = true;
            this.mSelectAssemblyButton.Click += new System.EventHandler(this.mSelectAssemblyButton_Click_1);
            // 
            // mAssemblyTextBox
            // 
            this.mAssemblyTextBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.mAssemblyTextBox.Location = new System.Drawing.Point(3, 16);
            this.mAssemblyTextBox.Name = "mAssemblyTextBox";
            this.mAssemblyTextBox.ReadOnly = true;
            this.mAssemblyTextBox.Size = new System.Drawing.Size(497, 20);
            this.mAssemblyTextBox.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.mRunButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 52);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(609, 48);
            this.panel1.TabIndex = 9;
            // 
            // mRunButton
            // 
            this.mRunButton.Location = new System.Drawing.Point(10, 13);
            this.mRunButton.Name = "mRunButton";
            this.mRunButton.Size = new System.Drawing.Size(75, 23);
            this.mRunButton.TabIndex = 0;
            this.mRunButton.Text = "Run";
            this.mRunButton.UseVisualStyleBackColor = true;
            this.mRunButton.Click += new System.EventHandler(this.mRunButton_Click_1);
            // 
            // mTraceListenerControl
            // 
            this.mTraceListenerControl.BackColor = System.Drawing.SystemColors.Desktop;
            this.mTraceListenerControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mTraceListenerControl.Location = new System.Drawing.Point(0, 100);
            this.mTraceListenerControl.Name = "mTraceListenerControl";
            this.mTraceListenerControl.Size = new System.Drawing.Size(609, 146);
            this.mTraceListenerControl.TabIndex = 9;
            // 
            // SimpleRunBuilderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 246);
            this.Controls.Add(this.mTraceListenerControl);
            this.Controls.Add(this.panel2);
            this.Name = "SimpleRunBuilderForm";
            this.Text = "SimpleRunBuilder";
            this.panel2.ResumeLayout(false);
            this.Assembly.ResumeLayout(false);
            this.Assembly.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button mRunButton;
        private System.Windows.Forms.GroupBox Assembly;
        private System.Windows.Forms.Button mSelectAssemblyButton;
        private System.Windows.Forms.TextBox mAssemblyTextBox;
        private TraceListenerControl mTraceListenerControl;
    }
}