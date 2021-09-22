﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float turnAngleLeft = -1;
    private MovementState playerMovementState = MovementState.Forward;
    enum MovementState { Left, Right, Back, Forward, Stop };

    private MovementState? nextTurn;

    private Rigidbody playerRigidbody;
    private float turningSpeed = 2;
    private float movingSpeed = 3;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Turn Left"))
        {
            nextTurn = MovementState.Left;
        } else if (Input.GetButtonDown("Turn Right"))
        {
            nextTurn = MovementState.Right;
        } else if (Input.GetButtonDown("Turn Back"))
        {
            nextTurn = MovementState.Back;
        }

        if ((playerMovementState == MovementState.Forward || playerMovementState == MovementState.Stop) && nextTurn == MovementState.Back)
        {
            turnAngleLeft = 180;
            playerMovementState = MovementState.Left;
            nextTurn = null;
        }

        if (playerMovementState == MovementState.Stop && nextTurn != null)
        {
            turnAngleLeft = 90;
            playerMovementState = (MovementState)nextTurn;
            nextTurn = null;
        }

        if (playerMovementState == MovementState.Left || playerMovementState == MovementState.Right || playerMovementState == MovementState.Back)
        {
            float currentRotation = turningSpeed * 90 * Time.deltaTime;
            transform.Rotate(Vector3.up, (playerMovementState == MovementState.Left ? -currentRotation : currentRotation));
            turnAngleLeft -= currentRotation;
            if (turnAngleLeft < 0)
            {
                transform.eulerAngles = new Vector3(
                    transform.eulerAngles.x,
                    Mathf.RoundToInt(transform.eulerAngles.y / 90) * 90,
                    transform.eulerAngles.z);
                playerMovementState = MovementState.Forward;
            }
        } else if (playerMovementState == MovementState.Forward)
        {
            
            
            transform.Translate(Vector3.forward * movingSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            playerMovementState = MovementState.Stop;
            if (Mathf.Abs(transform.eulerAngles.y % 180) < 1)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Round(transform.position.z));
            } else
            {
                transform.position = new Vector3(Mathf.Round(transform.position.x), transform.position.y, transform.position.z);
            }
        }
    }
}