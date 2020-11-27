using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientInfo : MonoBehaviour
{
    public Vector3 ogPosition;
    public GameObject choppedIngredient;
    // Start is called before the first frame update
    void Start()
    {
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
}
