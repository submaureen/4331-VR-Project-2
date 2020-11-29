using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientInfo : MonoBehaviour
{
    public Vector3 ogPosition;
    public GameObject choppedIngredient;
    public GameObject mixedIngredient = null;
    // Start is called before the first frame update
    void Start()
    {

        PrepManagement.finishPrep += ReplaceModels;
        ogPosition = gameObject.transform.position;
        if (choppedIngredient != null)
        {
            choppedIngredient.transform.position = ogPosition;
        }
        // Debug.Log($"i do be at { ogPosition }");
        ogPosition.y++;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReplaceModels()
    {
        if (mixedIngredient != null)
        {
            mixedIngredient.transform.position = gameObject.transform.position;
            Destroy(gameObject);
            Instantiate(mixedIngredient);
        }
    }
}
