namespace Memory.Models.States
{
    class BoardTwoHidingState : BoardStateBaseClass
    {
        public override BoardStates State => BoardStates.TwoHiding;


        public BoardTwoHidingState(MemoryBoard board) : base(board)
        {

        }
        public override void AddPreview(Tile tile)
        {


        }

        public override void TileAnimationEnded(Tile tile)
        {
            Board.PreviewingTiles.Remove(tile);
            if (Board.PreviewingTiles.Count == 0)
            {
                Board.State = new BoardNoPreviewState(Board);
            }
        }
    }
}
