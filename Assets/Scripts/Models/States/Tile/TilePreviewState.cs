namespace Memory.Models.States
{
    class TilePreviewState : TileStateBaseClass
    {
        public override TileStates State => TileStates.Preview;

        public TilePreviewState(Tile tile) : base(tile)
        {

        }
    }
}
