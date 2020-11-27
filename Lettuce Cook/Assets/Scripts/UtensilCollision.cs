using System.Collections;
using Assets.ObjectTypes;
using System.Collections.Generic;
using UnityEngine;

public class UtensilCollision : MonoBehaviour
{

    public static bool stepClear;
    public static PrepStep step;
    public static bool wrongIngredient;
    public static GameObject currentInteraction;
    public static bool prepDone = false;
    // Start is called before the first frame update
    void Start()
    {
        PrepManagement.finishPrep += Foo;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Foo()
    {
        // test to make sure that events are being fired properly
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "ingredient" && !prepDone)
        {
            if (step.ingredient == collider.gameObject.name)
            {
                // delete original gameobject
                // spawn in the same number of chopped prefabs as step.quantity
                currentInteraction = collider.gameObject;
                stepClear = true;
                Debug.Log($"cut into {step.quantity} pieces");
                Debug.Log("did a chopu");
            }
        }
    }
}
