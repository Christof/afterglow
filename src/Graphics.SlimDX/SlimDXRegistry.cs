using StructureMap.Configuration.DSL;

namespace TheNewEngine.Graphics.SlimDX
{
    /// <summary>
    /// Registry for SlimDX dependencies.
    /// </summary>
    public class SlimDXRegistry : Registry
    {
        /// <summary>
        /// Adds entries to the registry.
        /// </summary>
        protected override void configure()
        {
            CreateProfile("SlimDX")
                .For<IRenderWindow>().UseConcreteType<RenderWindow>();
        }
    }
}