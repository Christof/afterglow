namespace Afterglow.Applications.TestRunner.Components
{
    partial class TestRunBuilderForm
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.assembliesTab = new System.Windows.Forms.TabPage();
            this.assemblyList = new Afterglow.Applications.TestRunner.Components.CheckControl();
            this.classesTab = new System.Windows.Forms.TabPage();
            this.classList = new Afterglow.Applications.TestRunner.Components.CheckControl();
            this.functionsTab = new System.Windows.Forms.TabPage();
            this.functionList = new Afterglow.Applications.TestRunner.Components.CheckControl();
            this.categoriesTab = new System.Windows.Forms.TabPage();
            this.categoryList = new Afterglow.Applications.TestRunner.Components.CheckControl();
            this.reloadButton = new System.Windows.Forms.Button();
            this.runButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.tabControl.SuspendLayout();
            this.assembliesTab.SuspendLayout();
            this.classesTab.SuspendLayout();
            this.functionsTab.SuspendLayout();
            this.categoriesTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.assembliesTab);
            this.tabControl.Controls.Add(this.classesTab);
            this.tabControl.Controls.Add(this.functionsTab);
            this.tabControl.Controls.Add(this.categoriesTab);
            this.tabControl.Location = new System.Drawing.Point(4, 3);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(336, 189);
            this.tabControl.TabIndex = 0;
            // 
            // assembliesTab
            // 
            this.assembliesTab.Controls.Add(this.assemblyList);
            this.assembliesTab.Location = new System.Drawing.Point(4, 22);
            this.assembliesTab.Name = "assembliesTab";
            this.assembliesTab.Padding = new System.Windows.Forms.Padding(3);
            this.assembliesTab.Size = new System.Drawing.Size(328, 163);
            this.assembliesTab.TabIndex = 0;
            this.assembliesTab.Text = "Assemblies";
            this.assembliesTab.UseVisualStyleBackColor = true;
            // 
            // assemblyList
            // 
            this.assemblyList.AllowDrop = true;
            this.assemblyList.AllowDuplicates = false;
            this.assemblyList.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.assemblyList.FilterText = "";
            this.assemblyList.Location = new System.Drawing.Point(3, 3);
            this.assemblyList.Name = "assemblyList";
            this.assemblyList.OnSelectedChanged = null;
            this.assemblyList.Priority = ((long)(0));
            this.assemblyList.Size = new System.Drawing.Size(320, 157);
            this.assemblyList.TabIndex = 0;
            // 
            // classesTab
            // 
            this.classesTab.Controls.Add(this.classList);
            this.classesTab.Location = new System.Drawing.Point(4, 22);
            this.classesTab.Name = "classesTab";
            this.classesTab.Padding = new System.Windows.Forms.Padding(3);
            this.classesTab.Size = new System.Drawing.Size(328, 163);
            this.classesTab.TabIndex = 1;
            this.classesTab.Text = "Classes";
            this.classesTab.UseVisualStyleBackColor = true;
            // 
            // classList
            // 
            this.classList.AllowDrop = true;
            this.classList.AllowDuplicates = false;
            this.classList.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.classList.FilterText = "";
            this.classList.Location = new System.Drawing.Point(3, 3);
            this.classList.Name = "classList";
            this.classList.OnSelectedChanged = null;
            this.classList.Priority = ((long)(0));
            this.classList.Size = new System.Drawing.Size(320, 157);
            this.classList.TabIndex = 1;
            // 
            // functionsTab
            // 
            this.functionsTab.Controls.Add(this.functionList);
            this.functionsTab.Location = new System.Drawing.Point(4, 22);
            this.functionsTab.Name = "functionsTab";
            this.functionsTab.Padding = new System.Windows.Forms.Padding(3);
            this.functionsTab.Size = new System.Drawing.Size(328, 163);
            this.functionsTab.TabIndex = 2;
            this.functionsTab.Text = "Functions";
            this.functionsTab.UseVisualStyleBackColor = true;
            // 
            // functionList
            // 
            this.functionList.AllowDrop = true;
            this.functionList.AllowDuplicates = false;
            this.functionList.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.functionList.FilterText = "";
            this.functionList.Location = new System.Drawing.Point(3, 3);
            this.functionList.Name = "functionList";
            this.functionList.OnSelectedChanged = null;
            this.functionList.Priority = ((long)(0));
            this.functionList.Size = new System.Drawing.Size(320, 157);
            this.functionList.TabIndex = 2;
            // 
            // categoriesTab
            // 
            this.categoriesTab.Controls.Add(this.categoryList);
            this.categoriesTab.Location = new System.Drawing.Point(4, 22);
            this.categoriesTab.Name = "categoriesTab";
            this.categoriesTab.Padding = new System.Windows.Forms.Padding(3);
            this.categoriesTab.Size = new System.Drawing.Size(328, 163);
            this.categoriesTab.TabIndex = 3;
            this.categoriesTab.Text = "Catagories";
            this.categoriesTab.UseVisualStyleBackColor = true;
            // 
            // categoryList
            // 
            this.categoryList.AllowDrop = true;
            this.categoryList.AllowDuplicates = false;
            this.categoryList.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.categoryList.FilterText = "";
            this.categoryList.Location = new System.Drawing.Point(3, 3);
            this.categoryList.Name = "categoryList";
            this.categoryList.OnSelectedChanged = null;
            this.categoryList.Priority = ((long)(0));
            this.categoryList.Size = new System.Drawing.Size(320, 157);
            this.categoryList.TabIndex = 2;
            // 
            // reloadButton
            // 
            this.reloadButton.Location = new System.Drawing.Point(4, 198);
            this.reloadButton.Name = "reloadButton";
            this.reloadButton.Size = new System.Drawing.Size(75, 23);
            this.reloadButton.TabIndex = 1;
            this.reloadButton.Text = "Reload";
            this.reloadButton.UseVisualStyleBackColor = true;
            this.reloadButton.Click += new System.EventHandler(this.reloadButton_Click);
            // 
            // runButton
            // 
            this.runButton.Location = new System.Drawing.Point(181, 198);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(75, 23);
            this.runButton.TabIndex = 2;
            this.runButton.Text = "Run";
            this.runButton.UseVisualStyleBackColor = true;
            this.runButton.Click += new System.EventHandler(this.runButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(262, 198);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 3;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(86, 198);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 4;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Title = "Save Test Run Config";
            // 
            // TestRunBuilderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(340, 223);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.runButton);
            this.Controls.Add(this.reloadButton);
            this.Controls.Add(this.tabControl);
            this.Name = "TestRunBuilderForm";
            this.Text = "TestRun Builder";
            this.tabControl.ResumeLayout(false);
            this.assembliesTab.ResumeLayout(false);
            this.classesTab.ResumeLayout(false);
            this.functionsTab.ResumeLayout(false);
            this.categoriesTab.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage assembliesTab;
        private System.Windows.Forms.TabPage classesTab;
        private CheckControl assemblyList;
        private System.Windows.Forms.TabPage functionsTab;
        private System.Windows.Forms.TabPage categoriesTab;
        private CheckControl classList;
        private CheckControl functionList;
        private CheckControl categoryList;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.Button reloadButton;
        private System.Windows.Forms.Button runButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}