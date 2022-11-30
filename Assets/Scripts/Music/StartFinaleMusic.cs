using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartFinaleMusic : MonoBehaviour
{

    public AudioSource MainMusic;
    public GameObject Finale;

    void Start()
    {
        MainMusic = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MainMusic.mute = !MainMusic.mute;
        Finale.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        MainMusic.mute = !MainMusic.mute;
        Finale.SetActive(false);
    }

}
