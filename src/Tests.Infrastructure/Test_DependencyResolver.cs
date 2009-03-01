using Autofac.Builder;
using MbUnit.Framework;

namespace Afterglow.Infrastructure
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
            var builder = new ContainerBuilder();
            builder.Register<Implementation>().As<IContract>();
            DependencyResolver.SetContainer(builder.Build());

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
        public void Constructor_with_not_injected_named_parameter()
        {
            var parameter = "param";
            var builder = new ContainerBuilder();
            builder.Register<ImplementationWithParameter>()
                .As<IContract>();
            DependencyResolver.SetContainer(builder.Build());

            var implementation = DependencyResolver.ResolveWith<IContract>("parameter", parameter);

            Assert.IsNotNull(implementation);
            Assert.AreEqual(parameter, implementation.ReturnParameter());
        }

        public class ImplementationA : IContract
        {
            private readonly string mName;

            public ImplementationA(string name)
            {
                mName = name;
            }

            public string ReturnParameter()
            {
                return mName + "A";
            }
        }

        public class ImplementationB : IContract
        {
            private readonly string mName;

            public ImplementationB(string name)
            {
                mName = name;
            }

            public string ReturnParameter()
            {
                return mName + "B";
            }
        }

        public class ModuleA : Module
        {
            /// <summary>
            /// Override to add registrations to the container.
            /// </summary>
            /// <param name="builder">The builder.</param>
            protected override void Load(ContainerBuilder builder)
            {
                builder.Register<ImplementationA>().As<IContract>();
            }
        }

        public class ModuleB : Module
        {
            /// <summary>
            /// Override to add registrations to the container.
            /// </summary>
            /// <param name="builder">The builder.</param>
            protected override void Load(ContainerBuilder builder)
            {
                builder.Register<ImplementationB>().As<IContract>();
            }
        }

        [Test]
        public void Constructor_with_not_injected_named_parameter_with_modules()
        {
            var parameter = "param";
            var builder = new ContainerBuilder();
            builder.RegisterModule(new ModuleA());
            DependencyResolver.SetContainer(builder.Build());

            var implementation = DependencyResolver.ResolveWith<IContract>("name", parameter);

            Assert.IsNotNull(implementation);
            Assert.AreEqual(parameter + "A", implementation.ReturnParameter());

            builder = new ContainerBuilder();
            builder.RegisterModule(new ModuleB());
            DependencyResolver.SetContainer(builder.Build());

            implementation = DependencyResolver.ResolveWith<IContract>("name", parameter);

            Assert.IsNotNull(implementation);
            Assert.AreEqual(parameter + "B", implementation.ReturnParameter());
        }

        [Test]
        public void Constructor_with_not_injected_generic_parameter()
        {
            var parameter = "param";
            var builder = new ContainerBuilder();
            builder.Register<ImplementationWithParameter>()
                .As<IContract>();
            DependencyResolver.SetContainer(builder.Build());

            var implementation = DependencyResolver.ResolveWith<IContract, string>(parameter);

            Assert.IsNotNull(implementation);
            Assert.AreEqual(parameter, implementation.ReturnParameter());
        }
    }
}