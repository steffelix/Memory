using ExtensionMethods;
using Memory.Models;
using Memory.Models.States;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class MemoryBoard : ModelBaseClass
{

   private System.Random random = new System.Random();

    private int _rows;
    public int Rows
    {
        get { return _rows; }
        set
        {
            if (_rows == value) return;
            _rows = value;
            OnPropertyChanged();
        }
    }
    private int _columns;

    public int Columns
    {
        get { return _columns; }
        set
        {
            if (_columns == value) return;
            _columns = value;
            OnPropertyChanged();
            
        }
    }
    private List<Tile> _tiles = new List<Tile>();

    public List<Tile> Tiles
    {
        get { return _tiles; }
        set
        {
            if (_tiles == value) return;
            _tiles = value;
            OnPropertyChanged();
        }
    }

    private List<Tile> _previewingTiles = new List<Tile>();

    public List<Tile> PreviewingTiles
    {
        get { return _previewingTiles; }
        set
        {
            if (_previewingTiles == value) return;
            _previewingTiles = value;
            OnPropertyChanged();
        }
    }

    public bool WasCombinationFound;
    public bool IsCombinationFound
    {
        get
        {
            if (PreviewingTiles.Count == 2 && PreviewingTiles[0].MemoryCardId == PreviewingTiles[1].MemoryCardId)
            {
                return true;
            }
            return false;
        }
    }

    private Player _firstPlayer;

    public Player FirstPlayer
    {
        get => _firstPlayer;
        set
        {
            if (_firstPlayer == value) return;
            _firstPlayer = value;
            OnPropertyChanged();
        }
    }

    private Player _secondPlayer;

    public Player SecondPlayer
    {
        get => _secondPlayer;
        set
        {
            if (_secondPlayer == value) return;
            _secondPlayer = value;
            OnPropertyChanged();
        }
    }

    private string _theme;

    public string Theme
    {
        get => _theme;
        set => _theme = value;
        
    }

    private IBoardState _state;

    public IBoardState State
    {
        get => _state;
        set
        {
            if (_state == value) return;
            _state = value;
            OnPropertyChanged();

            if (FirstPlayer != null && SecondPlayer != null)
            { if (value.GetType() == typeof(BoardNoPreviewState))
                    ToggleActivePlayer(false);


                if (value.GetType() == typeof(BoardFinishedState))
                {
                    ToggleActivePlayer(true);

                    DBPlayersScore playersScore = new DBPlayersScore();
                    playersScore.PlayerName = FirstPlayer.Name;
                    playersScore.PlayerScore = FirstPlayer.Score;
                    PlayersScoreRepository repo = PlayersScoreRepository.Instance;
                    repo.ProcessPlayersScore(playersScore);

                    Debug.Log("playersscore posted" + playersScore.PlayerScore + playersScore.PlayerName);


                    DBPlayersScore playersScore2 = new DBPlayersScore();
                    playersScore2.PlayerName = SecondPlayer.Name;
                    playersScore2.PlayerScore = SecondPlayer.Score;
                    PlayersScoreRepository repo2 = PlayersScoreRepository.Instance;
                    repo2.ProcessPlayersScore(playersScore2);

                    Debug.Log("playersscore2 posted" + playersScore2.PlayerScore + playersScore2.PlayerName);
                }
                   
                if (value.GetType() == typeof(BoardTwoFoundState))
                {
                    if (FirstPlayer.IsActive)
                    {
                        FirstPlayer.Score++;
                    }
                    else if (SecondPlayer.IsActive)
                    {
                        SecondPlayer.Score++;
                    }
                }



            } 
        }
    }



    private void ToggleActivePlayer(bool isFinished)
    {
        if(isFinished)
        {
            FirstPlayer.IsActive = false;
            SecondPlayer.IsActive = false;
        }
        else
        {
            FirstPlayer.IsActive = !FirstPlayer.IsActive;
            SecondPlayer.IsActive = !SecondPlayer.IsActive;
        }
        
    }

    public MemoryBoard(int rows, int columns, string theme)
    {
        Rows = rows;
        Columns = columns;
        Theme = theme;
        for (int r = 0; r < Rows; r++)
        {
            for (int c = 0; c < Columns; c++)
            {
                Tiles.Add(new Tile(r, c, this));
            }
        }
        
        AssignMemoryCardIds();

        State = new BoardNoPreviewState(this);
    }

    private void AssignMemoryCardIds()
    {
        ImageRepository repo = ImageRepository.Instance;
        repo.ProcessImageIds(AssignMemoryCardIds, Theme);
    }

    private void AssignMemoryCardIds(List<int> memoryCardIds)
    {
        memoryCardIds = memoryCardIds.Shuffle();
        List<Tile> shuffledTiles = Tiles.Shuffle();



        int maxMemoryIDs = /*random.Next(1, 4)*/1;
        

        int memoryCardID = 0;
        int loopThroughInt = 0;
        bool first = true;
        foreach (var tile in shuffledTiles)
        {
            
            if (loopThroughInt < maxMemoryIDs)
            {
                tile.MemoryCardId = memoryCardIds[memoryCardID];
                if (first)
                {
                    first = false;
                }
                else
                {
                    loopThroughInt++;
                    first = true;
                }
            }
            else
            {
                first = false;
                loopThroughInt = 0;
                memoryCardID++;
                tile.MemoryCardId = memoryCardIds[memoryCardID];
            }
        }
    }

    //private void AssignMemoryCardIds()
    //{
    //    int index = 0;
    //    var tempList = Tiles.GetRange(0, Tiles.Count);
    //    while (tempList.Count > 0)
    //    {
    //        var first = tempList[Random.Range(0, tempList.Count)];
    //        tempList.Remove(first);
    //        first.MemoryCardId = index;

    //        if(tempList.Count > 0)
    //        {
    //            var second = tempList[Random.Range(0, tempList.Count)];
    //            tempList.Remove(second);
    //            second.MemoryCardId = index;
    //        }

    //        index++;
    //    }

    //}

    public override string ToString()
    {
        return $"MemoryBoard({Rows}, {Columns})";
    }





}
