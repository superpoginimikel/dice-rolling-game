using UnityEngine;
using System;
using System.Collections;
using System.IO;

public static class GameUtil
{
    public static void UnZip(string zipName, string zipPath)
    {
        
        ZipUtil.Unzip(zipName, zipPath);
    }

    public static string GetJsonStringFromFile(string path)
    {
        string jsonReturnString = "";

        StreamReader reader = new StreamReader(path);
        jsonReturnString = reader.ReadToEnd();
        reader.Close();

        return jsonReturnString;
    }

    public static Sprite GetSpriteByPath(string path)
    {
        Texture2D texture = GetTexture2DByPath(path);

        if(texture == null)
        {
            Debug.LogError("texture cannot be found");
            return null;
        }

        Sprite sprite;
        sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        return sprite;
    }

    private static Texture2D GetTexture2DByPath(string path)
    {
        if (path == null || path == "")
        {
            return null;
        }
        try
        {
            if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
            {
                path = path.Replace("/", "\\");
            }

            byte[] bytes;
            bytes = File.ReadAllBytes(path);
            Texture2D texture = new Texture2D(2, 2, TextureFormat.RGB24, false);

            texture.filterMode = FilterMode.Bilinear;
            texture.LoadImage(bytes);

            return texture;
        }
        catch (Exception)
        {
            //CPLog.PrintCritical (ex.Message, CPLogEnum.LogCategories.GUI);
            return null;
        }
    }
}