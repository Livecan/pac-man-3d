using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PawnMovement movement;

    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<PawnMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleUserInput();
    }

    // Registers user input - at the next turning point (when colliding with Wall/Turning Wall) this turning would be initiated.
    private void HandleUserInput()
    {
        if (Input.GetButtonDown("Turn Left"))
        {
            movement.NextTurn = PawnMovement.MovementState.Left;
        }
        else if (Input.GetButtonDown("Turn Right"))
        {
            movement.NextTurn = PawnMovement.MovementState.Right;
        }
        else if (Input.GetButtonDown("Turn Back"))
        {
            movement.NextTurn = PawnMovement.MovementState.Back;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Game Over");

        }
    }
}
