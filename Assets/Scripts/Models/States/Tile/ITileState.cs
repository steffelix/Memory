namespace Memory.Models.States
{
    interface ITileState
    {
        public TileStates State { get; }

        public Tile Tile { get; set; }

    } 
}
