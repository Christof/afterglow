using MbUnit.Framework;
using Moq;
using System;

namespace Afterglow.Input
{
    public class Test_InputHandler
    {
        [Test]
        public void FluentInterface_for_buttons()
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

        [Test]
        public void FluentInterface_for_axes()
        {
            Action<int> action = delta => { };

            var inputDevicesMock = new Mock<IInputDevices>();
            var axesInputDevice = new Mock<IAxesInputDevice>();

            var inputDevices = inputDevicesMock.Object;

            inputDevicesMock.SetupGet(i => i.Mouse).Returns(axesInputDevice.Object);
            axesInputDevice.Setup(i => i.On(Axis.X)).Returns(new AxisAction(Axis.X));

            inputDevices.Mouse.On(Axis.X).Do(action);
        }
    }
}