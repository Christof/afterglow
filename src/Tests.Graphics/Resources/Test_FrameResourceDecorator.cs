using MbUnit.Framework;
using Moq;

namespace TheNewEngine.Graphics.Resources
{
    [TestFixture]
    public class Test_FrameResourceDecorator
    {
        [Test]
        public void Constructor()
        {
            var decorator = new FrameResourceDecorator();

            Assert.IsNotNull(decorator);
        }

        [Test]
        public void Load_sets_decoree()
        {
            var decorator = new FrameResourceDecorator();

            var decoree = new Mock<IFrameResource>().Object;
            decorator.Load(decoree);

            Assert.AreEqual(decoree, decorator.Decoree);
        }

        [Test]
        public void Load_calls_Load_of_decorre_and_passes_the_decorator()
        {
            var decorator = new FrameResourceDecorator();

            var decoreeMock = new Mock<IFrameResource>();
            decoreeMock.Expect(d => d.Load(decorator));

            decorator.Load(decoreeMock.Object);

            decoreeMock.VerifyAll();
        }

        [Test]
        public void Unload_calls_Unload_of_Decoree()
        {
            var decorator = new FrameResourceDecorator();

            var decoreeMock = new Mock<IFrameResource>();
            decoreeMock.Expect(d => d.Unload());
            decorator.Load(decoreeMock.Object);

            decorator.Unload();

            decoreeMock.VerifyAll();
        }

        [Test]
        public void OnFrame_calls_OnFrame_of_Decoree()
        {
            var decorator = new FrameResourceDecorator();

            var decoreeMock = new Mock<IFrameResource>();
            decoreeMock.Expect(d => d.OnFrame());
            decorator.Load(decoreeMock.Object);

            decorator.OnFrame();

            decoreeMock.VerifyAll();
        }
    }
}