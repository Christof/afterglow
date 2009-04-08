using System.Windows.Forms;
using Afterglow.Applications.TestRunner.Components;
using MbUnit.Framework;

namespace Tests.Application.TestRunner
{
    [TestFixture]
    public class Test_CheckControl
    {
        private CheckControl checkControl;

        private int elementTotalCount;

        private Form form;

        [SetUp]
        public void Setup()
        {
            checkControl = new CheckControl();
            var elements = new[]
            {
                "Element 1",
                "Element 2",
                "Element 3"
            };
            checkControl.AddElements(elements);
            elementTotalCount = elements.Length;

            form = new Form();
            form.Controls.Add(checkControl);
            form.Visible = true;
        }

        [TearDown]
        public void Clean()
        {
            checkControl.ClearElements();
        }

        [Test]
        public void Test_check_all_checks_all()
        {
            Setup();

            checkControl.SetCheckedForAll(true);

            Assert.AreEqual(elementTotalCount, checkControl.CheckedElements.Length);
        }

        [Test]
        public void Test_setting_filter_text_changes_checked_items()
        {
            Setup();

            checkControl.FilterText = "3";

            Assert.AreEqual(1, checkControl.CheckedElements.Length);
        }

        [Test]
        public void Test_setting_filter_text_to_empty_string_does_not_change_checked_items()
        {
            Setup();

            checkControl.SetCheckedForAll(true);
            checkControl.FilterText = string.Empty;

            Assert.AreEqual(elementTotalCount, checkControl.CheckedElements.Length);

            checkControl.SetCheckedForAll(false);
            checkControl.FilterText = string.Empty;

            Assert.AreEqual(0, checkControl.CheckedElements.Length);
        }

        [Test]
        public void Test_filtering_everything_away()
        {
            Setup();

            checkControl.SetCheckedForAll(true);
            checkControl.FilterText = "xxx";

            Assert.AreEqual(0, checkControl.CheckedElements.Length);
        }

        [Test]
        public void Test_check_all_and_check_none_and_undo_checks_all()
        {
            Setup();

            checkControl.SetCheckedForAll(true);
            checkControl.SetCheckedForAll(false);
            checkControl.UndoLastAction();

            Assert.AreEqual(elementTotalCount, checkControl.CheckedElements.Length);
        }

        [Test]
        public void Test_check_all_and_undo_checks_none()
        {
            Setup();

            checkControl.SetCheckedForAll(true);
            checkControl.UndoLastAction();

            Assert.AreEqual(0, checkControl.CheckedElements.Length);
        }

        [Test]
        public void Test_single_undo_passes()
        {
            Setup();

            checkControl.UndoLastAction();
        }
    }
}