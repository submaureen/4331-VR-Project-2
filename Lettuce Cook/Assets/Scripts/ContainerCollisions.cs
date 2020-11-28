﻿using Assets.ObjectTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCollisions : MonoBehaviour
{
    public static bool stepClear;
    public static Step step;
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
        } else if (collider.gameObject.tag == "particle")
        {
            print("enter: " + collider);
        }

       
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "particle")
        {
            print("stay: " + other);
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
