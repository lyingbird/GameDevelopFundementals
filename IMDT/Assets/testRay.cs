using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testRay : MonoBehaviour
{
    Ray ray1;
    Vector3 Vup = new Vector3(0, 0, 1);
    Vector3 Vright = new Vector3(1, 0, 0);

    public float hitDistance;

    public Vector3 hitPosition;

    public Vector3 startPosition;

    public float unityHitDistance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ray1 = new Ray(transform.position + Vup / 2, Vup / 2);
        RaycastHit hit;
        if (Physics.Raycast(ray1, out hit, 1000f))
        {

            Vector3 distanceVector = (hit.point - (transform.position + Vup / 2));
            hitPosition = hit.point;
            hitDistance = distanceVector.magnitude;

            unityHitDistance = hit.distance;
            startPosition = transform.position + Vup / 2;

        }
    }
}
