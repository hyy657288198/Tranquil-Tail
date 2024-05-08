using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class textReplacer_village : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Transform traveler;
    [SerializeField] private Transform merchant;

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
        if (Vector2.Distance(player.position, traveler.position) < msgRange)
        {
            if (!isSoundPlaying)
            {
                soundEffect.Play();
                isSoundPlaying = true;
            }
            text.text = "Oh! It's you! Village seems not harmony right now. See you around!";
        }
        else if (Vector2.Distance(player.position, oldMan.position) < msgRange)
        {
            if (!isSoundPlaying)
            {
                soundEffect.Play();
                isSoundPlaying = true;
            }
            text.text = "My wife, died few years ago. I miss her so much...";
        }
        else if (Vector2.Distance(player.position, merchant.position) < msgRange)
        {
            if (!isSoundPlaying)
            {
                soundEffect.Play();
                isSoundPlaying = true;
            }
            text.text = "there's nothing to purchase right now. Come back later!";
        }
        else {
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
