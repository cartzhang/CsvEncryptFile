using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

public class CsvEditor{
    [MenuItem("Assets/加载csv文件")]
    public static void CreateCsv()
    {
        string path = EditorUtility.OpenFilePanel("选择文件", Application.streamingAssetsPath+"/Csv", "csv");
        LoadCsv(path);
    }

    private static void LoadCsv(string path)
    {
        string result = "";
        FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None);
        StreamReader sr = new StreamReader(fs, Encoding.GetEncoding(936));
        string str = sr.ReadLine();
        while (str != null)
        {
            str = sr.ReadLine();
            if (str == null)
            {
                break;
            }
            result += str + "#";
        }
        sr.Close();
        fs.Close();
        string[] files = path.Split('/');
        string fileName = files[files.Length - 1];
        if (SaveBinaryFile(fileName.Split('.')[0], result))
        {
            Debug.Log("生成文件成功");
        }
    }

    private static bool SaveBinaryFile(string fileName,string content)
    {
        string path = Application.streamingAssetsPath + "/Config/" + fileName + ".slqj";
        FileStream fs = new FileStream(path, FileMode.Create);
        BinaryWriter bw = new BinaryWriter(fs);
        try
        {
            bw.Write(Base64.EncodeBase64(content));
        }
        catch (IOException e)
        {
            Debug.Log(e.Message);
        }
        bw.Close();
        fs.Close();
        return true;
    }  
}
