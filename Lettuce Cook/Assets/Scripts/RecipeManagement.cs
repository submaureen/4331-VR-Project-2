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
    Text subtext;

    float currCountdownValue;
    int stepCounter = 0;
    Recipe currentRecipe;
    int maxHealth = 0;
    // bool currentStepStatus = false;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(instruction.text);
        // OPTIONAL TODO: populate recipes from json and remove SerializeField attribute

        // TODO: Get selected recipe from menu - currently assuming first recipe
        currentRecipe = recipes[0];

        // Get max health for score calculation
        foreach (Step step in currentRecipe.steps)
        {
            maxHealth += step.health;
        }

        PrepManagement.finishPrep += StartRecipe;

        // StartCoroutine(StartCountdown());

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartRecipe()
    {
        Debug.Log("aefie?");
        StartCoroutine(StartCountdown());
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
            subtext.text = "Take your time :)";
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
            subtext.text = "Countdown: " + currCountdownValue;
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

        int health = 0;
        // Calculate score
        foreach (Step step in currentRecipe.steps)
        {
            health += step.health;
        }

        float score = ((float)health / maxHealth) * 100f;

        subtext.text = "Score: " + score.ToString("0.00");

        Instantiate(currentRecipe.finalFood);
    }
}
