using Assets.ObjectTypes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManagement : MonoBehaviour
{

    [SerializeField]
    public AudioSource intro;

    public AudioSource loop;

    // keep a copy of the executing script
    private IEnumerator coroutine;

    // Start is called before the first frame update
    void Start()
    {
        ButtonUpdatePage.selectRecipe += PlayAudio;
        RadioControl.playAudio += PlayAudio;
        RadioControl.stopAudio += StopAudio;

        coroutine = WaitForIntro();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAudio()
    {
        coroutine = WaitForIntro();
        intro.enabled = true;
        StartCoroutine(coroutine);
    }

    public void StopAudio()
    {
        intro.enabled = false;
        loop.enabled = false;
        StopCoroutine(coroutine);
    }

    public IEnumerator WaitForIntro()
    {
        yield return new WaitForSeconds(10f);

        intro.enabled = false;
        loop.enabled = true;
        yield break;
    }
}
