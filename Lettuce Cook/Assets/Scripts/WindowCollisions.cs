using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowCollisions : MonoBehaviour
{
    [SerializeField]
    public AudioSource sfx;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collider)
    {
        sfx.Play();
        print("hit window");
    }
}
