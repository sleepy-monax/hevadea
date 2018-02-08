using System;
using Maker.Rise.Ressource;

namespace Maker.Hevadea.Game.Tiles
{
    public class GrassTile : Tile
    {
        private Random rnd = new Random();

        public GrassTile(byte id) : base(id)
        {
            Sprite = new Sprite(Ressources.TileTiles, 2);
        }
    }
}