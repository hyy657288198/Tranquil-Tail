using UnityEngine;

public class InitializationScript
{
#if UNITY_EDITOR
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void ClearPlayerPrefsOnStart()
    {
        Debug.Log("Development build: Clearing all PlayerPrefs data...");
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
#endif
}

