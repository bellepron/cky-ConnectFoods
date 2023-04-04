using cky.Reuseables.Helpers;
using cky.Reuseables.Level;
using ConnectFoods.Game;
using ConnectFoods.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConnectFoods.Grid.Managers
{
    public class ExplosionManager
    {
        public Action ExplosionIsOver;


        private readonly ICell[,] _grid;
        private readonly int _rows;
        private readonly int _cols;
        private readonly LevelSettings _levelSettings;



        public ExplosionManager(ICell[,] grid)
        {
            _grid = grid;
            _rows = _grid.GetLength(0);
            _cols = _grid.GetLength(1);
            _levelSettings = LevelManager.Instance.LevelSettings;
        }



        public void ExplodeClickedCellWithNeighbours(ICell cell, ICell[] cellsWillExplode)
        {
            foreach (var c in cellsWillExplode)
            {
                c.Explode();
            }
        }



        public void ExplodeConnectedCells(List<ICell> connectedCells)
        {
            foreach (ICell cell in connectedCells)
            {
                cell.IItem.IsFallableItem = false;
            }

            List<ICell> connectedCellsCached = new List<ICell>(connectedCells);
            StaticCoroutineCky.Perform(ExplodeWithOrder(connectedCellsCached));
        }



        IEnumerator ExplodeWithOrder(List<ICell> connectedCellsCached)
        {
            foreach (var cell in connectedCellsCached)
            {
                cell.Explode();

                yield return new WaitForSeconds(_levelSettings.ExplodeWithOrderDelay);
            }

            ExplosionIsOver?.Invoke();
        }
    }
}