using Assets.ObjectTypes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeManagement : MonoBehaviour
{
    [SerializeField]
    Recipe[] recipes;

    [SerializeField]
    Text instruction;

    [SerializeField]
    Text timer;

    float currCountdownValue;
    int stepCounter = 0;
    Recipe currentRecipe;
    // bool currentStepStatus = false;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(instruction.text);
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

        instruction.text = currentStep.instructions;
        currCountdownValue = currentStep.cookTime;

        ContainerCollisions.step = currentStep;

        // cookTime with -1 will wait for step to complete
        while (currCountdownValue == -1 && !ContainerCollisions.stepClear)
        {
            timer.text = "Take your time :)";
            if (ContainerCollisions.wrongIngredient)
            {
                ContainerCollisions.wrongIngredient = false;
                IngredientInfo info = ContainerCollisions.currentInteraction.GetComponent<IngredientInfo>();
                ContainerCollisions.currentInteraction.transform.parent.transform.position = info.ogPosition;

                // inserted wrong ingredient from recipe
                // reset ingredient
                // decrease last steps score
                // display smoke and play sound
            }
            yield return null;
        }

        // Countdown until step timer complete
        while (currCountdownValue > 0)
        {
            timer.text = "Countdown: " + currCountdownValue;
            if (ContainerCollisions.stepClear)
            {
                // Finished putting in all of the ingredients
                break;
            }
            if (ContainerCollisions.wrongIngredient)
            {
                ContainerCollisions.wrongIngredient = false;
                IngredientInfo info = ContainerCollisions.currentInteraction.GetComponent<IngredientInfo>();
                ContainerCollisions.currentInteraction.transform.parent.transform.position = info.ogPosition;

                // inserted wrong ingredient from recipe
                // reset ingredient
                // decrease last steps score
                // display smoke and play sound
            }
            yield return new WaitForSeconds(1.0f);
            currCountdownValue--;
        }

        // Check if the ingredient was inserted
        if (!ContainerCollisions.stepClear) // not inserted
        {
            // TODO: Show some smoke and play sound
            // Burn the previous step
            Step previous = currentRecipe.steps[stepCounter - 1];
            previous.health = Math.Max(0, previous.health - previous.penaltyAmount);
            StartCoroutine(StartCountdown());
        } else // inserted
        {
            // Reset step checker and move to next step
            ContainerCollisions.stepClear = false;
            stepCounter++;

            if (stepCounter < currentRecipe.steps.Length)
            {
                // Continue down recipe
                Debug.Log("Going to next step");
                StartCoroutine(StartCountdown());
            }
            else
            {
                // Done cooking
                DoLast();
            }
        }
    }

    public void DoLast()
    {
        instruction.text = "Finished! Good job!";
    }
}
