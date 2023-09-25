using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel;
using Memory.views;
using System.Linq;
using Memory.Models;

public class MemoryGame : MonoBehaviour
{
    [SerializeField] private GameObject _tilePrefab;
    [SerializeField] private GameObject _memoryBoard;

    [SerializeField] private string _theme;

    private List<PlayerView> _players = new List<PlayerView>();


    private MemoryBoard _board;
    private void Start()
    {
        _players = FindObjectsOfType<PlayerView>().ToList();
        for (int i = 0; i < _players.Count; i++)
        {
            _players[i].Model = new Player();
            _players[i].Model.Name = $"Player{ i + 1}";
            _players[i].Model.Score = 0;
            _players[i].Model.IsActive = i == 0;
            _players[i].Model.Elapsed = 0f;
        }
        _board = new MemoryBoard(3, 3, _theme);

        var boardview = _memoryBoard.GetComponent<MemoryBoardView>();
        boardview.SetUpMemoryBoardView(_board, _tilePrefab);
        boardview.FirstPlayer = _players[0];
        boardview.SecondPlayer = _players[1];
    }
   
}
