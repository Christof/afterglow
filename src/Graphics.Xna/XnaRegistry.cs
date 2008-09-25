using StructureMap.Configuration.DSL;

namespace TheNewEngine.Graphics.Xna
{
    /// <summary>
    /// Registry for Xna dependencies.
    /// </summary>
    public class XnaRegistry : Registry
    {
        /// <summary>
        /// Configures the Xna-implementations.
        /// </summary>
        protected override void configure()
        {
            ForRequestedType<IRenderWindow>()
                .TheDefault.Is.OfConcreteType<RenderWindow>()
                .WithCtorArg("control");

//            CreateProfile("Xna")
//                .For<IRenderWindow>().UseConcreteType<RenderWindow>();
        }
    }
}