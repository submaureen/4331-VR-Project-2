using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioControl : MonoBehaviour
{

    [SerializeField]
    public AudioSource button_push;

    public delegate void ClickAction();
    public static event ClickAction playAudio;
    public static event ClickAction stopAudio;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StopAudio()
    {
        stopAudio();
        button_push.Play();
    }

    public void PlayAudio()
    {
        playAudio();
        button_push.Play();
    }
}
