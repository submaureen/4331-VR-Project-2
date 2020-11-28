using System.Collections;
using System.Collections.Generic;
using Assets.ObjectTypes;
using UnityEngine;

public class PourDetector : MonoBehaviour
{
    public float pourThreshold;
    public Transform origin;
    public GameObject streamPrefab;

    private bool isPouring;
    private StreamPour currentStream;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool pourCheck = CalcPourAngle() > pourThreshold;

        // Debug.Log(Vector3.Dot(transform.up, Vector3.down));


        if (isPouring != pourCheck) {
            isPouring = pourCheck;

            if (isPouring)
            {
                StartPour();
            }
            else
            {
                EndPour();
            }
        }
        
    }

    private void StartPour()
    {
        print("Start");
        currentStream = CreateStream();
        currentStream.Begin();

    }

    private void EndPour()
    {
        currentStream.End();
        currentStream = null;
    }

    private float CalcPourAngle()
    {
        return Vector3.Dot(transform.up, Vector3.down);
    }

    private StreamPour CreateStream()
    {
        GameObject streamObject = Instantiate(streamPrefab, origin.position, Quaternion.identity, transform);
        return streamObject.GetComponent<StreamPour>();
    }
}
