using System.Collections.Generic;
using WorldOfImagination.Framework.Primitive;

namespace WorldOfImagination.Framework.Graphics.SpriteSheets
{
    public class Sprite
    {
        public float FrameTime;
        public List<Rectangle> SourcesRectangles;

        public Sprite()
        {
            FrameTime = 1f;
            SourcesRectangles = new List<Rectangle>();
        }
        public Sprite(float frameTime, List<Rectangle> sourcesRectangles)
        {
            FrameTime = frameTime;
            SourcesRectangles = sourcesRectangles;
        }
    }
}