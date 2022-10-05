using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBolitaWithForce : MonoBehaviour
{

    public enum BolitaRunMode
    {
        Friction,
        FluidFriction,
        Gravtiy,
    }

    public float Mass => mass;
    private MyVector position;
    [SerializeField] private BolitaRunMode runMode;
    [SerializeField] private MyVector velocity;
    [SerializeField] private MyVector acceleration;
    [SerializeField] private float mass = 1f;

    [Header("Forces")]
    [SerializeField] private MyVector wind;
    [SerializeField] private MyVector gravity;
    [SerializeField] private Color windColor;


    [Header("Other settings")]
    [SerializeField] Camera cameraRef;
    [SerializeField] private MyBolitaWithForce otherBolita;
    [Range(0f, 1f)] [SerializeField] private float dampingFactor = 0.9f;
    [Range(0f, 1f)] [SerializeField] private float frictionCoefficient = 0.9f;
    
    void Start()
    {
        //transform.forward;
        //transform.up;
        //transform.right;
        position = new MyVector(transform.position.x, transform.position.y);
    }
    private void FixedUpdate()
    {
    //  Reset acceleration
        acceleration *= 0f;

        if (runMode != BolitaRunMode.Gravtiy)
        {
            
            MyVector weight = gravity * mass;
            ApplyForce(weight);
            weight.Draw(position, Color.red);
        }

        if (runMode == BolitaRunMode.FluidFriction)
        {
            ApplyFluidFriction();
           
        }
        else if (runMode == BolitaRunMode.Friction)
        {
            ApplyFriction();
        }
        else if (runMode == BolitaRunMode.Gravtiy)
        {
            //weight
            MyVector r = otherBolita.position - position;
            float rSquare = r.magnitude * r.magnitude;
            MyVector gravityAttraction = (mass * otherBolita.mass / rSquare) * r.normalized;
            ApplyForce(gravityAttraction);
        }



        //Wind
        //ApplyForce(wind);

        //Integrate acceleration and velocity
        Move();
    }
    private void Update()
    {
        position.Draw(Color.blue);
        velocity.Draw(position, Color.red);
        
    }
    public void Move()
    {
        velocity = velocity + acceleration * Time.fixedDeltaTime;
        position = position + velocity * Time.fixedDeltaTime;

        if (runMode != BolitaRunMode.Gravtiy)
        {
            CheckWorldBoxBounds();
        }

        acceleration *= 0;
        transform.position = new Vector3(position.x, position.y);
    }

    private void ApplyForce(MyVector force)
    {
        acceleration += force / mass;
    }

    private void ApplyFriction()
    {
        // Friction
        float N = 1; //-mass * gravity.y;
        MyVector friction = -frictionCoefficient * N * velocity.normalized;
        ApplyForce(friction);
        friction.Draw(position, Color.blue);
    }

    private void ApplyFluidFriction()
    {
        if (transform.localPosition.y <= 0)
        {
            float frontalArea = transform.localScale.x;
            float rho = 1;
            float fluidDragCoefficient = 1;
            float velocityMagnitude = velocity.magnitude;
            float scalarPart = -0.5f * rho * velocityMagnitude * velocityMagnitude * frontalArea * fluidDragCoefficient;
            MyVector friction = scalarPart * velocity.normalized;
            ApplyForce(friction);
        }
    }
    private void CheckWorldBoxBounds()
    {
        //horizontal
        if (position.x > cameraRef.orthographicSize)
        {
            velocity.x *= -1;
            position.x = cameraRef.orthographicSize;
            velocity *= dampingFactor;
        }

        else if (position.x < -cameraRef.orthographicSize)
        {
            velocity.x *= -1;
            position.x = -cameraRef.orthographicSize;
            velocity *= dampingFactor;
        }

        //Vertical
        
        if (position.y > cameraRef.orthographicSize)
        {
            velocity.y *= -1;
            position.y = cameraRef.orthographicSize;
            velocity *= dampingFactor;
        }

        else if (position.y < -cameraRef.orthographicSize)
        {
            velocity.y *= -1;
            position.y = -cameraRef.orthographicSize;
            velocity *= dampingFactor;
        }
    }

}

//clase 13 min 