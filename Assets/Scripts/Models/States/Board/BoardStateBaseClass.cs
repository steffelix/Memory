namespace Memory.Models.States
{
    abstract class BoardStateBaseClass : IBoardState
    {
        public abstract BoardStates State { get; }


        private MemoryBoard _board;

        public MemoryBoard Board
        {
            get => _board;
            set
            {
                if (_board == value) return;
                _board = value;
            }
        }

        public BoardStateBaseClass(MemoryBoard board)
        {
            Board = board;
        }

        public abstract void AddPreview(Tile tile);

        public abstract void TileAnimationEnded(Tile tile);
        
    }
}