using Maker.Rise.Ressource;
using System;

namespace Maker.Hevadea.Game.Tiles
{
    public class GrassTile : Tile
    {
        Random rnd = new Random();

        public GrassTile(byte id) : base(id)
        {
            Sprite = new Sprite(Ressources.tile_tiles, 2);
        }
    }
}