using StructureMap.Configuration.DSL;

namespace TheNewEngine.Graphics.SlimDX
{
    /// <summary>
    /// Registry for SlimDX dependencies.
    /// </summary>
    public class SlimDXRegistry : Registry
    {
        protected override void configure()
        {
            CreateProfile("SlimDX")
                .For<IRenderWindow>().UseConcreteType<RenderWindow>();
        }
    }
}