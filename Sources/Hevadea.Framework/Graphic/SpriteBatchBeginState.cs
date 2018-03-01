using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Framework.Graphic
{
    public class SpriteBatchBeginState
    {
        public SpriteSortMode SortMode { get; set; } = SpriteSortMode.Deferred;
        public BlendState BlendState { get; set; } = null;
        public SamplerState SamplerState { get; set; } = null;
        public DepthStencilState DepthStencilState { get; set; } = null;
        public RasterizerState RasterizerState { get; set; } = null;
        public Effect Effect { get; set; } = null;
        public Matrix? TransformMatrix { get; set; } = null;
    }
}