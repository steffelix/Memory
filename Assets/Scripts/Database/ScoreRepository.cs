using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ScoreRepository : Singleton<ScoreRepository>
{
    string urlMemoryScore = "http://localhost/MemoryWebservice/Memory/";

    public void ProcessScores(DBScore score)
    {
        StartCoroutine(PostScore(score));
    }

    private IEnumerator PostScore(DBScore score)
    {
       string json = JsonConvert.SerializeObject(score);
        UnityWebRequest uwr = UnityWebRequest.Put(urlMemoryScore,json);
        uwr.SetRequestHeader("Content-Type","application/json");
        uwr.method = "POST";
        yield return uwr.SendWebRequest();

        if(uwr.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Post Score error: " + uwr.error);

        }
        else
        {
            string returnJson = uwr.downloadHandler.text;
            DBScore returnScore = JsonConvert.DeserializeObject<DBScore>(returnJson);
            Debug.Log(returnScore.Name + " " + returnScore.Score1);
        }
    }
}
