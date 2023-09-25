using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CoordinateRepository : Singleton<CoordinateRepository>
{

    //string urlMemoryCoordinate = "https://www.pd4memorywebservice.edu/api/Memory/";
    string urlMemoryCoordinate = "http://www.pd4memorywebservice.edu/api/Memory/";



    public void ProcessCoordinates(DBCoordinate coordinate)
    {
        StartCoroutine(PostCoordinate(coordinate));
    }

    private IEnumerator PostCoordinate(DBCoordinate coordinate)
    {
        string json = JsonConvert.SerializeObject(coordinate);
        UnityWebRequest uwr = UnityWebRequest.Put(urlMemoryCoordinate, json);
        uwr.SetRequestHeader("content-type", "application/json");
        uwr.method = "POST";
        yield return uwr.SendWebRequest();

        if (uwr.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Post Coordinate error: " + uwr.error);
        }
        else
        {
            string returnJson = uwr.downloadHandler.text;
            DBCoordinate returnCoordinate = JsonConvert.DeserializeObject<DBCoordinate>(returnJson);
            //Debug.Log("Coordinate: (" + returnCoordinate.RowNumbers + "," + returnCoordinate.ColumnNumbers + ")");
        }

    }
}
