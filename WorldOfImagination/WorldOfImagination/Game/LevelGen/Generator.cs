using Maker.Rise.Utils;
using System;
using System.Drawing;
using System.Windows.Forms;
using WorldOfImagination.Game.Tiles;

namespace WorldOfImagination.Game.LevelGen
{
    public class Generator
    {

        public int Seed = 0;

        public Level GenerateEmptyLevel(int worldSize = 256)
        {
            var l = new Level(worldSize, worldSize);
            var b = new Bitmap(worldSize, worldSize);
            var rnd = new Random(Seed);

            for (int x = 0; x < worldSize; x++)
            {
                for (int y = 0; y < worldSize; y++)
                {
                    var groundLevel = Perlin.OctavePerlin(x / 50f, y / 50f, Seed ^ Seed, 10, 0.5);
                    var biomsVariant = Perlin.OctavePerlin(x / 20f, y / 20f, Seed ^ Seed, 2, 0.5);

                    var v = (int)(255 * groundLevel) & 0xff;
                    b.SetPixel(x, y, Color.FromArgb(v, v, v));

                    if (groundLevel > 1)
                    {

                        if (biomsVariant > 0.7)
                        {
                            l.SetTile(new TilePosition(x, y), Tile.Grass.ID, 0);
                        }
                        else
                        {
                            l.SetTile(new TilePosition(x, y), Tile.Rock.ID, 0);
                        }

                    }
                    else if (groundLevel > 0.9)
                    {
                        if (biomsVariant > 0.5)
                        {
                            l.SetTile(new TilePosition(x, y), Tile.Grass.ID, 0);
                        }
                        else
                        {
                            l.SetTile(new TilePosition(x, y), Tile.Sand.ID, 0);
                        }
                    }
                    else
                    {
                        l.SetTile(new TilePosition(x, y), Tile.Water.ID, 0);
                    }
                    
                }
            }
            //new Form() { BackgroundImage = b, BackgroundImageLayout = ImageLayout.Zoom }.Show();
            return l;
        }

    }
}
