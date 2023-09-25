using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;


namespace Memory.views
{
    class MemoryBoardView : ViewBaseClass<MemoryBoard>
    {
        private PlayerView _firstPlayer;

        public PlayerView FirstPlayer
        {
            get => _firstPlayer;
            set
            {
                _firstPlayer = value;
                Model.FirstPlayer = value.Model;
            }
        }

        private PlayerView _secondPlayer;

        public PlayerView SecondPlayer
        {
            get => _secondPlayer;
            set
            {
                _secondPlayer = value;
                Model.SecondPlayer = value.Model;
            }
        }

        
        protected override void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            
        }

        private void Update()
        {
          if(FirstPlayer.Model.IsActive)
            {
                FirstPlayer.Model.Elapsed += Time.deltaTime;
            }
          else if (SecondPlayer.Model.IsActive)
            {
                SecondPlayer.Model.Elapsed += Time.deltaTime;
            }
        }

        public void SetUpMemoryBoardView(MemoryBoard model, GameObject tilePrefab)//, string theme)
        {
            Model = model;
            //Model.Theme = theme;
            foreach (var t in Model.Tiles)
            {
                var xPos = (t.Column - (Model.Columns / 2f) + .5f) * 2.1f;
                var zPos = -(t.Row - (Model.Rows / 2f) + .5f) * 2.1f;
                var location = new Vector3(xPos, 0, zPos);
                GameObject newTile = Instantiate(tilePrefab, location, Quaternion.Euler(-90, 0, 0), this.transform);
                var tileView = newTile.GetComponent<TileView>();
                tileView.Model = t;
                tileView.Theme = Model.Theme;
                //newTile.transform.GetChild(1).GetComponent<Renderer>().material = materials[t.MemoryCardId];
            }
        }
    }
}

