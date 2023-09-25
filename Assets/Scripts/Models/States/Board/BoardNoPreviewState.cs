namespace Memory.Models.States
{

    class BoardNoPreviewState : BoardStateBaseClass
    {
        public override BoardStates State => BoardStates.NoPreview;


        public BoardNoPreviewState(MemoryBoard board) : base(board)
        {

        }
        public override void AddPreview(Tile tile)
        {
            if (tile.State.State != TileStates.Hidden) return;

            tile.State = new TilePreviewState(tile);

            tile.Board.PreviewingTiles.Add(tile);

            tile.Board.State = new BoardOnePreviewState(tile.Board);
        }

        public override void TileAnimationEnded(Tile tile)
        {
            
        }
    }
}
