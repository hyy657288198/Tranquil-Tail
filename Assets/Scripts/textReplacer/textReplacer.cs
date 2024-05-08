using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class textReplacer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Transform billBoard;
    [SerializeField] private Transform oldMan;
    [SerializeField] private Transform player;
    [SerializeField] private AudioSource soundEffect;
    private float msgRange = 1f;
    private bool isSoundPlaying = false;

    private void Start()
    {
        text.text = "";
        soundEffect.loop = false;
    }

    private void Update()
    {
        if (Vector3.Distance(player.position, billBoard.position) < msgRange)
        {
            if (!isSoundPlaying)
            {
                soundEffect.Play();
                isSoundPlaying = true;
            }
            text.text = "You may hope to find a way to support you to that hill";
        }
        else if (Vector3.Distance(player.position, oldMan.position) < msgRange)
        {
            if (!isSoundPlaying)
            {
                soundEffect.Play();
                isSoundPlaying = true;
            }
            text.text = "Young man, trust your intuition and you will find the way";
        }
        else
        {
            if (isSoundPlaying)
            {
                soundEffect.Stop();
                isSoundPlaying = false;
            }
            text.text = "";
        }
    }
}
