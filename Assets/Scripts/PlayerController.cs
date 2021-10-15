using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    private PawnMovement movement;
    private AudioSource audioSource;
    [SerializeField] AudioClip eatFoodAudio, walkAudio, dieAudio;
    public UnityEvent onFoodEaten;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<PawnMovement>();
        audioSource = GetComponent<AudioSource>();
        gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
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
            onFoodEaten.Invoke();
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            audioSource.PlayOneShot(dieAudio);
            gameManager.GameOver();
            Debug.Log("Game Over");

        }
    }

    private int CountRemainingFood()
    {
        return GameObject.FindGameObjectsWithTag("Food").Length;
    }
}
