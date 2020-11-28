using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreamPour : MonoBehaviour
{
    // Start is called before the first frame update

    private LineRenderer lineRenderer = null;

    private ParticleSystem splashParticle = null;

    private Coroutine pourRoutine = null;

    private Vector3 targetPos = Vector3.zero;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        splashParticle = GetComponentInChildren<ParticleSystem>();
        Debug.Log(splashParticle);
    }

    void Start()
    {
        MoveToPosition(0, transform.position);
        MoveToPosition(1, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Begin()
    {
        StartCoroutine(UpdateParticle());
        pourRoutine = StartCoroutine(BeginPour());
    }

    private IEnumerator BeginPour()
    {
        while (gameObject.activeSelf)
        {
            targetPos = FindEndpoint();
            MoveToPosition(0, transform.position);
            AnimToPosition(1, targetPos);
            yield return null;
        }

    }

    public void End()
    {
        StopCoroutine(pourRoutine);
        pourRoutine = StartCoroutine(EndPour());
    }

    private IEnumerator EndPour()
    {
        while (!HasReachedPosition(0, targetPos))
        {
            targetPos = FindEndpoint();
            AnimToPosition(0, targetPos);
            AnimToPosition(1, targetPos);
            yield return null;
        }

        Destroy(gameObject);
    }

    private Vector3 FindEndpoint()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down);

        Physics.Raycast(ray, out hit, 2.0f);

        Vector3 endPoint = hit.collider ? hit.point : ray.GetPoint(2.0f);


        return endPoint;
    }

    private void MoveToPosition(int index, Vector3 targetPos)
    {
        lineRenderer.SetPosition(index, targetPos);
    }

    private void AnimToPosition(int index, Vector3 targetPos)
    {
        Vector3 currentPoint = lineRenderer.GetPosition(index);
        Vector3 newPosition = Vector3.MoveTowards(currentPoint, targetPos, Time.deltaTime * 1.75f);
        lineRenderer.SetPosition(index, newPosition);
    }

    private bool HasReachedPosition(int index, Vector3 targetPos)
    {
        Vector3 currentPosition = lineRenderer.GetPosition(index);
        return currentPosition == targetPos;
    }

    private IEnumerator UpdateParticle()
    {
        while (gameObject.activeSelf)
        {
            splashParticle.gameObject.transform.position = targetPos;
            bool isHitting = HasReachedPosition(1, targetPos);
            splashParticle.gameObject.SetActive(isHitting);

            yield return null;
        }
    }
}
