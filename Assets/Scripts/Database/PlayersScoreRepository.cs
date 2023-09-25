using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayersScoreRepository : Singleton<PlayersScoreRepository>
{

    //string urlMemoryCoordinate = "https://www.pd4memorywebservice.edu/api/Memory/";
    string urlMemoryPlayersScore = "http://www.pd4memorywebservice.edu/api/Memory/";



    public void ProcessPlayersScore(DBPlayersScore playersScore)
    {
        StartCoroutine(PostPlayersScore(playersScore));
    }

    private IEnumerator PostPlayersScore(DBPlayersScore playersScore)
    {
        string json = JsonConvert.SerializeObject(playersScore);
        UnityWebRequest uwr = UnityWebRequest.Put(urlMemoryPlayersScore + "PlayersScore/", json);
        uwr.SetRequestHeader("content-type", "application/json");
        uwr.method = "POST";
        yield return uwr.SendWebRequest();

        if (uwr.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Post PlayersScore error: " + uwr.error);
        }
        else
        {
            string returnJson = uwr.downloadHandler.text;
            DBPlayersScore returnPlayersScore = JsonConvert.DeserializeObject<DBPlayersScore>(returnJson);
            Debug.Log("Player Name:" + returnPlayersScore.PlayerName + "Player Score:" + returnPlayersScore.PlayerScore);
        }

    }
}
