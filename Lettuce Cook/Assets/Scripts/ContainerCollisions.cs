using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCollisions : MonoBehaviour
{
    public static bool stepClear;
    public static string stepIngredient;
    public static bool wrongIngredient;
    public static GameObject currentInteraction;

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
            if (stepIngredient == collider.gameObject.name)
            {
                stepClear = true;
            } else
            {
                currentInteraction = collider.gameObject;
                Debug.Log(currentInteraction);
                wrongIngredient = true;
            }
        }

       
    }
}
