using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class boardManager : MonoBehaviour
{
    public TextMeshProUGUI rankText;
    public TextMeshProUGUI clearedText;
    public TextMeshProUGUI timeText;

    [System.Serializable]
    public class Charts
    {
        public List<int> cleared = new List<int>();
        public List<int> not = new List<int>();
    }

    void Start()
    {
        LoadAndDisplayCharts();
    }

    void LoadAndDisplayCharts()
    {
        Charts charts = LoadCharts();
        
        List<int> displayData = new List<int>();
        
        for (int i = 0; i < charts.cleared.Count && i < 3; i++)
        {
            displayData.Add(charts.cleared[i]);
        }
        
        if (displayData.Count < 3)
        {
            for (int j = 0; j < charts.not.Count && displayData.Count < 3; j++)
            {
                displayData.Add(charts.not[j]);
            }
        }
        
        rankText.text = "";
        clearedText.text = "";
        timeText.text = ""; 

        for (int i = 0; i < displayData.Count; i++)
        {
            rankText.text += (i + 1).ToString() + "\n\n";
            timeText.text += displayData[i] + "s\n\n"; 

            if (i < charts.cleared.Count)
            {
                clearedText.text += "cleared\n\n";
            }
            else
            {
                clearedText.text += "not cleared\n\n";
            }
        }
    }

    Charts LoadCharts()
    {
        string path = Path.Combine(Application.persistentDataPath, "charts.json");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<Charts>(json);
        }
        return new Charts();
    }
}

