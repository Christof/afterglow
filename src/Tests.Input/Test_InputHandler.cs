using MbUnit.Framework;
using Moq;
using System;

namespace TheNewEngine.Input
{
    public class Test_InputHandler
    {
        [Test]
        public void FluentInterface()
        {
            Action action = () => { };

            var inputDevicesMock = new Mock<IInputDevices>();
            var inputDeviceMock = new Mock<IInputDevice>();

            var inputDevices = inputDevicesMock.Object;
            
            inputDevicesMock.SetupGet(i => i.Keyboard).Returns(inputDeviceMock.Object);
            inputDeviceMock.Setup(i => i.On(Button.W)).Returns(new ButtonAction(Button.W));
            
            inputDevices.Keyboard.On(Button.W).IsDown()
                .Do(action);
        }
    }
}