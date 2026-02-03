using System.Collections;
using UnityEngine;

public class Indicator : MonoBehaviour
{
    public Transform turnIndicator;
    public float indicatorOffset = 3f;
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;
    public Color normalColor;
    public Color correctColor = Color.cyan;
    public Color errorColor = Color.red;

    //Referencia al Script "GameManager"
    public GameManagerV2 gameManager;

    public void MoveAndRotateIndicator()
    {
        if (turnIndicator == null) return;

        Transform targetPlayer = (gameManager.playerTurn == 0) ? gameManager.player1.playerAnimator.transform : gameManager.player2.playerAnimator.transform;
        Vector3 targetPosition = targetPlayer.position + Vector3.up * indicatorOffset;
        turnIndicator.position = Vector3.Lerp(turnIndicator.position, targetPosition, Time.deltaTime * moveSpeed);
        turnIndicator.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    public void ChangeColorIndicator(Color newColor)
    {
        if (turnIndicator != null)
        {
            turnIndicator.GetComponent<MeshRenderer>().material.color = newColor;
        }
    }

    public IEnumerator ResetIndicatorColor()
    {
        ChangeColorIndicator(errorColor);
        yield return new WaitForSeconds(1f);
        ChangeColorIndicator(normalColor);
    }
}
