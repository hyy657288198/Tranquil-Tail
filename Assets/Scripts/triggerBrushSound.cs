using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerBrushSound : MonoBehaviour
{
    [SerializeField] private Transform waypoint1;
    [SerializeField] private Transform waypoint2;
    [SerializeField] private AudioSource brushSound;
    [SerializeField] private Transform player;

    private void Start()
    {
        if (brushSound != null)
        {
            brushSound.loop = true;
            brushSound.volume = 2f;
        }
        else
        {
            Debug.LogError("AudioSource not assigned to brushSound!");
        }
    }

    private void Update()
    {   
        if (IsPlayerWithinRange())
        {
            if (!brushSound.isPlaying)
            {   
                //reset the volume
                brushSound.volume = 2f;
                brushSound.Play();
            }
        }
        else
        {
            if (brushSound.isPlaying)
            {
                // Fade out the sound
                while (brushSound.volume > 0)
                {
                    brushSound.volume -= 0.1f;
                }

                brushSound.Stop();
            }
        }
    }

    private bool IsPlayerWithinRange()
    {
        return player.position.x > waypoint2.position.x && player.position.x < waypoint1.position.x;
    }
}
