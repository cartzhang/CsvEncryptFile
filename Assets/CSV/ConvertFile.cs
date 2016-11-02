using UnityEngine;
using System.Collections;
using System.Text;
using System;
using System.Collections.Generic;
using System.IO;

public class ConvertFile : MonoBehaviour
{
    [Header("勾选是加密，不勾则解密")]
    public bool ToBinary = false;
    private string csvPath;
    private string binaPath;
    private string binaEncryPath;
    private string binadeencrypath;

    // Use this for initialization
    void Start()
    {
        // 文件路径
        csvPath = ReadCSV.getPath() + "/Resources/example.csv";
        binaPath = csvPath.Substring(0, csvPath.Length - 4) + ".cat";
        binaEncryPath = csvPath.Substring(0, csvPath.Length - 4) + ".cate";
        binadeencrypath = csvPath.Substring(0, csvPath.Length - 4) + ".catde";

        Debug.Log(csvPath);
        if (ToBinary)
        {
            FileEncryptConvertHelper.EncryptFile(csvPath, binaEncryPath);
        }
        else
        {
            List<Dictionary<string, object>> content = new List<Dictionary<string, object>>();
            FileEncryptConvertHelper.DecryptFile(binaEncryPath, binadeencrypath);
            string tmpStr = ReadCSV.ReadContentToEnd(binadeencrypath);
            content =  ReadCSV.ReadContent(tmpStr);

            ShowData(content);
            Debug.Log(content.Count);
        }
    }

    void ShowData(List<Dictionary<string, object>> contentlist)
    {
        int len = contentlist.Count;
        foreach (var item1 in contentlist)
        {
            string lineContent = "";
            foreach (var item2 in item1)
            {
                lineContent += item2;
            }
            Debug.Log(lineContent);
        }
        
    }

}
