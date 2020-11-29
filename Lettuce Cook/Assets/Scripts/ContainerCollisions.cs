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

    [SerializeField]
    public AudioSource[] sfx;

    // Start is called before the first frame update
    void Start()
    {
        PrepManagement.finishPrep += BeginFryNoise;
        RecipeManagement.burnedIngredient += PlayBurnNoise;
        RecipeManagement.finishGame += StopAudio;
        Debug.Log("hayo");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BeginFryNoise()
    {
        sfx[0].Play();
    }

    public void PlayBurnNoise()
    {
        sfx[3].Play();
    }

    public void StopAudio()
    {
        foreach (AudioSource source in sfx)
        {
            source.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "ingredient" && UtensilCollision.prepDone)
        {
            if (step.ingredient == collider.gameObject.name)
            {
                sfx[1].Play();
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
                sfx[2].loop = true;
                sfx[2].Play();
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
                sfx[2].loop = false;
                sfx[2].Stop();
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
            sfx[2].loop = false;
            print("exit:" + other);
        }
        //Debug.Log($" poured for {time * 100}");
    }
}
