using Assets.ObjectTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeManagement : MonoBehaviour
{
    [SerializeField]
    Recipe[] recipes;

    float currCountdownValue;
    int stepCounter = 0;
    Recipe currentRecipe;
    // bool currentStepStatus = false;

    // Start is called before the first frame update
    void Start()
    {
        // OPTIONAL TODO: populate recipes from json and remove SerializeField attribute

        // TODO: Get selected recipe from menu - currently assuming first recipe
        currentRecipe = recipes[0];

        StartCoroutine(StartCountdown());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator StartCountdown()
    {
        Step currentStep = currentRecipe.steps[stepCounter];
        Debug.Log("Looking for: " + currentStep.ingredient);

        currCountdownValue = currentStep.cookTime;
        while (currCountdownValue > 0)
        {
            Debug.Log("Countdown: " + currCountdownValue);
            if (ContainerCollisions.stageClear)
            {
                Debug.Log(currentStep.ingredient + " is in!");
                break;
            }
            yield return new WaitForSeconds(1.0f);
            currCountdownValue--;
        }

        Debug.Log("Countdown: " + currCountdownValue);

        Debug.Log("We broke out!");
        ContainerCollisions.stageClear = false;
        stepCounter++;
        if (stepCounter < currentRecipe.steps.Length)
        {
            Debug.Log("Going to next step");
            StartCoroutine(StartCountdown());
        } else
        {
            Debug.Log("I think we're done homie");
        }

        DoLast();
    }

    public void DoLast()
    {
        Debug.Log("test");
        //yield return null;
    }
}
