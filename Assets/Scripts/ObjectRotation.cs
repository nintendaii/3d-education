using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotation : MonoBehaviour
{
    public float[] vector = new float[3];
    void Update()
    {
        gameObject.transform.Rotate(vector[0],vector[1],vector[2]);
    }
}
