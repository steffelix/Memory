using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static readonly Lazy<T> LazyInstance = new Lazy<T>(CreateSingleton);

    public static T Instance => LazyInstance.Value;

    private static T CreateSingleton()
    {
        var ownerObject = new GameObject($"{typeof(T).Name} (singleton)");
        var instance = ownerObject.AddComponent<T>();
        DontDestroyOnLoad(ownerObject);
        return instance;
    }
}
public class ImageRepository : Singleton<ImageRepository>
{
    //string urlMemoryImages = "https://www.pd4memorywebservice.edu/api/Memory/";
    string urlMemoryImages = "http://www.pd4memorywebservice.edu/api/Memory/";

    //For in Swagger the / -> all shizzle
    public void ProcessImageIds(Action<List<int>> processIds, string theme)
    {
        StartCoroutine(GetImageIDs(processIds, theme));
    }

    private IEnumerator GetImageIDs(Action<List<int>> processIds, string theme)
    {
        UnityWebRequest uwrIds = UnityWebRequest.Get(urlMemoryImages+"Theme/"+theme);
        yield return uwrIds.SendWebRequest();
        if( uwrIds.result!= UnityWebRequest.Result.Success)
        {
            Debug.Log("ImageRepository.GetimageIDs: " + uwrIds.error);
        }
        else
        {

            string json = uwrIds.downloadHandler.text;
            List<DBImage> images = JsonConvert.DeserializeObject<List<DBImage>>(json);
            images = images.Where(i=>i.IsBack ==false).ToList();
            List<int> imageIds = images.Select(i=>i.Id).ToList();
            processIds(imageIds);
        }
    }

    
    public void GetProcessTexture(int imgId, Action<Texture2D> processTexture)
    {
        StartCoroutine(GetTexture(imgId, processTexture));
    }

    private IEnumerator GetTexture(int imgId, Action<Texture2D> processTexture)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(urlMemoryImages + imgId);
        yield return uwr.SendWebRequest();
        if (uwr.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("ImageRepository.GetimageIDs: " + uwr.error);
        }
        else
        {

            Texture2D texture = new Texture2D(20, 20);
            ImageConversion.LoadImage(texture, uwr.downloadHandler.data);
            processTexture(texture);
        }
    }


    //for in swagger the Back request
    public void GetProcessBack(Action<Texture2D> processTexture, string theme)
    {
        StartCoroutine(GetBack(processTexture, theme));
    }

    private IEnumerator GetBack( Action<Texture2D> processTexture, string theme)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(urlMemoryImages + "Back/"+theme);
        yield return uwr.SendWebRequest();
        if (uwr.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("ImageRepository.GetimageIDs: " + uwr.error);
        }
        else
        {
            //rubberen metjes
            Texture2D texture = new Texture2D(20, 20);
            ImageConversion.LoadImage(texture, uwr.downloadHandler.data);
            processTexture(texture);
        }
    }


    //private IEnumerator GetTheme(Action<string> process,int id)
    //{
    //    UnityWebRequest uwrIds = UnityWebRequest.Get(urlMemoryImages);
    //    yield return uwrIds.SendWebRequest();
    //    if (uwrIds.result != UnityWebRequest.Result.Success)
    //    {
    //        Debug.Log("ImageRepository.GetimageIDs: " + uwrIds.error);
    //    }
    //    else
    //    {

    //        string json = uwrIds.downloadHandler.text;
    //        List<DBImage> images = JsonConvert.DeserializeObject<List<DBImage>>(json);
    //        string imageIds = images.Where(i=> i.Id == id).Select(i => i.Theme).ToList();
    //        process(imageIds);
    //    }
    //}
}
