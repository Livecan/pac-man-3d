using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PawnMovement movement;
    private AudioSource audioSource;
    [SerializeField] AudioClip eatFoodAudio, walkAudio, dieAudio;

    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<PawnMovement>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HandleUserInput();
        if (movement.PawnMovementState == PawnMovement.MovementState.Forward)
        {
            //audioSource.PlayOneShot(walkAudio);
        }
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
        if (other.gameObject.CompareTag("Food"))
        {
            audioSource.PlayOneShot(eatFoodAudio);
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            audioSource.PlayOneShot(dieAudio);
            Debug.Log("Game Over");

        }
    }
}
