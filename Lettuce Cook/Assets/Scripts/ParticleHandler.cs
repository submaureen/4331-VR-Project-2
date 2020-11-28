using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHandler : MonoBehaviour
{

    float time;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    time = 0;
    //    print("enter" + other);
    //}

    //private void OnTriggerStay(Collider other)
    //{
    //    //time += Time.deltaTime;
    //    print("a");
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    print(other);
    //    Debug.Log($" poured for {time * 100}");
    //}

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collide");
    }
}
