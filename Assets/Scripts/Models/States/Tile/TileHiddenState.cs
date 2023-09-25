namespace Memory.Models.States
{
    class TileHiddenState : TileStateBaseClass
    {
        public override TileStates State => TileStates.Hidden;

        public TileHiddenState(Tile tile) : base(tile)
        {

        }
    }
}