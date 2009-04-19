using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Afterglow.Infrastructure;
using MbUnit.Framework;
using WysiwygEditor.Components;

namespace Tests.Application.WysiwygEditor
{
    [TestFixture]
    public class Test_FileListControl
    {
        private ListControl<FileInfo> mFileListControl;
        private Form mForm;

        [SetUp]
        public void Setup()
        {
            mFileListControl = new ListControl<FileInfo>(
                () =>
                {
                    var openFileDialog = new OpenFileDialog();
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                        return (from filename in openFileDialog.FileNames 
                                select new FileInfo(filename)).ToArray();
                    return new FileInfo[0];
                }
                );

            mForm = new Form();
            mForm.Controls.Add(mFileListControl);
            mForm.Visible = true;
        }

        [Test]
        public void Test_items_are_empty_at_startup()
        {
            Assert.AreEqual(0, mFileListControl.Elements.Length);
            Assert.AreEqual(0, mFileListControl.SelectedIndices.Length);
        }
        [Test]
        public void Test_adding_exsting_file()
        {
            var fileInfo = new FileInfo(Path.GetTempFileName());
            using (var textWriter = fileInfo.CreateText())
                ;

            mFileListControl.AddElements(fileInfo.ToArrayWithOneElement());
            Assert.AreEqual(1, mFileListControl.Elements.Length);
            Assert.AreEqual(fileInfo.FullName, mFileListControl.Elements.First().FullName);

            fileInfo.Delete();
        }

        [Test]
        public void Test_add_and_remove_file_using_selection()
        {
            var fileInfo = new FileInfo(Path.GetTempFileName());
            using (var textWriter = fileInfo.CreateText())
                ;

            mFileListControl.AddElements(fileInfo.ToArrayWithOneElement());
            Assert.AreEqual(1, mFileListControl.Elements.Length);
            Assert.AreEqual(fileInfo.FullName, mFileListControl.Elements.First().FullName);

            fileInfo.Delete();
            uint i = 0;
            mFileListControl.SelectedIndices = i.ToArrayWithOneElement();
            Assert.AreEqual(1, mFileListControl.SelectedIndices.Length);

            mFileListControl.RemoveSelectedItems();
            Assert.AreEqual(0, mFileListControl.Elements.Length);
            Assert.AreEqual(0, mFileListControl.SelectedIndices.Length);
        }




    }
}
