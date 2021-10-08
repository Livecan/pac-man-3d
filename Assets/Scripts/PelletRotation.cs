using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelletRotation : MonoBehaviour
{
    private float rotationRange = 180f;
    private Vector3 rotationVector;

    // Start is called before the first frame update
    void Start()
    {
        rotationVector = new Vector3(Random.Range(-rotationRange, rotationRange), Random.Range(-rotationRange, rotationRange), Random.Range(-rotationRange, rotationRange));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.Rotate(rotationVector * Time.fixedDeltaTime);
    }
}
