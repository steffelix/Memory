

using UnityEngine;

namespace Memory.Models.States
{
    class BoardOnePreviewState : BoardStateBaseClass
    {
        public override BoardStates State => BoardStates.OnePreview;


        public BoardOnePreviewState(MemoryBoard board) : base(board)
        {

        }
        public override void AddPreview(Tile tile)
        {
            if (tile.State.State != TileStates.Hidden) return;

            tile.Board.PreviewingTiles.Add(tile);

            if(Board.IsCombinationFound)
            {
                foreach (var t in Board.PreviewingTiles)
                {
                    t.State = new TileFoundState(t);
                }

                DBCombination combination = new DBCombination();
                combination.Name = tile.MemoryCardId.ToString();
                //Debug.Log(combination.Name);
                CombinationFoundRepository repo = CombinationFoundRepository.Instance;
                repo.ProcessCombination(combination);

                Board.State = new BoardTwoFoundState(Board);
            }

            else
            {
                tile.State = new TilePreviewState(tile);
                tile.Board.State = new BoardTwoPreviewState(tile.Board);
            }
            
        }

        public override void TileAnimationEnded(Tile tile)
        {

        }
    }
}
