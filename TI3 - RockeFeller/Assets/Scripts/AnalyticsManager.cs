using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class AnalyticsManager : MonoBehaviour
{
    public static AnalyticsManager Instance { get; private set; }

    [SerializeField] private string appsScriptUrl = "https://script.google.com/macros/s/AKfycbywa230mtrWhC6qB1TQxNuQnJAXkptspBkTA6L7kkX2S_DbURZEUoDH82gwqQn-wQPcrw/exec";

    private string GuidAtual;

    public enum Area
    {
        Engineering,
        Programming,
        Biology,
        Medicine
    }

    [Serializable]
    public class AnalyticsPacket
    {
        public string guid;
        public UpdateData[] updates;
    }

    [Serializable]
    public class UpdateData
    {
        public string column;
        public string value;

        public UpdateData(string column, string value)
        {
            this.column = column;
            this.value = value;
        }
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void MarcarGameStarted()
    {
        GuidAtual = Guid.NewGuid().ToString("N");
        EnviarEvento("Game Started");
    }

    public void MarcarGameEnded()
    {
        EnviarEvento("Game Ended");
    }

    public void MarcarNPCInteragido(Area area)
    {
        EnviarEvento(GetAreaName(area) + " NPC Interacted");
    }

    public void MarcarPuzzleStarted(Area area)
    {
        EnviarEvento(GetAreaName(area) + " Puzzle Started");
    }

    public void MarcarPuzzleFinished(Area area)
    {
        EnviarEvento(GetAreaName(area) + " Puzzle Finished");
    }

    private string GetAreaName(Area area)
    {
        switch (area)
        {
            case Area.Engineering:
                return "Engineering";

            case Area.Programming:
                return "Programming";

            case Area.Biology:
                return "Biology";

            case Area.Medicine:
                return "Medicine";

            default:
                return "";
        }
    }

    private void EnviarEvento(string columnName)
    {
        string dataHora = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

        AnalyticsPacket packet = new AnalyticsPacket
        {
            guid = GuidAtual,
            updates = new UpdateData[]
            {
                new UpdateData(columnName, dataHora)
            }
        };

        StartCoroutine(Enviar(packet));
    }

    private IEnumerator Enviar(AnalyticsPacket packet)
    {
        string json = JsonUtility.ToJson(packet, true);
        byte[] body = Encoding.UTF8.GetBytes(json);

        using UnityWebRequest request = new UnityWebRequest(appsScriptUrl, "POST");

        request.uploadHandler = new UploadHandlerRaw(body);
        request.downloadHandler = new DownloadHandlerBuffer();

        request.SetRequestHeader("Content-Type", "text/plain;charset=utf-8");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogWarning("Erro ao Enviar analytics: " + request.error);
        }
        else
        {
            Debug.Log("Analytics enviado: " + request.downloadHandler.text);
        }
    }
}