using System;
using System.Linq;
using System.Windows.Forms;
using Afterglow.Applications.TestRunner.Components.Utils;

namespace Afterglow.Applications.TestRunner.Components
{
    public partial class TestRunBuilderForm : Form
    {
        private PriorityList<TabPage> mPriorities = new PriorityList<TabPage>();
        private TestRunnerStorage mStorage = new TestRunnerStorage();

        public TestRunBuilderForm()
        {
            InitializeComponent();

            mPriorities.Add(0, assembliesTab);
            mPriorities.Add(1, classesTab);
            mPriorities.Add(2, functionsTab);
            mPriorities.Add(3, categoriesTab);

            ReloadTabOrder();

            assemblyList.OnSelectedChanged = classList.OnSelectedChanged = functionList.OnSelectedChanged =
                                                                           categoryList.OnSelectedChanged =
                                                                           (checkControl, checkedObjects) =>
                                                                               {
                                                                                   mPriorities.Move(
                                                                                       (TabPage) checkControl.Parent, 0);
                                                                                   ReloadTabOrder();
                                                                               };
        }

        private void ReloadTabOrder()
        {
            tabControl.TabPages.Clear();
            for (int i = 0; i < mPriorities.Size; i++)
            {
                tabControl.TabPages.Insert(i, mPriorities[i]);
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
            foreach (Type testFixtureType in mStorage.TestFixtureTypes)
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
            ReloadCheckControls();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
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
                                                          ToFilterElements(categoryList))
                );
            runner.Run();
        }
    }
}