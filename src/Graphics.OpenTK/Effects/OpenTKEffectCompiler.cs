using System;
using OpenTK.Graphics;

namespace Afterglow.Graphics.Effects
{
    /// <summary>
    /// Xna effect compiler.
    /// </summary>
    public class OpenTKEffectCompiler : IEffectCompiler, IDisposable
    {
        private int mProgramHandle; //TODO: make more than one program possible...

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenTKEffectCompiler"/> class.
        /// </summary>
        public OpenTKEffectCompiler()
        {
            ;
        }

        /// <summary>
        /// Compiles an effect file specified trough the given path.
        /// </summary>
        /// <param name="path">The path to the effect file.</param>
        /// <returns>The compiled effect.</returns>
        public IEffect Compile(string path)
        {
            string source = new System.IO.StreamReader(path).ReadToEnd();

            int effectHandle = GL.CreateShader(ShaderType.VertexShader); //TODO ShaderType.FragmentShader
            GL.ShaderSource(effectHandle, source); //change the source code of the shader behind the handle
            GL.CompileShader(effectHandle);

            int statusCode;
            string info;

            GL.GetShaderInfoLog(effectHandle, out info);
            GL.GetShader(effectHandle, ShaderParameter.CompileStatus, out statusCode);

            if (statusCode != 1)
            {
                throw new Exception(string.Format("Error while loading shader: {0}, {1}", source, info));
            }

            this.mProgramHandle = GL.CreateProgram();
            GL.AttachShader(mProgramHandle, effectHandle);

            GL.LinkProgram(mProgramHandle); //TODO: Just move this into a kind of an EffectBinder like the BufferBinder
            GL.UseProgram(mProgramHandle);

            return new OpenTKEffect(effectHandle);
        }

        #region IDisposable Members

        public void Dispose()
        {
            GL.DeleteProgram(this.mProgramHandle); //TODO: move this to another place...
        }

        #endregion
    }
}