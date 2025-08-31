using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMove : MonoBehaviour
{
    public float speed = 5f;        
    public float leftLimit = -9f;  
    public float rightLimit = 9f;   
    private int direction = 1;  // 1 = right, -1 = left

    void Update()
    {
        // move the obstacle on X axis 
        transform.Translate(Vector3.right * speed * direction * Time.deltaTime);

        // if it goes past the right limit it changes direction to left
        if (transform.position.x >= rightLimit)
        {
            direction = -1;// -1 means going left 
        }

        // if it goes past the left limit it changes direction to right
        if (transform.position.x <= leftLimit)
        {
            direction = 1;
        }
    }
}
