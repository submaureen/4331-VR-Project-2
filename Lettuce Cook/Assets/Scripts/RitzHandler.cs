using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RitzHandler : MonoBehaviour
{
    [SerializeField]
    public GameObject chi;

    bool ritzOffCooldown = true;
    // Start is called before the first frame update
    void Start()
    {
    }

    public IEnumerator toggle()
    {
        yield return new WaitForSeconds(10f);
        ritzOffCooldown = true;
        Debug.Log("you can cut again.");
        yield break;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("touched " + other);
        if (other.gameObject.tag == "chop" && ritzOffCooldown)
        {
            Instantiate(chi);
            ritzOffCooldown = false;
            StartCoroutine(toggle());
        }
    }

}
