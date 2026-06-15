using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class AnalyticsData
{
    public string sender;
    public string step;
    public string value;
    
    public AnalyticsData(string _sender, string _step, string _value)
    {
        sender = _sender;
        step = _step;
        value = _value;
    }
}

[System.Serializable]
public class AnalyticsFile
{
    public AnalyticsData[] data;
}

public class AnalyticsManager : MonoBehaviour
{
    public List<AnalyticsData> data;
    public static AnalyticsManager Instance;
    private string sessionGUID;
    private string urlForms = "https://docs.google.com/forms/d/e/1FAIpQLSdPwmp0SVDtcKgjxiQwkwKL8jFYxM8_Be2xjKy094aNGKG42Q/formResponse";

    void Awake()
    {
        Instance = this;
        sessionGUID = System.Guid.NewGuid().ToString();
    }

    public void AddAnalytics(string sender, string step, string value)
    {
        AnalyticsData d = new AnalyticsData(sender, step, value);
        Debug.Log("Sender: " + sender + " - Step: " + step + " - Value: " + value);
        data.Add(d);
    }

    public void Save()
    {
        AnalyticsFile f = new AnalyticsFile();
        f.data = data.ToArray();
        string json = JsonUtility.ToJson(f, true);
        SaveFile(json);
    }

    public void SaveFile(string text)
    {
        string path = Application.dataPath + "/analytics.txt";
        Debug.Log("Arquivo salvo em: " + path);
        File.WriteAllText(path, text);
    }
}
