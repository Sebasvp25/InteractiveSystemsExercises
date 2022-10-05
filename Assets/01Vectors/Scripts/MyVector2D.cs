using System;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

[Serializable]
struct MyVector
{
    public float x;
    public float y;
    public float magnitude => Mathf.Sqrt(x * x + y * y);

    public MyVector normalized
    {
        get
        {
            if (magnitude <= 0.0001f)
            {
                return new MyVector(0, 0);

            }

            return new MyVector(x / magnitude, y / magnitude);
        }
    }
                
    public MyVector(float x, float y)
    {
        this.x = x;
        this.y = y;
    }

    public void Normalize()
    {
        float tolerance = 0.0001f;
        if (magnitude <= tolerance)
        {
            x = 0; y = 0;
            return;
        }
        x /= magnitude; y /= magnitude;
    }
    public void Draw(Color color)
    {
        Vector3 start = Vector3.zero;
        Vector3 end = new Vector3(x, y);
        Debug.DrawLine(start, end, color);
    }

    public void Draw(MyVector newOrigin, Color color)
    {
        Vector3 start = new Vector3(newOrigin.x, newOrigin.y);
        Vector3 end = new Vector3(x:newOrigin.x + x, y:newOrigin.y + y);
        Debug.DrawLine(start, end , color);
    }

    public static MyVector operator +(MyVector a, MyVector b)
    {
        return new MyVector(x: a.x + b.x, y:a.y + b.y);
    }

    public static MyVector operator -(MyVector a, MyVector b)
    {
        return new MyVector(x: a.x - b.x, y: a.y - b.y);
    }

    public static MyVector operator *(MyVector a, float b)
    {
        return new MyVector(x: a.x * b, y: a.y * b);
    }

    public static MyVector operator /(MyVector a ,float b)
    {
        return new MyVector(x: a.x / b, y: a.y / b);
    }

    public static MyVector operator *( float b,MyVector a)
    {
        return new MyVector(x: a.x * b, y: a.y * b);
    }
}