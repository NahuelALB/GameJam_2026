using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManagerV2 : MonoBehaviour
{
    [Header ("Referencia al Player")]

    public PlayerController player1;
    public PlayerController player2;

    [Header ("Referencia al Indicador de turno")]
    public Indicator indicatorCube;

    // Todo sobre la lógica del juego
    private List<int> gameSequence = new List<int>();
    private int currentIndex = 0;
    private bool canPress;
    public int playerTurn;
    private bool waitingNewInput = false;

    [Header ("Interfaz del Temporizador")]
    public float time = 8;
    private float timeForAnswer;
    public TextMeshProUGUI timerUI;

    void Start()
    {
        canPress = false;
        Invoke("StartTurn", 3f);
        timeForAnswer = time;

        if (player1.sliderPowerUp != null) player1.sliderPowerUp.value = 0;
        if (player2.sliderPowerUp != null) player2.sliderPowerUp.value = 0;
    }

    void Update()
    {
        indicatorCube.MoveAndRotateIndicator();
        
        if (canPress == false) return;

        if (playerTurn == 0)
        {
            StartCoroutine(DetectControls(KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D));
        }
        else
        {
            StartCoroutine(DetectControls(KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow));
        }

        if (gameSequence.Count >= 1)
        {
            timeForAnswer -= Time.deltaTime;
            if (timeForAnswer < 1)
            {
                ManejarFallo();
            }
        }

        timerUI.text = Mathf.FloorToInt(timeForAnswer).ToString();

        player1.healthPlayer.text = player1.playerLives.ToString();
        if(player1.playerLives >= 3) player1.maxTextLives.enabled = true;
        else player1.maxTextLives.enabled = false;

        player2.healthPlayer.text = player2.playerLives.ToString();
        if (player2.playerLives >= 3) player2.maxTextLives.enabled = true;
        else player2.maxTextLives.enabled = false;
    }

    void StartTurn()
    {
        playerTurn = Random.Range(0, 2);
        indicatorCube.ChangeColorIndicator(indicatorCube.normalColor);
        //CambiarColorIndicador(normalColor);
        Debug.Log("Empieza el Jugador: " + playerTurn);
        canPress = true;
    }

    IEnumerator DetectControls(KeyCode up, KeyCode down, KeyCode left, KeyCode right)
    {
        if (Input.GetKeyDown(up))
        {
            if (playerTurn == 0) player1.playerAnimator.SetTrigger("Up");
            else player2.playerAnimator.SetTrigger("Up");

            canPress = false;
            ProcessInput(0);
            yield return new WaitForSeconds(0.5f);
            canPress = true;


        }
        else if (Input.GetKeyDown(down))
        {
            if (playerTurn == 0) player1.playerAnimator.SetTrigger("Down");
            else player2.playerAnimator.SetTrigger("Down");

            canPress = false;
            ProcessInput(1);
            yield return new WaitForSeconds(0.5f);
            canPress = true;
        }
        else if (Input.GetKeyDown(left))
        {
            if (playerTurn == 0) player1.playerAnimator.SetTrigger("Left");
            else player2.playerAnimator.SetTrigger("Left");

            canPress = false;
            ProcessInput(2);
            yield return new WaitForSeconds(0.5f);
            canPress = true;
        }
        else if (Input.GetKeyDown(right))
        {
            if (playerTurn == 0) player1.playerAnimator.SetTrigger("Right");
            else player2.playerAnimator.SetTrigger("Right");

            canPress = false;
            ProcessInput(3);
            yield return new WaitForSeconds(0.5f);
            canPress = true;
        }
        else
        {
            yield return null;
            if (playerTurn == 0) player1.playerAnimator.SetBool("Idle", true);
            else player2.playerAnimator.SetBool("Idle", true);
        }
    }


    void ProcessInput(int inputKey)
    {
        if (playerTurn == 0) player1.buttonsPlayer[inputKey].Brilla();
        else player2.buttonsPlayer[inputKey].Brilla();

        if (waitingNewInput == true)
        {
            FindObjectOfType<AudioManager>().PlayGameSound(playerTurn, inputKey, true);
            gameSequence.Add(inputKey);
            canPress = false;
            waitingNewInput = false;
            currentIndex = 0;
            timeForAnswer = time;
            Invoke("ChangeTurn", 0.5f);
            return;
        }

        if (gameSequence.Count == 0)
        {
            FindObjectOfType<AudioManager>().PlayGameSound(playerTurn, inputKey, true);
            gameSequence.Add(inputKey);
            canPress = false;
            Invoke("ChangeTurn", 0.5f);
            return;
        }

        if (inputKey == gameSequence[currentIndex])
        {
            FindObjectOfType<AudioManager>().PlayGameSound(playerTurn, inputKey, true);
            ActualizarHits();
            currentIndex++;
            if (currentIndex >= gameSequence.Count)
            {
                FindObjectOfType<AudioManager>().PlayGameSound(playerTurn, 4, true);
                indicatorCube.ChangeColorIndicator(indicatorCube.correctColor);
                waitingNewInput = true;
            }
        }
        else
        {
            FindObjectOfType<AudioManager>().PlayGameSound(playerTurn, inputKey, false);
            ManejarFallo();
            if (playerTurn == 0)
            {
                player1.playerAnimator.SetTrigger("Error");
                StartCoroutine(player1.ResetColor());
            }
            else
            { 
                player2.playerAnimator.SetTrigger("Error");
                StartCoroutine(player2.ResetColor());
            }
        }
    }

    void ActualizarHits()
    {
        if (playerTurn == 0)
        {
            player1.playerHits++;
            if (player1.sliderPowerUp != null) player1.sliderPowerUp.value = player1.playerHits;
            if (player1.playerHits >= 10)
            {
                player1.playerLives++;
                if (player1.playerLives > 3)
                {
                    player1.playerLives = 3;
                }
                player1.playerHits = 0;
                player1.sliderPowerUp.value = 0;
            }
        }
        else
        {
            player2.playerHits++;
            if (player2.sliderPowerUp != null) player2.sliderPowerUp.value = player2.playerHits;
            if (player2.playerHits >= 10)
            {
                player2.playerLives++;
                if (player2.playerLives > 3) player2.playerLives = 3;
                player2.playerHits = 0;
                player2.sliderPowerUp.value = 0;
            }
        }
    }

    void ManejarFallo()
    {
        canPress = false;
        StartCoroutine(indicatorCube.ResetIndicatorColor());

        if (playerTurn == 0)
        {
            player1.playerLives--;
            player1.playerHits = 0;
            if (player1.sliderPowerUp != null) player1.sliderPowerUp.value = 0;
        }
        else
        {
            player2.playerLives--;
            player2.playerHits = 0;
            if (player2.sliderPowerUp != null) player2.sliderPowerUp.value = 0;
        }

        // Revisar Game Over (Corrección)
        if (player1.playerLives <= 0)
        {
            SceneManager.LoadScene(5);
            return;
        }
        if (player2.playerLives <= 0)
        {
            SceneManager.LoadScene(6);
            return;
        }

        // SI NO ES GAME OVER: Limpiar secuencia y pasar el turno al otro
        gameSequence.Clear();
        currentIndex = 0;
        //Invoke("ChangeTurn", 1.5f); // Espera un poco en rojo y luego cambia
    }

    void ChangeTurn()
    {
        playerTurn = (playerTurn == 0) ? 1 : 0;
        indicatorCube.ChangeColorIndicator(indicatorCube.normalColor); // Aquí vuelve al color normal (verde/cian)
        canPress = true;
        timeForAnswer = time;
        waitingNewInput = false;
        currentIndex = 0;
    }

    void RepeatSequence()
    {
        gameSequence.Clear();
        player1.playerLives = 3;
        player2.playerLives = 3;
        player1.playerHits = 0;
        player2.playerHits = 0;
        if (player1.sliderPowerUp != null) player1.sliderPowerUp.value = 0;
        if (player2.sliderPowerUp != null) player2.sliderPowerUp.value = 0;
        currentIndex = 0;
        canPress = true;
        timeForAnswer = time;
    }
}