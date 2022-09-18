using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class MyBolita : MonoBehaviour
{
    private MyVector position;
    //private MyVector displacement;
    [SerializeField] private MyVector acceleration;
    [SerializeField] private MyVector velocity;
    [Range(0f, 1f)][SerializeField] private float dampingFactor = 0.9f;

    [Header("World")]
    [SerializeField] Camera camera;


    private int currentAccelerationCounter = 0;
    private readonly MyVector[] directions = new MyVector[4]
    {
        new MyVector (x:0, y:-9.8f),
        new MyVector (x:9.8f, y:0f),
        new MyVector (x:0, y:9.8f),
        new MyVector (x:-9.8f, y:0f)
    };
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Change the acceleration
            acceleration = directions[(++currentAccelerationCounter) % directions.Length];
            velocity *= 0;
        }

    }

    public void Move()
    {
        velocity = velocity + acceleration * Time.fixedDeltaTime;
        position = position + velocity * Time.fixedDeltaTime;

        if (position.x > camera.orthographicSize)
        {
            velocity.x *= -1;
            position.x = -camera.orthographicSize;
            velocity *= dampingFactor; //damping factor

        }
        else if (position.x < -camera.orthographicSize)
        {
            velocity.x *= -1;
            position.x = -camera.orthographicSize;
            velocity *= dampingFactor; //damping factor
            
        }

        if (position.y > camera.orthographicSize)
        {
            velocity.y *= -1;
            position.y = -camera.orthographicSize;
            velocity *= dampingFactor; //damping factor
        }
        else if (position.y < -camera.orthographicSize)
        {
            velocity.y *= -1;
            position.y = -camera.orthographicSize;
            velocity *= dampingFactor; //damping factor
        }

            



        CheckBounds(ref position.x, ref velocity.x, camera.orthographicSize);
        CheckBounds(ref position.y, ref velocity.y, camera.orthographicSize);
        transform.position = new Vector3(position.x, position.y);


    
    }


    private void CheckBounds(ref float x, ref float displacementX, float halfWidth)
    {
        if (Mathf.Abs(x) > halfWidth)
        {
            displacementX = displacementX * -1;
            x = Mathf.Sign(x) * camera.orthographicSize;
        }

    }
  
}
