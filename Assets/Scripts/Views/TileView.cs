using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Memory.views
{
    class TileView : ViewBaseClass<Tile>, IPointerDownHandler
    {
        private string _theme;

        public string Theme
        {
            set { _theme = value; }
        }
        private Animator _animator;

        private int _index;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            AddEvents();
            LoadBack();
        }


        public void OnPointerDown(PointerEventData eventData)
        {
            Model.Board.State.AddPreview(Model);
            
            if(Model.Board.PreviewingTiles.Contains(Model) == true)
            {
                if (Model.Board.PreviewingTiles.Count == 1)
                {
                    if (_index >= 1)
                    {
                        DBCoordinate coordinate = new DBCoordinate();
                        coordinate.ColumnNumbers = Model.Column + 1;
                        coordinate.RowNumbers = Model.Row + 1;
                        CoordinateRepository repo = CoordinateRepository.Instance;
                        repo.ProcessCoordinates(coordinate);

                        //Debug.Log("Coordinate posted" + coordinate.ColumnNumbers + coordinate.RowNumbers);
                    }
                    else
                        _index++;
                }
                
            }

            

            if (Model.Board.PreviewingTiles.Contains(Model) ==false)
            {

                DBCoordinate coordinate = new DBCoordinate();
                coordinate.ColumnNumbers = Model.Column + 1;
                coordinate.RowNumbers = Model.Row + 1;
                CoordinateRepository repo = CoordinateRepository.Instance;
                repo.ProcessCoordinates(coordinate);

                //Debug.Log("Coordinate posted");

            }
           

        }

        protected override void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(Model.State)))
            {
             
                StartAnimation();
            }
            else if (e.PropertyName.Equals(nameof(Model.MemoryCardId)))
            {
                LoadFront();

            }
        }

        private void StartAnimation()
        {
            switch (Model.State.State)
            {
                case Models.States.TileStates.Hidden:
                    _animator.Play("Hidden");
                    break;
                case Models.States.TileStates.Preview:
                    _animator.Play("Shown");
                    break;
                case Models.States.TileStates.Found:
                    _animator.Play("Shown");
                    break;
                default:
                    break;
            }
        }

        private void AddEvents()
        {
            for (int i = 0; i < _animator.runtimeAnimatorController.animationClips.Length; i++)
            {
                AnimationClip clip = _animator.runtimeAnimatorController.animationClips[i];

                AnimationEvent animationEndEvent = new AnimationEvent();
                animationEndEvent.time = clip.length;
                animationEndEvent.functionName = "AnimationCompleteHandler";
                animationEndEvent.stringParameter = clip.name;

                clip.AddEvent(animationEndEvent);
            }

        }

        
        public void AnimationCompleteHandler(string name)
        {
            Model.Board.State.TileAnimationEnded(Model);
        }

        private void LoadFront()
        {
            ImageRepository.Instance.GetProcessTexture(Model.MemoryCardId, LoadFront);
        }

        private void LoadFront(Texture2D texture)
        {
            gameObject.transform.Find("Back").GetComponent<Renderer>().material.mainTexture = texture;
        }

        private void LoadBack()
        {
            ImageRepository.Instance.GetProcessBack(LoadBack, _theme);
        }

        private void LoadBack(Texture2D texture)
        {
            gameObject.transform.Find("Tile").GetComponent<Renderer>().material.mainTexture = texture;
        }
    }
}
