using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnMovement : MonoBehaviour
{
    private float turnAngleLeft = -1;
    private MovementState playerMovementState = MovementState.Forward;
    public enum MovementState { Left, Right, Back, Forward, Stop };

    private MovementState? nextTurn;
    public MovementState NextTurn { set => nextTurn = value; }

    private float turningSpeed = 2;
    private float movingSpeed = 3;
    private float tileSize = 1;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if ((playerMovementState == MovementState.Forward || playerMovementState == MovementState.Stop) && nextTurn == MovementState.Back)
        {
            StartTurning(180, MovementState.Left);
        }

        if (playerMovementState == MovementState.Stop && nextTurn != null)
        {
            StartTurning(90, (MovementState)nextTurn);
        }

        if (playerMovementState == MovementState.Left || playerMovementState == MovementState.Right || playerMovementState == MovementState.Back)
        {
            RotatePlayer();
        }
        else if (playerMovementState == MovementState.Forward)
        {
            transform.Translate(Vector3.forward * movingSpeed * Time.deltaTime);
        }
    }

    // Sets player's state to turning in the given direction
    private void StartTurning(float degreesLeft, MovementState direction)
    {
        turnAngleLeft = degreesLeft;
        playerMovementState = direction;
        nextTurn = null;
    }

    // Rotates the player according to the player's turning direction and rotation left
    private void RotatePlayer()
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
    }

    // Aligns player in appropriate distance from the given wall
    private void AlignPlayer(GameObject wall)
    {
        if (Mathf.Abs(transform.eulerAngles.y % 180) < 1)
        {
            float zDimension = transform.position.z < wall.transform.position.z ?
                wall.transform.position.z - tileSize / 2 :
                wall.transform.position.z + tileSize / 2;
            transform.position = new Vector3(transform.position.x, transform.position.y, zDimension);
        }
        else
        {
            float xDimension = transform.position.x < wall.transform.position.x ?
                wall.transform.position.x - tileSize / 2 :
                wall.transform.position.x + tileSize / 2;
            transform.position = new Vector3(xDimension, transform.position.y, transform.position.z);
        }
    }

    // After player hits a Wall, he stops and is put in appropriate distance from the wall.
    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            playerMovementState = MovementState.Stop;
            AlignPlayer(other.gameObject);
        }
    }

    // After player hits hits a turning wall, if next turn is registered,
    // it is started and the player is aligned in appropriate dostance from the wall.
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Turning Wall") && nextTurn != null)
        {
            AlignPlayer(other.gameObject);
            StartTurning(90, (MovementState)nextTurn);
        }
    }
}
