using UnityEngine;
using System;
using System.Collections;
using System.IO;

public class GameUtil : MonoBehaviour
{
    public void UnZipData(string zipName, string zipPath, Action OnFinished)
    {
        StartCoroutine(UnZip(zipName, zipPath, OnFinished));
    }

    IEnumerator UnZip(string zipName, string zipPath, Action OnFinished)
    {
        ZipUtil.Unzip(zipName, zipPath);
        yield return null;
        OnFinished();
    }

    public string ReadJsonFile(string path)
    {
        string jsonReturnString = "";

        StreamReader reader = new StreamReader(path);
        jsonReturnString = reader.ReadToEnd();
        reader.Close();

        return jsonReturnString;
    }
}