using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{

    public float duration = 0.5f;
    public AnimationCurve curve;
    public bool start = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            start = false;
            StartCoroutine(Shaking());
        }
    }

    public IEnumerator Shaking()
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            elapsedTime+= Time.deltaTime;
            float str = curve.Evaluate(elapsedTime/duration);
            transform.position = startPosition + Random.insideUnitSphere * str;
            yield return null;
        }

        transform.position = startPosition;
    }
}
