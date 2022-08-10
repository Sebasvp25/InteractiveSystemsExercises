using System;
using UnityEngine;

[Serializable]
struct MyVector
{
    public float x;
    public float y;

    public MyVector(float x, float y)
    {
        this.x = x;
        this.y = y;
    }

    public void Draw(Color color)
    {
        Debug.DrawLine(start: Vector3.zero, end: new Vector3(x, y), color);
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

    public static MyVector operator *(float b, MyVector a)
    {
        return new MyVector(x: a.x * b, y: a.y * b);
    }

}
