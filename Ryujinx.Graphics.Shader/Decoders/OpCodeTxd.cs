using Ryujinx.Graphics.Shader.Instructions;

namespace Ryujinx.Graphics.Shader.Decoders
{
    class OpCodeTxd : OpCodeTexture
    {
        public bool IsBindless { get; }

        public OpCodeTxd(InstEmitter emitter, ulong address, long opCode) : base(emitter, address, opCode)
        {
            HasOffset = opCode.Extract(35);

            IsBindless = opCode.Extract(54);

            LodMode = TextureLodMode.None;
        }
    }
}