using Maker.Hevadea.Game.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Maker.Hevadea.Test
{
    [TestClass]
    public class TilePosition
    {
        [TestMethod]
        public void TilePositionTest()
        {
            Assert.IsFalse(new Game.Tiles.TilePosition(1000, 1000) != new Game.Tiles.TilePosition(1000, 1000));
            Assert.IsTrue(new Game.Tiles.TilePosition(0, 0) == new Game.Tiles.TilePosition(0, 0));
            Assert.IsTrue(new Game.Tiles.TilePosition(0, 0) != new Game.Tiles.TilePosition(1, 1));
            Assert.IsTrue(new Game.Tiles.TilePosition(1256, 56465) != new Game.Tiles.TilePosition(0, 0));

            var e = new Entity();

            var oldPos = e.GetTilePosition();
            e.SetPosition(1000, 1000);

            var newPos = e.GetTilePosition();

            Assert.IsTrue(oldPos != newPos, "Entity moving");
        }
    }
}