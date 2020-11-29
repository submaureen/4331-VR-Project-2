using Assets.ObjectTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCollisions : MonoBehaviour
{
    public static bool stepClear;
    public static Step step;
    public static bool wrongIngredient;
    public static GameObject currentInteraction;
    private float stepPourTime = -1;

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
        if (collider.gameObject.tag == "ingredient" && UtensilCollision.prepDone)
        {
            if (step.ingredient == collider.gameObject.name)
            {
                step.quantity--;
                if (step.quantity == 0)
                {
                    stepClear = true;
                }
                Debug.Log(step.ingredient + " is in!");
            } else
            {
                currentInteraction = collider.gameObject;
                Debug.Log(currentInteraction);
                wrongIngredient = true;
            }
        } else if (collider.gameObject.tag == "particle" && !stepClear)
        {
            print("enter: " + collider);

            // Set pour time to quantity only the inital pour
            if (stepPourTime == -1)
            {
                stepPourTime = step.quantity;
            }
        }

       
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "particle" && !stepClear)
        {
            if (stepPourTime > 0)
            {
                // print("stay: " + other);
                stepPourTime = stepPourTime - Time.deltaTime;
                Debug.Log(stepPourTime);
                Debug.Log(Time.deltaTime);
            }
            else {
                step.quantity = 0;
                stepClear = true;
            }
        }
        //time += Time.deltaTime;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "particle")
        {
            print("exit:" + other);
        }
        //Debug.Log($" poured for {time * 100}");
    }
}
