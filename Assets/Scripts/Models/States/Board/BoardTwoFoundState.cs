using System.Linq;

namespace Memory.Models.States
{
    class BoardTwoFoundState : BoardStateBaseClass
    {
        public override BoardStates State => BoardStates.TwoFound;


        public BoardTwoFoundState(MemoryBoard board) : base(board)
        {

        }
        public override void AddPreview(Tile tile)
        {


        }

        public override void TileAnimationEnded(Tile tile)
        {
            if(tile != Board.PreviewingTiles[0] && tile == Board.PreviewingTiles[1])
            {
                Board.PreviewingTiles.Clear();
                if(Board.PreviewingTiles.Count == 0)
                {
                    if(Board.Tiles.Where(t => t.State.State == TileStates.Hidden).Count() < 2)
                    {
                        Board.State = new BoardFinishedState(Board);
                    }
                    else
                    {
                        Board.State = new BoardNoPreviewState(Board);
                        Board.WasCombinationFound = true;
                    }
                }
            }
        }
    }
}
