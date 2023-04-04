using UnityEngine;

namespace cky.Reuseables.Utils
{
    public static class MatrixUtils
    {
        public static bool IsCellInGridBounds(int row, int col, int rows, int cols)
        {
            if (row >= rows || row < 0 || col >= cols || col < 0)
                return false;

            return true;
        }
    }
}