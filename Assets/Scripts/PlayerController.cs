using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float turnAngleLeft = -1;
    private Direction turningDirection = Direction.None;
    enum Direction { Left, Right, None };

    private float turningSpeed = 2;
    private float movingSpeed = 3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Turn Left") && turningDirection == Direction.None)
        {
            turnAngleLeft = 90;
            turningDirection = Direction.Left;
        } else if (Input.GetButtonDown("Turn Right") && turningDirection == Direction.None)
        {
            turnAngleLeft = 90;
            turningDirection = Direction.Right;
        }

        if (turningDirection != Direction.None)
        {
            float currentRotation = turningSpeed * 90 * Time.deltaTime;
            transform.Rotate(Vector3.up, (turningDirection == Direction.Left ? -currentRotation : currentRotation));
            turnAngleLeft -= currentRotation;
            if (turnAngleLeft < 0)
            {
                transform.eulerAngles = new Vector3(
                    transform.eulerAngles.x,
                    Mathf.RoundToInt(transform.eulerAngles.y / 90) * 90,
                    transform.eulerAngles.z);
                turningDirection = Direction.None;
            }
        } else
        {
            transform.Translate(Vector3.forward * movingSpeed * Time.deltaTime);
        }
    }
}
