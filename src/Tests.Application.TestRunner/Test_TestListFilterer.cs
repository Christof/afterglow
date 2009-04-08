using System;
using Afterglow.Applications.TestRunner;
using Afterglow.Infrastructure;
using MbUnit.Framework;

namespace Tests.Application.TestRunner
{
    [TestFixture]
    public class Test_TestListFilterer
    {
        private string[] mAssemblies = new[] { "assembly1", "assembly2" };
        private string[] mTypes = new[] { "type1", "type2" };
        private string[] mFunctions = new[] { "function1", "function2" };
        private string[] mCategories = new[] { "category1", "category2" };

        private TestListFilterer mFilterer;

        [SetUp]
        public void Setup()
        {
            mFilterer = new TestListFilterer(
                () => mAssemblies,
                () => mTypes,
                () => mFunctions,
                () => mCategories);
        }

        [TearDown]
        public void Clean()
        {
            ;
        }

        [Test]
        public void Test_null_is_handled_like_a_normal_item()
        {
            Setup();

            bool inFilter = mFilterer.Check(null, null, null, null);
            inFilter.ShouldEqual(false);
        }

        [Test]
        public void Test_is_in_filter_if_only_assembly_matches()
        {
            Setup();

            bool inFilter = mFilterer.Check("assembly1", null, null, null);
            inFilter.ShouldEqual(true);
        }

        [Test]
        public void Test_is_in_filter_if_only_type_matches()
        {
            Setup();

            bool inFilter = mFilterer.Check(null, "type1", null, null);
            inFilter.ShouldEqual(true);
        }

        [Test]
        public void Test_is_in_filter_if_only_function_matches()
        {
            Setup();

            bool inFilter = mFilterer.Check(null, null, "function1", null);
            inFilter.ShouldEqual(true);
        }

        [Test]
        public void Test_is_in_filter_if_only_category_matches()
        {
            Setup();

            bool inFilter = mFilterer.Check(null, null, null, "category1");
            inFilter.ShouldEqual(true);
        }

        [Test]
        public void Test_is_in_filter_if_two_match()
        {
            Setup();

            bool inFilter = mFilterer.Check(null, null, "function1", "category1");
            inFilter.ShouldEqual(true);
        }
    }
}