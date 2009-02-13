namespace TheNewEngine.Input
{
    public interface IInputDevice
    {
        IButtonState On(Button button);
    }
}