namespace Memory.Models
{
    class Player : ModelBaseClass
    {
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        private int _score;
        public int Score
        {
            get => _score;
            set
            {
                _score = value;
                OnPropertyChanged();
            }
        }

        private bool _isActive;
        public bool IsActive
        {
            get => _isActive;
            set
            {
                _isActive = value;
                OnPropertyChanged();
            }
        }

        private float _elapsed;
        public float Elapsed
        {
            get => _elapsed;
            set
            {
                
                _elapsed = value;
                OnPropertyChanged();
            }
        }

        public int mm
        {
            get => (int)(Elapsed / 60);
        }

        public int ss
        {
            get => (int)(Elapsed % 60);
        }

        public int ms
        {
            get => (int)((Elapsed % 1) * 1000);
        }

        public Player()
        {

        }
    }
}