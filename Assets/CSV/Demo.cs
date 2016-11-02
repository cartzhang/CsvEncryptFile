using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Demo : MonoBehaviour {

    void Awake()
    {
        // 在Csv中，添加中文，读取不会报错，但是中文接卸不出来。暂时只有英文。
        List<Dictionary<string, object>> data = ReadCSV.Read("example");

        for (var i = 0; i < data.Count; i++)
        {
            print("name " + data[i]["name"] + " " +
                   "age " + data[i]["age"] + " " +
                   "speed " + data[i]["speed"] + " " +
                   "desc " + data[i]["description"] + " " +
                   "isDelicious " + data[i]["isDelicious"] + " "
                   //"breath" + data[i]["breath"]
                   );
        }

    }
}
