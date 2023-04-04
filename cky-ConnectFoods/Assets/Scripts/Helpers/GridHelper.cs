using System.Collections.Generic;
using System;
using ConnectFoods.Enums;
using ConnectFoods.Grid;

namespace ConnectFoods.Helpers
{
    public static class GridHelper
    {


        public static void SetCellNeighbours(this ICell cell, ICell[,] grid, ref List<ICell> neighbours, ref ICell firstCellBelow)
        {
            neighbours = new List<ICell>();

            var up = cell.GetCellNeighbourWithDirection(grid, Direction.Up);
            var upRight = cell.GetCellNeighbourWithDirection(grid, Direction.UpRight);
            var right = cell.GetCellNeighbourWithDirection(grid, Direction.Right);
            var downRight = cell.GetCellNeighbourWithDirection(grid, Direction.DownRight);
            var down = cell.GetCellNeighbourWithDirection(grid, Direction.Down);
            var downLeft = cell.GetCellNeighbourWithDirection(grid, Direction.DownLeft);
            var left = cell.GetCellNeighbourWithDirection(grid, Direction.Left);
            var upLeft = cell.GetCellNeighbourWithDirection(grid, Direction.UpLeft);

            if (up != null) neighbours.Add(up);
            if (upRight != null) neighbours.Add(upRight);
            if (right != null) neighbours.Add(right);
            if (downRight != null) neighbours.Add(downRight);
            if (down != null) neighbours.Add(down);
            if (downLeft != null) neighbours.Add(downLeft);
            if (left != null) neighbours.Add(left);
            if (upLeft != null) neighbours.Add(upLeft);

            if (down != null) cell.FirstCellBelow = down;
        }


        private static ICell GetCellNeighbourWithDirection(this ICell cell, ICell[,] grid, Direction direction)
        {
            var rows = grid.GetLength(0);
            var cols = grid.GetLength(1);
            var y = cell.Row;
            var x = cell.Col;

            switch (direction)
            {
                case Direction.None:
                    break;
                case Direction.Up:
                    y += 1;
                    break;
                case Direction.UpRight:
                    y += 1;
                    x += 1;
                    break;
                case Direction.Right:
                    x += 1;
                    break;
                case Direction.DownRight:
                    y -= 1;
                    x += 1;
                    break;
                case Direction.Down:
                    y -= 1;
                    break;
                case Direction.DownLeft:
                    y -= 1;
                    x -= 1;
                    break;
                case Direction.Left:
                    x -= 1;
                    break;
                case Direction.UpLeft:
                    y += 1;
                    x -= 1;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("direction", direction, null);
            }

            if (x >= cols || x < 0 || y >= rows || y < 0) return null;

            return grid[y, x];
        }



    }
}