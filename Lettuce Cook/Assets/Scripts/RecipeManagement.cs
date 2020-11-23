using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeManagement : MonoBehaviour
{

    float currCountdownValue;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartCountdown());

        Debug.Log("all done");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator StartCountdown(float countdownValue = 10)
    {
        currCountdownValue = countdownValue;
        while (currCountdownValue > 0)
        {
            Debug.Log("Countdown: " + currCountdownValue);
            yield return new WaitForSeconds(1.0f);
            currCountdownValue--;
        }
        Debug.Log("Countdown: " + currCountdownValue);
        DoLast();
    }

    public void DoLast()
    {
        Debug.Log("test");
        //yield return null;
    }
}
