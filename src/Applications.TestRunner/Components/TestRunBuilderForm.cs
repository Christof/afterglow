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

        public void ReloadStorage()
        {
            string currentDirectory = Application.StartupPath;
            var directory = new DirectoryInfo(currentDirectory);
            var assemblyFiles = new FileInfo[0];
           
            assemblyFiles = directory.GetFiles("Tests.Graphics.OpenTK.dll", SearchOption.TopDirectoryOnly);

            foreach (var assemblyFile in assemblyFiles)
            {
                var types = new Type[0];
                try
                {
                    var assembly = Assembly.LoadFrom(assemblyFile.FullName);
                    types = assembly.GetExportedTypes();
                }
                catch(Exception ex)
                {
                    Trace.WriteLine(ex.GetType().Name);
                    Trace.WriteLine(ex.Message);
                    continue;
                }
                foreach (var type in types)
                {
                    try
                    {
                        var methods = type.GetMethods();
                        foreach (var methodInfo in methods)
                        {
                            var testAttributes = methodInfo.GetCustomAttributes(typeof(TestAttribute), true);
                            if (testAttributes.Length > 0)
                            {
                                string category = null;
                                var categoryAttributes = methodInfo.GetCustomAttributes(typeof(CategoryAttribute), true);
                                if (categoryAttributes.Length > 0)
                                {
                                    var categoryAttribute = (CategoryAttribute)categoryAttributes[0];
                                    category = categoryAttribute.Category;
                                }


                                mStorage.AddTestMethod(assemblyFile.Name, type, methodInfo, category);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Trace.WriteLine(ex.GetType().Name);
                        Trace.WriteLine(ex.Message);
                    }
                }
            }

            ReloadCheckControls();
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
                        mStorage[testFixtureType].TransformCollection(
                            testInfo => testInfo.First));
                    categoryList.AddElements(
                        mStorage[testFixtureType].TransformCollection(
                            testInfo => testInfo.Second));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        private void reloadButton_Click(object sender, EventArgs e)
        {
            this.ReloadStorage();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void runButton_Click(object sender, EventArgs e)
        {
            var runner = new TestListRunner(mStorage, new TestListFilterer(
                () => this.assemblyList.CheckedElements.TransformCollection(
                    (checkedElement) => checkedElement.ToString()).ToArray(),
                () => this.classList.CheckedElements.TransformCollection(
                    (checkedElement) => checkedElement.ToString()).ToArray(),
                () => this.functionList.CheckedElements.TransformCollection(
                    (checkedElement) => checkedElement.ToString()).ToArray(),
                () => this.categoryList.CheckedElements.TransformCollection(
                    (checkedElement) => checkedElement.ToString()).ToArray()
                )
            );
            runner.Run();
        }
    }
}
