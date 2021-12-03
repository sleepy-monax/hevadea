using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Hevadea.Utils
{
    public class Maze
    {
        public class Cell
        {
            public int Id { get; set; }
            public int X { get; }
            public int Y { get; }

            public Cell(int id, int x, int y)
            {
                Id = id;
                X = x;
                Y = y;
            }
        }

        public class Door
        {
            public bool Open { get; set; } = false;

            public Cell Cell0 { get; }
            public Cell Cell1 { get; }

            public Door(Cell cell0, Cell cell1)
            {
                Cell0 = cell0;
                Cell1 = cell1;
            }
        }

        public Cell[,] Cells;
        public List<Door> Doors = new List<Door>();
        public int Width { get; }
        public int Height { get; }
        private readonly Random _rnd;

        public Maze(int width, int height, Random rnd)
        {
            _rnd = rnd;

            Width = width;
            Height = height;
            Cells = new Cell[Width, Height];

            var counter = 0;

            for (var x = 0; x < width; x++)
            for (var y = 0; y < height; y++)
                Cells[x, y] = new Cell(counter++, x, y);

            Generate();
        }

        public Door GetDoor(Cell cell0, Cell cell1)
        {
            foreach (var d in Doors)
                if (d.Cell0 == cell0 && d.Cell1 == cell1 ||
                    d.Cell0 == cell1 && d.Cell1 == cell0)
                    return d;

            return null;
        }

        public Cell GetCell(Point p)
        {
            return Cells[p.X, p.Y];
        }

        public bool IsGenerated()
        {
            var id = -1;

            for (var x = 0; x < Width; x++)
            for (var y = 0; y < Height; y++)
                if (id == -1)
                    id = Cells[x, y].Id;
                else if (id != Cells[x, y].Id) return false;

            return true;
        }

        private void ExtendRegion(int from, int to, Point where)
        {
            if (where.X < 0 || where.X >= Width || where.Y < 0 || where.Y >= Height) return;

            var c = GetCell(where);
            if (c.Id != from) return;
            c.Id = to;

            for (var i = 0; i < 4; i++)
            {
                var d = (Direction) i;
                var p = d.ToPoint();

                ExtendRegion(from, to, where + p);
            }
        }

        private void Generate()
        {
            while (!IsGenerated())
            {
                var dir = (Direction) _rnd.Next(0, 4);
                var c0 = new Point(_rnd.Next(0, Width), _rnd.Next(0, Height));
                var c1 = c0 + dir.ToPoint();

                if (c1.X >= 0 && c1.X < Width &&
                    c1.Y >= 0 && c1.Y < Height)
                {
                    var cc0 = GetCell(c0);
                    var cc1 = GetCell(c1);

                    if (cc0.Id != cc1.Id)
                    {
                        var door = GetDoor(cc0, cc1);

                        if (door == null)
                        {
                            door = new Door(cc0, cc1);
                            Doors.Add(door);
                            ExtendRegion(cc1.Id, cc0.Id, c1);
                        }

                        door.Open = true;
                    }
                }
            }
        }
    }
}