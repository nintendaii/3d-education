using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotation : MonoBehaviour
{
    public float[] vector = new float[3];
    void Update()
    {
        gameObject.transform.Rotate(vector[0]=0.5f,vector[1]=0.5f,vector[2]=0.4f);
    }
}
