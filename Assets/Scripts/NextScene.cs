using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform waypoint;
    [SerializeField] private string sceneName;

    [SerializeField] private Animator transition;

    private void Update()
    {
        if (Vector3.Distance(player.position, waypoint.position) < 1f)
        {
        StartCoroutine(LoadSceneCoroutine(sceneName));
        }
    }

    private IEnumerator LoadSceneCoroutine(string sceneName)
    {
            transition.SetTrigger("Start");
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene(sceneName);
    }
    
}
