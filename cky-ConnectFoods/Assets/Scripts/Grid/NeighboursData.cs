namespace ConnectFoods.Grid
{
    public struct NeighboursData
    {
        public Cell up, down, right, left, upright, upleft, downright, downleft;
        public NeighboursData(Cell up, Cell down, Cell right, Cell left, Cell upright, Cell upleft, Cell downright, Cell downleft)
        {
            this.up = up;
            this.down = down;
            this.right = right;
            this.left = left;
            this.upright = upright;
            this.upleft = upleft;
            this.downright = downright;
            this.downleft = downleft;
        }
    }
}