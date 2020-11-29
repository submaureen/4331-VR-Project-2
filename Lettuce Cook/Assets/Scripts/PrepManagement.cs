using System.Collections;
using Assets.ObjectTypes;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrepManagement : MonoBehaviour
{
    private ButtonUpdatePage updateCurrentPage;

    int recipePage;

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
        //grabs the current page from book script 
        // recipePage = GameObject.Find("Button").GetComponent<ButtonUpdatePage>().getCurrentPage();

        // currentPrep = prep[0];
        ButtonUpdatePage.selectRecipe += testFunc;
        // Debug.Log(currentPrep.name);

        // StartCoroutine(StartPrep());
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(recipePage);

    }

    public void testFunc()
    {
        Debug.Log("starting preparation stage");
        // currentPrep = prep[ButtonUpdatePage.recipePage/2];
        int page = ((ButtonUpdatePage.recipePage / 2) - 1);
        //Debug.Log((ButtonUpdatePage.recipePage / 2) - 1);
        if (page < 0)
        {
            instructions.text = ("Pick a valid recipe!");
            //Debug.Log(ButtonUpdatePage.recipePage);
        }
        else
        {
            currentPrep = prep[page];
            StartCoroutine(StartPrep());
            Debug.Log((ButtonUpdatePage.recipePage / 2) - 1);
            print(ButtonUpdatePage.recipePage);
            //StartCoroutine(StartPrep());
        }

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
