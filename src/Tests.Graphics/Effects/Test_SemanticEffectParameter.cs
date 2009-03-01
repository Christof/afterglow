using MbUnit.Framework;
using Moq;

namespace Afterglow.Graphics.Effects
{
    public class Test_SemanticEffectParameter
    {
        [Test]
        public void SemanticName()
        {
            string semanticName = "name";
            var mock = new Mock<SemanticEffectParameter<float>>(semanticName);
            var parameter = mock.Object;

            Assert.AreEqual(semanticName, parameter.SemanticName);
        }

        [Test]
        public void Value_can_be_set_and_get()
        {
            var time = 11.32f;
            var mock = new Mock<SemanticEffectParameter<float>>(MockBehavior.Strict, "Time");
            mock.SetupSet(p => p.Value, time);
            mock.SetupGet(p => p.Value).Returns(time);
            var parameter = mock.Object;
            
            parameter.Value = time;

            Assert.AreEqual(time, parameter.Value);
        }
    }
}