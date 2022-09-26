using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBolitaWithForce : MonoBehaviour
{
    private MyVector position;
    [SerializeField] private MyVector velocity;
    [SerializeField] private MyVector acceleration;
    [SerializeField] private float mass = 1f;
    private MyVector weight;

    [Header("Forces")]
    [SerializeField] private MyVector wind;
    [SerializeField] private MyVector gravity;


    [Header("World")]
    [SerializeField] Camera cam;
    [Range(0f, 1f)] [SerializeField] private float dampingFactor = 0.9f;
    [Range(0f, 1f)] [SerializeField] private float frictionCoefficient = 0.9f;

    void Start()
    {
        position = new MyVector(transform.position.x, transform.position.y);
    }
    private void FixedUpdate()
    {
    //  Reset acceleration
        acceleration = new MyVector(0, 0);

        // Weight
        MyVector weight = gravity * mass;
        ApplyForce(weight);

        // Friction
        float N = -mass * gravity.y;
        MyVector friction = -frictionCoefficient * N * velocity.normalized;
        ApplyForce(friction);
        friction.Draw(position, Color.blue);

        //Wind
        //ApplyForce(wind);

        //Integrate acceleration and velocity
        Move();
    }
    private void Update()
    {
        position.Draw(Color.blue);
        velocity.Draw(position, Color.red);
        //acceleration.Draw(position, Color.green);
    }
    public void Move()
    {
        velocity = velocity + acceleration * Time.fixedDeltaTime;
        position = position + velocity * Time.fixedDeltaTime;
        

        if (position.x > cam.orthographicSize)
        {
            velocity.x *= -1;
            velocity *= dampingFactor;
            position.x = cam.orthographicSize;
        }

        else if (position.x < -cam.orthographicSize)
        {
            velocity.x *= -1;
            velocity *= dampingFactor;
            position.x = -cam.orthographicSize;
        }

        if (position.y > cam.orthographicSize)
        {
            velocity.y *= -1;
            velocity *= dampingFactor;
            position.y = cam.orthographicSize;
        }

        else if (position.y < -cam.orthographicSize)
        {
            velocity.y *= -1;
            velocity *= dampingFactor;
            position.y = -cam.orthographicSize;
        }

        transform.position = new Vector3(position.x, position.y);
    }

    private void ApplyForce(MyVector force)
    {
        acceleration = force / mass;
    }
}

//clase 13 min 12