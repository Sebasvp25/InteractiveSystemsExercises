using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelloWorld : MonoBehaviour
{
    [SerializeField] MyVector myFirstVector = new MyVector(x:-3, y:4);
    [SerializeField] MyVector mySecondVector = new MyVector(x:3, y:4);
    [Range(0, 1)] [SerializeField] float scalar = 0;

    private void Update()
    {
        

        MyVector diff = (mySecondVector - myFirstVector) * scalar;
        MyVector final = myFirstVector + diff;

        // Debug vectors
        #region Debug
        myFirstVector.Draw(Color.red);
        mySecondVector.Draw(Color.green);
        diff.Draw(Color.yellow);
        diff.Draw(myFirstVector, Color.yellow);
        final.Draw(Color.blue);
        #endregion

    }


}
