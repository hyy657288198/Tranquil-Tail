using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class popupManager : MonoBehaviour
{
    public GameObject popupWindow;


    private void Start()
    {
        popupWindow.SetActive(false);
    }


    public void TogglePopup()
    {
        try
        {
            PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();
            if (playerMovement.played == true)
            {
                Time.timeScale = 1;
                SceneManager.UnloadSceneAsync("StartMenu");
            }
        }
        catch
        {

            bool isActive = popupWindow.activeSelf;
            popupWindow.SetActive(!isActive);

        }
    }
    public void Okay()
    {
        popupWindow.SetActive(false);
    }

}
