namespace Memory.Models.States
{
    class BoardTwoPreviewState : BoardStateBaseClass
    {
        public override BoardStates State => BoardStates.TwoPreview;


        public BoardTwoPreviewState(MemoryBoard board) : base(board)
        {

        }
        public override void AddPreview(Tile tile)
        {
           

        }

        public override void TileAnimationEnded(Tile tile)
        {
            if(tile != Board.PreviewingTiles[0] && tile == Board.PreviewingTiles[1])
            {
                foreach (var t in Board.PreviewingTiles)
                {
                    t.State = new TileHiddenState(t);
                }
                Board.State = new BoardTwoHidingState(Board);
            }
        }
    }
}
