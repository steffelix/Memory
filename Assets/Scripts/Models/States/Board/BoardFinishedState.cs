namespace Memory.Models.States
{
    class BoardFinishedState : BoardStateBaseClass
    {
        public override BoardStates State => BoardStates.Finished;


        public BoardFinishedState(MemoryBoard board) : base(board)
        {

        }
        public override void AddPreview(Tile tile)
        {


        }

        public override void TileAnimationEnded(Tile tile)
        {

        }
    }
}
