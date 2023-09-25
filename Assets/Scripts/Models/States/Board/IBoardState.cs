namespace Memory.Models.States
{
    interface IBoardState
    {
        public BoardStates State { get; }

        public MemoryBoard Board { get; set; }

        public void AddPreview(Tile tile);

        public void TileAnimationEnded(Tile tile);
    }
}
