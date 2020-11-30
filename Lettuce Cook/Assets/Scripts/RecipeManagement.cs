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

    [SerializeField]
    AudioClip[] scoreClips;

    [SerializeField]
    AudioSource sfx;

    float currCountdownValue;
    int stepCounter = 0;
    Recipe currentRecipe;
    int maxHealth = 0;

    private bool hasStarted = false;

    public delegate void ClickAction();
    public static event ClickAction burnedIngredient;

    public static event ClickAction finishGame;
    // bool currentStepStatus = false;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(instruction.text);
        // OPTIONAL TODO: populate recipes from json and remove SerializeField attribute

        // TODO: Get selected recipe from menu - currently assuming first recipe


        PrepManagement.finishPrep += StartRecipe;

        // StartCoroutine(StartCountdown());

        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartRecipe()
    {


        if (!hasStarted)
        {
            Debug.Log("aefie?");
            int page = ((ButtonUpdatePage.recipePage / 2) - 1);
            currentRecipe = recipes[page];

            // Get max health for score calculation
            foreach (Step step in currentRecipe.steps)
            {
                maxHealth += step.health;
            }
            UtensilCollision.prepDone = true;
            hasStarted = true;
            StartCoroutine(StartCountdown());
        }
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


                burnedIngredient();
                Step previous = currentRecipe.steps[stepCounter - 1];
                previous.health = Math.Max(0, previous.health - previous.penaltyAmount);

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
            burnedIngredient();
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
                sfx.Play();
                // Continue down recipe
                Debug.Log("Going to next step");
                StartCoroutine(StartCountdown());
            }
            else
            {
                // Done cooking
                subtext.text = "";
                DoLast();
            }
        }
    }

    public void DoLast()
    {
        finishGame();
        instruction.text = "Finished! Good job! Your score is:";

        sfx.clip = scoreClips[1];
        sfx.loop = true;
        sfx.Play();
        

        StartCoroutine(ChottoMatte());


    }

    public IEnumerator ChottoMatte()
    {
        yield return new WaitForSeconds(2.5f);

        int health = 0;
        // Calculate score
        foreach (Step step in currentRecipe.steps)
        {
            health += step.health;
        }

        float score = ((float)health / maxHealth) * 100f;

        switch (score)
        {
            case (100.0f):
                sfx.Stop();
                sfx.clip = scoreClips[4];
                sfx.loop = false;
                sfx.Play();
                break;
            case var expression when (score > 70.0f && score < 99.99f):
                sfx.Stop();
                sfx.clip = scoreClips[3];
                sfx.loop = false;
                sfx.Play();
                break;
            case var expression when (score < 70.0f):
                sfx.Stop();
                sfx.clip = scoreClips[2];
                sfx.loop = false;
                sfx.Play();
                break;
        }

        subtext.text = "Score: " + score.ToString("0.00");

        Instantiate(currentRecipe.finalFood);
    }
}
