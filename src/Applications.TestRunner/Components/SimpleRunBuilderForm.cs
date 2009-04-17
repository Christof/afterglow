using System;
using System.Diagnostics;
using System.IO;
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
            if (openFileDialog.ShowDialog() == DialogResult.OK)
                mAssemblyTextBox.Text = openFileDialog.FileName;
        }

        private void mRunButton_Click_1(object sender, EventArgs e)
        {
            string filename = openFileDialog.FileName;

            if (File.Exists(filename))
            {
                var storage = new TestRunnerStorage();
                new TestAssemblyLoader().LoadToStorage(filename, storage);
                storage.WriteToTrace();
                new TestListRunner(storage, null).Run();
            }
        }

        private void SimpleRunBuilderForm_Load(object sender, EventArgs e)
        {
            const string DEFAULT_ASS = "/Users/josefkoller/Desktop/bin/Tests.Graphics.OpenTK.dll";
            if (File.Exists(DEFAULT_ASS))
            {
                mAssemblyTextBox.Text = openFileDialog.FileName = DEFAULT_ASS;
            }
            else
            {
                Trace.WriteLine(string.Format("default assembly {0} not found", DEFAULT_ASS));
            }
        }
    }
}