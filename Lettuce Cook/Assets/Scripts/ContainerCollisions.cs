using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCollisions : MonoBehaviour
{
    public static bool stageClear;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("hayo");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "ingredient")
        {
            stageClear = true;
            Debug.Log(collider.gameObject.name);
        }

       
    }
}
