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
    public static GameObject self;
    public static int mixCheck = -1;
    private float stepPourTime = -1;

    [SerializeField]
    public AudioSource[] sfx;
    // Start is called before the first frame update
    void Start()
    {
        // self = gameObject;
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
                self = gameObject;


                if (self.tag == "chop" && self.tag == step.stepType)
                {
                    Debug.Log("we gonna chopu chopu");
                    currentInteraction = collider.gameObject;
                    sfx[0].Play();
                    stepClear = true;
                }
                if (self.tag == "mix" && self.tag == step.stepType)
                {
                    currentInteraction = collider.gameObject;
                    step.quantity--;
                    sfx[0].Play();
                    if (step.quantity == 0)
                    {
                        stepClear = true;
                    }
                }
            }
        }
        else if (collider.gameObject.tag == "particle" && !prepDone && (step.ingredient == collider.gameObject.name))
        {
            print("enter: " + collider);

            // Set pour time to quantity only the inital pour
            if (stepPourTime == -1)
            {
                sfx[1].Play();
                stepPourTime = step.quantity;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "particle" && !stepClear &&(step.ingredient == other.gameObject.name))
        {
            if (stepPourTime > 0)
            {
                // print("stay: " + other);
                stepPourTime = stepPourTime - Time.deltaTime;
                Debug.Log(stepPourTime);
                Debug.Log(Time.deltaTime);
            }
            else
            {
                step.quantity = 0;
                sfx[1].Stop();
                gameObject.transform.Find("bowl_liquid").gameObject.SetActive(true);
                stepClear = true;
            }
        }
        //time += Time.deltaTime;
    }
}
