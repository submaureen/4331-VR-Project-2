using System.Collections;
using Assets.ObjectTypes;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrepManagement : MonoBehaviour
{

    [SerializeField]
    Preparation[] prep;

    [SerializeField]
    Text instructions;

    Preparation currentPrep;
    int stepCounter = 0;

    public delegate void ClickAction();
    public static event ClickAction finishPrep;



    // Start is called before the first frame update
    void Start()
    {
        currentPrep = prep[0];

        Debug.Log(currentPrep.name);

        StartCoroutine(StartPrep());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public IEnumerator StartPrep()
    {
        PrepStep currentStep = currentPrep.steps[stepCounter];
        UtensilCollision.step = currentStep;
        instructions.text = currentStep.instructions;

        while (!UtensilCollision.stepClear)
        {
            yield return null;
        }

        IngredientInfo info = UtensilCollision.currentInteraction.GetComponent<IngredientInfo>();

        UtensilCollision.stepClear = false;
        stepCounter++;

        Debug.Log(UtensilCollision.self);




        if (info.choppedIngredient != null && !(UtensilCollision.self.tag == "mix") && currentStep.stepType == "chop")
        {
            info.choppedIngredient.transform.position = UtensilCollision.currentInteraction.transform.position;
            Destroy(UtensilCollision.currentInteraction);
            for (int i = 0; i < currentStep.quantity; i++)
            {
                Instantiate(info.choppedIngredient);
            }
        }


        if (stepCounter < currentPrep.steps.Length)
        {
            // Continue down recipe
            Debug.Log("Going to next step");
            StartCoroutine(StartPrep());
        }
        else
        {
            // Done cooking
            UtensilCollision.prepDone = true;
            Debug.Log("All ready to cook.");
            instructions.text = "ready to cook";

            finishPrep();
        }



    }
}
