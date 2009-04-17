using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Afterglow.Applications.TestRunner.Components.Utils;
using Afterglow.Infrastructure;
using MbUnit.Framework;
using CategoryAttribute=System.ComponentModel.CategoryAttribute;

namespace Afterglow.Applications.TestRunner.Components
{
    public partial class TestRunBuilderForm : Form
    {
        private PriorityList<TabPage> mPriorities = new PriorityList<TabPage>();
        private TestRunnerStorage mStorage = new TestRunnerStorage();

        public TestRunBuilderForm()
        {
            InitializeComponent();

            mPriorities.Add(0, this.assembliesTab);
            mPriorities.Add(1, this.classesTab);
            mPriorities.Add(2, this.functionsTab);
            mPriorities.Add(3, this.categoriesTab);

            this.ReloadTabOrder();

            assemblyList.OnSelectedChanged = classList.OnSelectedChanged = functionList.OnSelectedChanged =
                categoryList.OnSelectedChanged = (checkControl, checkedObjects) =>
                                                 {
                                                     mPriorities.Move((TabPage)checkControl.Parent, 0);
                                                     ReloadTabOrder();
                                                 };
        }

        private void ReloadTabOrder()
        {
            this.tabControl.TabPages.Clear();
            for(int i = 0; i < this.mPriorities.Size; i++)
            {
                this.tabControl.TabPages.Insert(i, mPriorities[i]);
            }
        }

        private void ReloadCheckControls()
        {
            assemblyList.ClearElements();
            classList.ClearElements();

            assemblyList.AddElements(mStorage.AssemblyNames);
            classList.AddElements(mStorage.TestFixtureTypes);

            functionList.ClearElements();
            categoryList.ClearElements();
            foreach (var testFixtureType in mStorage.TestFixtureTypes)
            {
                try
                {
                    functionList.AddElements(
                        from testinfo in mStorage[testFixtureType]
                        select testinfo.Key);

                    categoryList.AddElements(
                        from testInfo in mStorage[testFixtureType]
                        select testInfo.Value);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        private void reloadButton_Click(object sender, EventArgs e)
        {
            new TestAssemblyLoader().LoadToStorage("Tests.OpenTK.Graphics.dll", mStorage);
            this.ReloadCheckControls();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private Func<string[]> ToFilterElements(CheckControl checkControl)
        {
            return () =>
                   (from checkedElement in checkControl.CheckedElements.ToArray()
                   select checkedElement.ToString()).ToArray();
        }

        private void runButton_Click(object sender, EventArgs e)
        {
            var runner = new TestListRunner(mStorage, new TestListFilterer(
                                                          ToFilterElements(assemblyList),
                                                          ToFilterElements(classList),
                                                          ToFilterElements(functionList),
                                                          ToFilterElements(categoryList) )
                                                          );
            runner.Run();
        }
    }
}
