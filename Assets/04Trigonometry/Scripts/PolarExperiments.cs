using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PolarExperiments : MonoBehaviour
{
    [SerializeField] float radius;
    [SerializeField] float angleDeg;

    [Header("Speed and acceleration")]
    [SerializeField] float angularSpeed;
    [SerializeField] float angularAccel;
    [SerializeField] float radialSpeed;
    [SerializeField] float radialAccel;

    [Header("World")]
    [SerializeField] Transform bolita;
    private void Start()
    {
        Assert.IsNotNull(bolita, "Bolita reference is required");
    }

    private void Update()
    {
        //incrementa el angulo y el radio
        angleDeg += angularSpeed * Time.deltaTime;
        radius += radialSpeed * Time.deltaTime;

        // Actualiza la posicion de la bolita
        bolita.position = PolarToCartesian(radius, angleDeg);
        Debug.DrawLine(Vector3.zero, PolarToCartesian(radius, angleDeg));
    }

    private Vector3 PolarToCartesian(float radius, float angle)
    {
        float x = radius * Mathf.Cos(angle * Mathf.Deg2Rad);
        float y = radius * Mathf.Sin(angle * Mathf.Deg2Rad);
        return new Vector3(x, y, 0);
    }
    //clase 16 min 11:25
}
