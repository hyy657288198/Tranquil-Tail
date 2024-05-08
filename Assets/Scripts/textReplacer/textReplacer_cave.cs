using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class textReplacer_cave : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Transform traveler;
    [SerializeField] private Transform statue;
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
        if (Vector3.Distance(player.position, traveler.position) < msgRange)
        {
            if (!isSoundPlaying)
            {
                soundEffect.Play();
                isSoundPlaying = true;
            }
            text.text = "Hi! You saved me! Thank you! I will go back to the village now. I hope you find your way out of this cave!";
            //destroy the traveler
            Destroy(traveler.gameObject,6f);
            //set the text to empty after 6 seconds
            StartCoroutine(EmptyText());
        }
        else if (Vector3.Distance(player.position, statue.position) < msgRange)
        {
            if (!isSoundPlaying)
            {
                soundEffect.Play();
                isSoundPlaying = true;
            }
            text.text = "Head damage is critical for all lives...";
        }
        else if (Vector3.Distance(player.position, traveler.position) > msgRange)
        {
            if (isSoundPlaying)
            {
                soundEffect.Stop();
                isSoundPlaying = false;
            }
            text.text = "";
        }
    }

    IEnumerator EmptyText()
    {
        yield return new WaitForSeconds(6f);
        text.text = "";
    }
}
