using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateAround : MonoBehaviour
{
    public Transform centerObject; // the object around which the rotating object will rotate
    //private float radius = 1f; // the radius of the circle that the object will rotate around
    private float speed = 540f; // the speed at which the object will rotate
    private bool moveRight = true;

    //private float angle; // the current angle of rotation

    void Update()
    {
        Vector3 rotationAxis = centerObject.forward;
        float angle = speed * Time.deltaTime;

        // rotate the object around the center object
        transform.RotateAround(centerObject.position, rotationAxis, angle);


        if (Input.GetKeyDown(KeyCode.Space))
        {
            speed = 0;
            Invoke("changeSpeed", .15f);
        }
    }



    private void changeSpeed()
    {
        if (moveRight)
            speed = -540f;
        else
            speed = 540f;

        moveRight = !moveRight;
    }
}
