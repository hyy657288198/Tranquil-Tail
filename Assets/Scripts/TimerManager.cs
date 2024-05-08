using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerManager : MonoBehaviour
{
    private bool isTiming;
    private float timer;

    [System.Serializable]
    public class Charts
    {
        public List<int> cleared = new List<int>();
        public List<int> not = new List<int>();
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Update()
    {
        if (isTiming)
        {
            timer += Time.deltaTime;
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Win" || scene.name == "Lose")
        {
            StopTimer();

            int finalTime = Mathf.FloorToInt(timer);
            Debug.Log("Done");
            Debug.Log(finalTime);
            UpdateCharts(scene.name, finalTime);
        }
        else if (scene.name == "Tutorial")
        {
            StartTimer();
        }
    }

    void StartTimer()
    {
        timer = 0.0f;
        isTiming = true;
    }

    void StopTimer()
    {
        isTiming = false;
    }

    void UpdateCharts(string sceneName, int finalTime)
    {
        Charts charts = LoadCharts();

        if (sceneName == "Win")
        {
            Debug.Log("win");
            charts.cleared.Add(finalTime);
            charts.cleared.Sort();
        }
        else if (sceneName == "Lose" && charts.cleared.Count < 3)
        {
            Debug.Log("lose");
            charts.not.Add(finalTime);
            charts.not.Sort((a,b) => b.CompareTo(a));
        }

        SaveCharts(charts);
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

    void SaveCharts(Charts charts)
    {
        string json = JsonUtility.ToJson(charts, true);
        string path = Path.Combine(Application.persistentDataPath, "charts.json");
        Debug.Log("path");
        Debug.Log(Application.persistentDataPath);
        File.WriteAllText(path, json);
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}



