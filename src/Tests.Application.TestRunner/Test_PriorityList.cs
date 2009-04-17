using System.Windows.Forms;
using Afterglow.Applications.TestRunner.Components;
using Afterglow.Applications.TestRunner.Components.Utils;
using Afterglow.Infrastructure;
using MbUnit.Framework;

namespace Tests.Application.TestRunner
{
    [TestFixture]
    public class Test_PriorityList
    {
        private PriorityList<string> mPriorityList;

        [SetUp]
        public void Setup()
        {
            mPriorityList = new PriorityList<string>();
            var elements = new[]
            {
                "Element 1",
                "Element 2",
                "Element 3"
            };
            mPriorityList.AddRange(elements);
        }

        [TearDown]
        public void Clean()
        {
            mPriorityList.Clear();
        }

        [Test]
        public void Test_adding_an_element_to_first_position_shifts_all_back()
        {
            Setup();

            mPriorityList.Add( 0, "Element 0");
            mPriorityList[1].ShouldEqual("Element 1");
        }

        [Test]
        public void Test_adding_an_element_to_last_position_changes_length()
        {
            Setup();

            mPriorityList.Add(3, "Element 4");
            mPriorityList.Size.ShouldEqual(4);
        }

        [Test]
        public void Test_adding_item_to_first_index_using_index_operator_changes_length_and_shifts_others()
        {
            Setup();

            mPriorityList[0] = "Element 0";
            mPriorityList.Size.ShouldEqual(4);
            mPriorityList[1].ShouldEqual("Element 1");
        }


        [Test]
        public void Test_adding_item_to_last_index_using_index_operator_changes_length_and_shifts_last_item()
        {
            Setup();

            mPriorityList[mPriorityList.Size-1] = "Element 4";
            mPriorityList.Size.ShouldEqual(4);
            mPriorityList[mPriorityList.Size-1].ShouldEqual("Element 3");
        }

        [Test]
        public void Test_adding_item_to_new_index_using_index_operator_changes_length()
        {
            Setup();

            mPriorityList[mPriorityList.Size] = "Element 4";
            mPriorityList.Size.ShouldEqual(4);
            mPriorityList[mPriorityList.Size - 1].ShouldEqual("Element 4");
        }

        [Test]
        public void Test_adding_item_far_after_the_end_adds_item_to_the_end()
        {
            Setup();

            mPriorityList[mPriorityList.Size + 2] = "Element 4";
            mPriorityList.Size.ShouldEqual(4);
            mPriorityList[mPriorityList.Size - 1].ShouldEqual("Element 4");

        }
    }
}