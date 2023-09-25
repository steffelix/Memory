namespace Memory.Models.States
{
    class TileFoundState : TileStateBaseClass
    {
        public override TileStates State => TileStates.Found;

        public TileFoundState(Tile tile) : base(tile)
        {

        }
    }
}
