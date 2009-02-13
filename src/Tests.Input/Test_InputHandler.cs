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
            var buttonStateMock = new Mock<IButtonState>();
            var buttonActionMock = new Mock<IButtonAction>();

            var inputDevices = inputDevicesMock.Object;
            
            inputDevicesMock.SetupGet(i => i.Keyboard).Returns(inputDeviceMock.Object);
            inputDeviceMock.Setup(i => i.On(Button.W)).Returns(buttonStateMock.Object);
            buttonStateMock.Setup(i => i.IsDown()).Returns(buttonActionMock.Object);
            buttonActionMock.Setup(i => i.Do(action));
            
            inputDevices.Keyboard.On(Button.W).IsDown()
                .Do(action);
        }
    }
}