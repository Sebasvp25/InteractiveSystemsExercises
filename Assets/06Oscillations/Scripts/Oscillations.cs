using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillations : MonoBehaviour
{

    [SerializeField] private float amplitude = 1;
    [SerializeField] private float period = 1;
    Vector3 initialPosition;
    

    private void Start()
    {
        initialPosition = transform.position;
    }


    private void Update()
    {

        float x = Mathf.Sin(5f * Time.time) + Mathf.Cos(Time.time / 3f) + Mathf.Sin(Time.time/ 13f);
        transform.position = initialPosition + new Vector3(x, x, 0);
    }
}