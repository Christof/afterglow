using MbUnit.Framework;

namespace TheNewEngine.Infrastructure
{
    public class Test_DependencyResolver
    {
        public interface IContract
        {
            string ReturnParameter();
        }

        public class Implementation : IContract
        {
            public string ReturnParameter()
            {
                return "Hello";
            }
        }

        [Test]
        public void Resolve()
        {
            var resolve = DependencyResolver.Resolve<IContract>();

            Assert.IsNotNull(resolve);
        }

        public class ImplementationWithParameter : IContract
        {
            private readonly string mParameter;

            public ImplementationWithParameter(string parameter)
            {
                mParameter = parameter;
            }

            public string ReturnParameter()
            {
                return mParameter;
            }
        }

        [Test]
        public void Constructor_with_not_injected_parameter()
        {
            var parameter = "param";
            
            var implementation = DependencyResolver.ResolveWith<IContract>("parameter", parameter);

            Assert.IsNotNull(implementation);
            Assert.AreEqual(parameter, implementation.ReturnParameter());
        }

        [Test]
        public void Constructor_with_not_injected_parameter_with_profile()
        {
            var parameter = "param";
            
            var implementation = DependencyResolver.ResolveWith<IContract>("parameter", parameter);

            Assert.IsNotNull(implementation);
            Assert.AreEqual(parameter, implementation.ReturnParameter());
        }

        [Test]
        public void Constructor_with_not_injected_generic_parameter()
        {
            var parameter = "param";

            var implementation = DependencyResolver.ResolveWith<IContract, string>(parameter);

            Assert.IsNotNull(implementation);
            Assert.AreEqual(parameter, implementation.ReturnParameter());
        }
    }
}