using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using Random = System.Random;

public class MyBolitaFallingIntoGargantua : MonoBehaviour
{
    private MyVector position;
    [SerializeField] private MyVector acceleration;
    [SerializeField] private MyVector velocity;

    [Header("World")]
    [SerializeField] Camera camera;
    [SerializeField] Transform gargantua;



    void Start()
    {
        position = new MyVector (transform.position.x, transform.position.y);
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        position.Draw(Color.blue);
        acceleration.Draw(position, Color.green);
        velocity.Draw(position, Color.red);

        //acceleration = /**/;
    }

    public void Move()
    {
        velocity = velocity + acceleration * Time.fixedDeltaTime;
        position = position + velocity * Time.fixedDeltaTime;

        transform.position = new Vector3(position.x, position.y);

    }


    //clase 10
}
