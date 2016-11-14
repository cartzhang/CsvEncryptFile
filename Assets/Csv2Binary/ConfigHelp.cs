using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class ConfigHelp {
    public static void LoadCsv<T>(string path, Dictionary<int,T> dict) where T :class,new()
    {
        FileStream fs = new FileStream(Application.streamingAssetsPath + "/Config/"+path+".slqj", FileMode.Open, FileAccess.Read, FileShare.None);
        BinaryReader br = new BinaryReader(fs);
        try
        {
            string str = Base64.DecodeBase64(br.ReadString());
            string[] datas = str.Split('#');
            int len = datas.Length;
            for (int i = 0; i < len; i++)
            {
                if (datas[i] == "")
                {
                    continue;
                }
                string[] data = datas[i].Split(',');
                T t = new T();
                (t as BaseCsvData).SetData(data);
                dict.Add((t as BaseCsvData).Id,t);
            }
        }
        catch (EndOfStreamException e)
        {
            Debug.Log(e.Message);
        }
        br.Close();
        fs.Close();
    }
}

public class BaseCsvController
{
    public static BaseCsvController Instance { get { return SingletonProvider<BaseCsvController>.Instance; } }
    protected string path = "";

    public virtual void Load()
    {

    }
}

public class TestController : BaseCsvController
{
    public static TestController Instance { get { return SingletonProvider<TestController>.Instance; } }
    private Dictionary<int, TestData> dict = new Dictionary<int, TestData>();

    public override void Load()
    {
        ConfigHelp.LoadCsv<TestData>(CsvPathName.TEST_DATA, dict);
    }

}

public class BaseCsvData
{
    public int Id;
    public virtual void SetData(string[] datas)
    {
        int.TryParse(datas[0],out Id);
    }
}

public class TestData:BaseCsvData
{
    public string Name;

    public override void SetData(string[] datas)
    {
        base.SetData(datas);
        Name = datas[1];
    }
}

public class CsvPathName
{
    public static string TEST_DATA = "aaa";
    public static string ANIMAL_DATA = "animal";
    public static string GLOBAL_DATA = "global";
    public static string GUN_DATA = "gun";
}
    
 
