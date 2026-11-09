using System;
using System.Collections;
using System.Globalization;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class CncJsonController : MonoBehaviour
{
    public string url = "https://cnc.kovalev.team/get/3";
    public int channel = 1;
    public float interval = 0.5f;

    public Transform nodeX;
    public Transform nodeY;
    public Transform nodeZ;

    private Vector3 startX;
    private Vector3 startY;
    private Vector3 startZ;

    private IEnumerator Start()
    {
        if (nodeX == null || nodeY == null || nodeZ == null)
        {
            Debug.LogError("Назначьте три узла станка в Inspector.");
            yield break;
        }

        startX = nodeX.localPosition;
        startY = nodeY.localPosition;
        startZ = nodeZ.localPosition;

        while (true)
        {
            using (UnityWebRequest request = UnityWebRequest.Get(url))
            {
                request.timeout = 10;
                yield return request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.Success)
                {
                    try
                    {
                        MoveNodes(request.downloadHandler.text);
                    }
                    catch (Exception error)
                    {
                        Debug.LogWarning("Ошибка чтения JSON: " + error.Message);
                    }
                }
                else
                {
                    Debug.LogWarning($"Ошибка запроса: {request.responseCode} {request.error}");
                }
            }

            yield return new WaitForSeconds(interval);
        }
    }

    private void MoveNodes(string json)
    {
        JObject data = JObject.Parse(json);

        float x = GetCoordinate(data, "X") * 0.001f;
        float y = GetCoordinate(data, "Y") * 0.001f;
        float z = GetCoordinate(data, "Z") * 0.001f;

        nodeX.localPosition = startX + Vector3.right * x;
        nodeY.localPosition = startY + Vector3.forward * y;
        nodeZ.localPosition = startZ + Vector3.up * z;
    }

    private float GetCoordinate(JObject data, string axis)
    {
        JToken sections = data["data"];
        if (sections == null)
            throw new FormatException("В JSON нет раздела data");

        string channelName = "Оси Канала " + channel;
        string axisName = "Перемещение по оси " + axis;

        foreach (JToken section in sections)
        {
            if (section == null || section.Type != JTokenType.Array)
                continue;

            if (!string.Equals((string)section[0], channelName, StringComparison.OrdinalIgnoreCase))
                continue;

            foreach (JToken row in section[1])
            {
                if (row == null || row.Type != JTokenType.Array)
                    continue;

                if (!string.Equals((string)row[0], axisName, StringComparison.OrdinalIgnoreCase))
                    continue;

                string text = ((string)row[1]).Replace("мм", "").Trim();
                text = text.Replace(',', '.');

                float value = float.Parse(text, CultureInfo.InvariantCulture);

                if (float.IsNaN(value) || float.IsInfinity(value))
                    throw new FormatException("Некорректная координата " + axis);

                return value;
            }
        }

        throw new FormatException("Не найдена координата " + axis);
    }
}