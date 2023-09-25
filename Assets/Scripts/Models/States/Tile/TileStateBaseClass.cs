namespace Memory.Models.States
{
    abstract class TileStateBaseClass : ITileState
    {
        public abstract TileStates State { get; }


        private Tile _tile;

        public Tile Tile
        {
            get => _tile;
            set
            {
                if (_tile == value) return;
                _tile = value;
            }
        }

        public TileStateBaseClass(Tile tile)
        {
            tile = Tile;
        }

    }
}
