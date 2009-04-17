using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Afterglow.Applications.TestRunner.Components
{
    public partial class SimpleRunBuilderForm : Form
    {
        public SimpleRunBuilderForm()
        {
            InitializeComponent();
            mTraceListenerControl.Clear();
            mTraceListenerControl.RegisterListener();
        }

        private void mSelectAssemblyButton_Click_1(object sender, EventArgs e)
        {
            if (this.openFileDialog.ShowDialog() == DialogResult.OK)
                mAssemblyTextBox.Text = openFileDialog.FileName;

        }

        private void mRunButton_Click_1(object sender, EventArgs e)
        {
            var filename = openFileDialog.FileName;

            if (File.Exists(filename))
            {
                var storage = new TestRunnerStorage();
                new TestAssemblyLoader().LoadToStorage(filename, storage);
                storage.WriteToTrace();
                new TestListRunner(storage, null).Run();
            }

        }
    }
}
