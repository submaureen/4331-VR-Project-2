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

    private bool doneIntro = false;
    // Start is called before the first frame update
    void Start()
    {
        ButtonUpdatePage.selectRecipe += PlayAudio;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAudio()
    {
        intro.enabled = true;
        StartCoroutine(WaitForIntro());
    }

    public IEnumerator WaitForIntro()
    {
        yield return new WaitForSeconds(10f);

        Debug.Log("song is finished!");

        intro.enabled = false;
        loop.enabled = true;
        yield break;
    }
}
