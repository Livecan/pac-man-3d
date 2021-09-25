using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnMovement : MonoBehaviour
{
    private float turnAngleLeft = -1;
    private MovementState pawnMovementState = MovementState.Forward;
    public MovementState PawnMovementState { get => pawnMovementState; }

    public MovementState nextTurn = MovementState.Forward;
    public MovementState NextTurn { set => nextTurn = value; }

    public enum MovementState { Left, Right, Back, Forward, Stop };

    private float turningSpeed = 3;
    public float movingSpeed = 3.5f;
    private float tileSize = 1;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if ((pawnMovementState == MovementState.Forward || pawnMovementState == MovementState.Stop) && nextTurn == MovementState.Back)
        {
            StartTurning(180, MovementState.Left);
        }

        if (pawnMovementState == MovementState.Stop && nextTurn != MovementState.Forward)
        {
            StartTurning(90, nextTurn);
        }

        if (pawnMovementState == MovementState.Left || pawnMovementState == MovementState.Right || pawnMovementState == MovementState.Back)
        {
            RotatePlayer();
        }
        else if (pawnMovementState == MovementState.Forward)
        {
            transform.Translate(Vector3.forward * movingSpeed * Time.deltaTime);
        }
    }

    // Sets player's state to turning in the given direction
    private void StartTurning(float degreesLeft, MovementState direction)
    {
        turnAngleLeft = degreesLeft;
        pawnMovementState = direction;
        nextTurn = MovementState.Forward;
    }

    // Rotates the player according to the player's turning direction and rotation left
    private void RotatePlayer()
    {
        float currentRotation = turningSpeed * 90 * Time.deltaTime;
        transform.Rotate(Vector3.up, (pawnMovementState == MovementState.Left ? -currentRotation : currentRotation));
        turnAngleLeft -= currentRotation;
        if (turnAngleLeft < 0)
        {
            transform.eulerAngles = new Vector3(
                transform.eulerAngles.x,
                Mathf.RoundToInt(transform.eulerAngles.y / 90) * 90,
                transform.eulerAngles.z);
            pawnMovementState = MovementState.Forward;
        }
    }

    private bool IsMovingAlongZAxis()
    {
        return ((int)Mathf.Abs(transform.eulerAngles.y % 180)) == 0;
    }

    // Aligns player in appropriate distance from the given wall
    private void AlignPlayer(GameObject wall)
    {
        if (IsMovingAlongZAxis())
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

    // After player hits hits a turning wall, if next turn is registered,
    // it is started and the player is aligned in appropriate dostance from the wall.
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            List<WallTurnsHint.PassType> turnHint = other.gameObject.GetComponent<WallTurnsHint>().availablePasses;
            pawnMovementState = MovementState.Stop;
            AlignPlayer(other.gameObject);

            int playerRotation = ((int)transform.eulerAngles.y % 360);
            //coming from bottom
            if (playerRotation == 0 &&
                !(turnHint.Contains(WallTurnsHint.PassType.LeftBottom) && turnHint.Contains(WallTurnsHint.PassType.RightBottom)))
            {
                StartTurning(90, turnHint.Contains(WallTurnsHint.PassType.LeftBottom) ? MovementState.Left : MovementState.Right);
                nextTurn = MovementState.Forward;
            }
            //coming from left
            if (playerRotation == 90 &&
                !(turnHint.Contains(WallTurnsHint.PassType.LeftUp) && turnHint.Contains(WallTurnsHint.PassType.LeftBottom)))
            {
                StartTurning(90, turnHint.Contains(WallTurnsHint.PassType.LeftBottom) ? MovementState.Right : MovementState.Left);
                nextTurn = MovementState.Forward;
            }
            //coming from up
            if (playerRotation == 180 &&
                !(turnHint.Contains(WallTurnsHint.PassType.LeftUp) && turnHint.Contains(WallTurnsHint.PassType.RightUp)))
            {
                StartTurning(90, turnHint.Contains(WallTurnsHint.PassType.LeftUp) ? MovementState.Right : MovementState.Left);
                nextTurn = MovementState.Forward;
            }
            //coming from right
            if (playerRotation == 270 &&
                !(turnHint.Contains(WallTurnsHint.PassType.RightUp) && turnHint.Contains(WallTurnsHint.PassType.RightBottom)))
            {
                StartTurning(90, turnHint.Contains(WallTurnsHint.PassType.RightUp) ? MovementState.Right : MovementState.Left);
                nextTurn = MovementState.Forward;
            }

        }

        if (other.gameObject.CompareTag("Turning Wall") && nextTurn != MovementState.Forward)
        {
            List<WallTurnsHint.PassType> turnHint = other.gameObject.GetComponent<WallTurnsHint>().availablePasses;
            int playerRotation = ((int)transform.eulerAngles.y % 360);

            if ((playerRotation == 0 &&
                    turnHint.Contains(nextTurn == MovementState.Left ? WallTurnsHint.PassType.LeftBottom : WallTurnsHint.PassType.RightBottom)) ||    //from bottom
                (playerRotation == 90 &&
                    turnHint.Contains(nextTurn == MovementState.Left ? WallTurnsHint.PassType.LeftUp : WallTurnsHint.PassType.LeftBottom)) ||   //from left
                (playerRotation == 180 &&
                    turnHint.Contains(nextTurn == MovementState.Left ? WallTurnsHint.PassType.RightUp : WallTurnsHint.PassType.LeftUp)) ||  //from top
                (playerRotation == 270 &&
                    turnHint.Contains(nextTurn == MovementState.Left ? WallTurnsHint.PassType.RightBottom : WallTurnsHint.PassType.RightUp))) //from right
            {
                AlignPlayer(other.gameObject);
                StartTurning(90, nextTurn);
            }
        }
    }
}
