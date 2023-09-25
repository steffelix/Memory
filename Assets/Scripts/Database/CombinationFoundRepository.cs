using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CombinationFoundRepository : Singleton<CombinationFoundRepository>
{

    //string urlMemoryCoordinate = "https://www.pd4memorywebservice.edu/api/Memory/";
    string urlMemoryCombination = "http://www.pd4memorywebservice.edu/api/Memory/";



    public void ProcessCombination(DBCombination combination)
    {
        StartCoroutine(PostCombination(combination));
    }

    private IEnumerator PostCombination(DBCombination combination)
    {
        string json = JsonConvert.SerializeObject(combination);
        UnityWebRequest uwr = UnityWebRequest.Put(urlMemoryCombination + "Combination/", json);
        uwr.SetRequestHeader("content-type", "application/json");
        uwr.method = "POST";
        yield return uwr.SendWebRequest();

        if (uwr.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Post Combination error: " + uwr.error);
        }
        else
        {
            string returnJson = uwr.downloadHandler.text;
            DBCombination returnCombination = JsonConvert.DeserializeObject<DBCombination>(returnJson);
            //Debug.Log("Combination:"  + returnCombination.Name);
        }

    }
}
