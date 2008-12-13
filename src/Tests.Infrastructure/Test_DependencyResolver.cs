using MbUnit.Framework;
using StructureMap;

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
            ObjectFactory.Initialize(c => c.ForRequestedType<IContract>()
                .TheDefaultIsConcreteType<Implementation>());

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
            ObjectFactory.Initialize(x => x.ForRequestedType<IContract>()
                .TheDefault.Is.OfConcreteType<ImplementationWithParameter>()
                .WithCtorArg("parameter"));

            var implementation = DependencyResolver.ResolveWith<IContract>("parameter", parameter);

            Assert.IsNotNull(implementation);
            Assert.AreEqual(parameter, implementation.ReturnParameter());
        }

        [Test]
        public void Constructor_with_not_injected_parameter_with_profile()
        {
            var parameter = "param";
            ObjectFactory.Initialize(x => x.ForRequestedType<IContract>()
                .TheDefault.Is.OfConcreteType<ImplementationWithParameter>()
                .WithCtorArg("parameter"));

            var implementation = DependencyResolver.ResolveWith<IContract>("parameter", parameter);

            Assert.IsNotNull(implementation);
            Assert.AreEqual(parameter, implementation.ReturnParameter());
        }

        [Test]
        public void Constructor_with_not_injected_parameter2()
        {
            var parameter = "param";
            ObjectFactory.Initialize(x => x.ForRequestedType<IContract>()
                .TheDefault.Is.OfConcreteType<ImplementationWithParameter>()
                .WithCtorArg("parameter"));

            var implementation = DependencyResolver.ResolveWith<IContract, string>(parameter);

            Assert.IsNotNull(implementation);
            Assert.AreEqual(parameter, implementation.ReturnParameter());
        }
    }
}