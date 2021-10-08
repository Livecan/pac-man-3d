using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private PawnMovement movement;
    public int turnThreshold = 20;

    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<PawnMovement>();
        StartCoroutine(ChooseRandomTurnCoroutine());
    }

    IEnumerator ChooseRandomTurnCoroutine()
    {
        while (true)
        {
            ChooseRandomTurn();

            yield return new WaitForSeconds(0.5f);
        }
    }

    private void ChooseRandomTurn()
    {
        int randomMax = movement.PawnMovementState == PawnMovement.MovementState.Stop ?
                turnThreshold :
                100;
        int random = Random.Range(0, randomMax);
        if (random < turnThreshold / 2)
        {
            movement.NextTurn = PawnMovement.MovementState.Left;
        }
        else if (random < turnThreshold)
        {
            movement.NextTurn = PawnMovement.MovementState.Right;
        }
        else
        {
            movement.NextTurn = PawnMovement.MovementState.Forward;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (movement.PawnMovementState == PawnMovement.MovementState.Stop)
        {
            ChooseRandomTurn();
        }
    }
}
