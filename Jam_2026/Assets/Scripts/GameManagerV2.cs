using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class GameManagerV2 : MonoBehaviour
{
    // Todo sobre el Player 1
    public IndicatorPlayer player_1;
    public Animator Player_Robot;
    public VisualButton[] buttonsPlayer_1;
    public Slider sliderP1; //Referencia a la barra del Jugador 

    // Todo sobre el Player 2
    public IndicatorPlayer player_2;
    public Animator Player_Coquena;
    public VisualButton[] buttonsPlayer_2;
    public Slider sliderP2;

    // Todo sobre la lógica del juego
    private List<int> gameSequence = new List<int>();
    private int currentIndex = 0;
    private bool canPress;
    private int playerTurn;
    private bool waitingNewInput = false;

    // Control de vidas
    private int p1Lives = 3;
    private int p2Lives = 3;

    // Contadores de teclas acertadas
    private int p1Hits = 0;
    private int p2Hits = 0;

    // Todo sobre la UI
    public float time = 10;
    private float timeForAnswer;
    public TextMeshProUGUI timerUI;
    public GameObject player1_image;
    public GameObject player2_image;

    void Start()
    {
        canPress = false;
        Invoke("StartTurn", 3f);
        timeForAnswer = time;

        // barras en 0 al empezar
        if (sliderP1 != null) sliderP1.value = 0;
        if (sliderP2 != null) sliderP2.value = 0;

    }

    void Update()
    {
        if (canPress == false) return;

        if (playerTurn == 0)
        {
            player1_image.SetActive(true);
            player2_image.SetActive(false);
            StartCoroutine(DetectControls(KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D));
            //DetectControls(KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D);
        }
        else
        {
            player1_image.SetActive(false);
            player2_image.SetActive(true);
            StartCoroutine(DetectControls(KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow));
            //DetectControls(KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow);
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
    }

    void StartTurn()
    {
        playerTurn = Random.Range(0, 2);
        Debug.Log("Empieza el Jugador: " + playerTurn);
        canPress = true;
    }

    IEnumerator DetectControls(KeyCode up, KeyCode down, KeyCode left, KeyCode right)
    {
        if (Input.GetKeyUp(up))
        {
            ProcessInput(0);
            if (playerTurn == 0)
            {
                Player_Robot.SetBool("Idle", false);
                Player_Robot.SetBool("Move", true);
                Player_Robot.SetBool("Up", true);
                Player_Robot.SetBool("Down", false);
                Player_Robot.SetBool("Left", false);
                Player_Robot.SetBool("Right", false);

                canPress = false;
                yield return new WaitForSeconds(0.5f);
                canPress = true;
            }
            else
            {
                Player_Coquena.SetBool("Idle", false);
                Player_Coquena.SetBool("Move", true);
                Player_Coquena.SetBool("Up", true);
                Player_Coquena.SetBool("Down", false);
                Player_Coquena.SetBool("Left", false);
                Player_Coquena.SetBool("Right", false);

                canPress = false;
                yield return new WaitForSeconds(0.5f);
                canPress = true;
            }
        }
        else if (Input.GetKeyUp(down))
        {
            ProcessInput(1);
            if (playerTurn == 0)
            {
                Player_Robot.SetBool("Idle", false);
                Player_Robot.SetBool("Move", true);
                Player_Robot.SetBool("Down", true);
                Player_Robot.SetBool("Up", false);
                Player_Robot.SetBool("Left", false);
                Player_Robot.SetBool("Right", false);

                canPress = false;
                yield return new WaitForSeconds(0.5f);
                canPress = true;
            }
            else
            {
                Player_Coquena.SetBool("Idle", false);
                Player_Coquena.SetBool("Move", true);
                Player_Coquena.SetBool("Down", true);
                Player_Coquena.SetBool("Up", false);
                Player_Coquena.SetBool("Left", false);
                Player_Coquena.SetBool("Right", false);

                canPress = false;
                yield return new WaitForSeconds(0.5f);
                canPress = true;
            }
        }
        else if (Input.GetKeyUp(left))
        {
            ProcessInput(2);
            if (playerTurn == 0)
            {
                Player_Robot.SetBool("Idle", false);
                Player_Robot.SetBool("Move", true);
                Player_Robot.SetBool("Left", true);
                Player_Robot.SetBool("Right", false);
                Player_Robot.SetBool("Up", false);
                Player_Robot.SetBool("Down", false);

                canPress = false;
                yield return new WaitForSeconds(0.5f);
                canPress = true;
            }
            else
            {
                Player_Coquena.SetBool("Idle", false);
                Player_Coquena.SetBool("Move", true);
                Player_Coquena.SetBool("Left", true);
                Player_Coquena.SetBool("Right", false);
                Player_Coquena.SetBool("Up", false);
                Player_Coquena.SetBool("Down", false);

                canPress = false;
                yield return new WaitForSeconds(0.5f);
                canPress = true;
            }
        }
        else if (Input.GetKeyUp(right))
        {
            ProcessInput(3);
            if (playerTurn == 0)
            {
                Player_Robot.SetBool("Idle", false);
                Player_Robot.SetBool("Move", true);
                Player_Robot.SetBool("Right", true);
                Player_Robot.SetBool("Left", false);
                Player_Robot.SetBool("Up", false);
                Player_Robot.SetBool("Down", false);

                canPress = false;
                yield return new WaitForSeconds(0.5f);
                canPress = true;
            }
            else
            {
                Player_Coquena.SetBool("Idle", false);
                Player_Coquena.SetBool("Move", true);
                Player_Coquena.SetBool("Right", true);
                Player_Coquena.SetBool("Left", false);
                Player_Coquena.SetBool("Up", false);
                Player_Coquena.SetBool("Down", false);

                canPress = false;
                yield return new WaitForSeconds(0.5f);
                canPress = true;
            }
        }
        else
        {
            Player_Robot.SetBool("Move", false);
            Player_Robot.SetBool("Idle", true);
            Player_Coquena.SetBool("Move", false);
            Player_Coquena.SetBool("Idle", true);
        }
    }

    //void DetectControls(KeyCode up, KeyCode down, KeyCode left, KeyCode right)
    //{
    //    if (Input.GetKeyUp(up))
    //    {
    //        ProcessInput(0);
    //        if (playerTurn == 0)
    //        {
    //            Player_Robot.SetBool("Idle", false);
    //            Player_Robot.SetBool("Move", true);
    //            Player_Robot.SetBool("Up", true);
    //            Player_Robot.SetBool("Down", false);
    //            Player_Robot.SetBool("Left", false);
    //            Player_Robot.SetBool("Right", false);
    //        }
    //        else
    //        {
    //            Player_Coquena.SetBool("Idle", false);
    //            Player_Coquena.SetBool("Move", true);
    //            Player_Coquena.SetBool("Up", true);
    //            Player_Coquena.SetBool("Down", false);
    //            Player_Coquena.SetBool("Left", false);
    //            Player_Coquena.SetBool("Right", false);
    //        }
    //    }
    //    else if (Input.GetKeyUp(down))
    //    {
    //        ProcessInput(1);
    //        if (playerTurn == 0)
    //        {
    //            Player_Robot.SetBool("Idle", false);
    //            Player_Robot.SetBool("Move", true);
    //            Player_Robot.SetBool("Down", true);
    //            Player_Robot.SetBool("Up", false);
    //            Player_Robot.SetBool("Left", false);
    //            Player_Robot.SetBool("Right", false);
    //        }
    //        else
    //        {
    //            Player_Coquena.SetBool("Idle", false);
    //            Player_Coquena.SetBool("Move", true);
    //            Player_Coquena.SetBool("Down", true);
    //            Player_Coquena.SetBool("Up", false);
    //            Player_Coquena.SetBool("Left", false);
    //            Player_Coquena.SetBool("Right", false);
    //        }
    //    }
    //    else if (Input.GetKeyUp(left))
    //    {
    //        ProcessInput(2);
    //        if (playerTurn == 0)
    //        {
    //            Player_Robot.SetBool("Idle", false);
    //            Player_Robot.SetBool("Move", true);
    //            Player_Robot.SetBool("Left", true);
    //            Player_Robot.SetBool("Right", false);
    //            Player_Robot.SetBool("Up", false);
    //            Player_Robot.SetBool("Down", false);
    //        }
    //        else
    //        {
    //            Player_Coquena.SetBool("Idle", false);
    //            Player_Coquena.SetBool("Move", true);
    //            Player_Coquena.SetBool("Left", true);
    //            Player_Coquena.SetBool("Right", false);
    //            Player_Coquena.SetBool("Up", false);
    //            Player_Coquena.SetBool("Down", false);
    //        }
    //    }
    //    else if (Input.GetKeyUp(right))
    //    {
    //        ProcessInput(3);
    //        if (playerTurn == 0)
    //        {
    //            Player_Robot.SetBool("Idle", false);
    //            Player_Robot.SetBool("Move", true);
    //            Player_Robot.SetBool("Right", true);
    //            Player_Robot.SetBool("Left", false);
    //            Player_Robot.SetBool("Up", false);
    //            Player_Robot.SetBool("Down", false);
    //        }
    //        else
    //        {
    //            Player_Coquena.SetBool("Idle", false);
    //            Player_Coquena.SetBool("Move", true);
    //            Player_Coquena.SetBool("Right", true);
    //            Player_Coquena.SetBool("Left", false);
    //            Player_Coquena.SetBool("Up", false);
    //            Player_Coquena.SetBool("Down", false);
    //        }
    //    }
    //    else
    //    {
    //        Player_Robot.SetBool("Move", false);
    //        Player_Robot.SetBool("Idle", true);
    //        Player_Coquena.SetBool("Move", false);
    //        Player_Coquena.SetBool("Idle", true);
    //    }
    //}

    void ProcessInput(int inputKey)
    {
        if (playerTurn == 0) buttonsPlayer_1[inputKey].Brilla();
        else buttonsPlayer_2[inputKey].Brilla();


        if (waitingNewInput == true)
        {
            gameSequence.Add(inputKey);
            canPress = false;
            waitingNewInput = false;
            currentIndex = 0;
            timeForAnswer = 10;
            Invoke("ChangeTurn", 0.5f);
            return;
        }

        if (gameSequence.Count == 0)
        {
            gameSequence.Add(inputKey);
            canPress = false;
            Invoke("ChangeTurn", 0.5f);
            return;
        }

        // VALIDACIÓN DE TECLA
        if (inputKey == gameSequence[currentIndex])
        {
            // sumamos el acierto por cada TECLA correcta
            if (playerTurn == 0)
            {
                p1Hits++;
                if (sliderP1 != null) sliderP1.value = p1Hits; // Actualiza la barra P1
                if (p1Hits >= 10)
                {
                    p1Lives++;
                    p1Hits = 0;
                    if (sliderP1 != null) sliderP1.value = 0; // Reinicia la barra P1
                    Debug.Log("<color=cyan>¡P1 ganó vida extra! Total: " + p1Lives + "</color>");
                }
            }
            else
            {
                p2Hits++;
                if (sliderP2 != null) sliderP2.value = p2Hits; // Actualiza la barra P2
                if (p2Hits >= 10)
                {
                    p2Lives++;
                    p2Hits = 0;
                    if (sliderP2 != null) sliderP2.value = 0; // Reinicia la barra P2
                    Debug.Log("<color=cyan>¡P2 ganó vida extra! Total: " + p2Lives + "</color>");
                }
            }

            currentIndex++;
            if (currentIndex >= gameSequence.Count)
            {
                if (playerTurn == 0) player_1.ViewCorrect();
                else player_2.ViewCorrect();

                Debug.Log("CORRECTOOO!! Agrega una nueva interaccion");
                waitingNewInput = true;
            }
        }
        else
        {
            ManejarFallo();
        }
    }

    void ManejarFallo()
    {
        canPress = false;

        if (playerTurn == 0)
        {
            p1Lives--;
            p1Hits = 0; // Se pierde la racha al fallar
            if (sliderP1 != null) sliderP1.value = 0; // Reinicia la barra al fallar
            Debug.Log("P1 FALLÓ. Vidas: " + p1Lives);
            if (p1Lives > 0) player_1.ViewWarning();
            else player_1.ViewError();
        }
        else
        {
            p2Lives--;
            p2Hits = 0; // Se pierde la racha al fallar
            if (sliderP2 != null) sliderP2.value = 0; //Reinicia la barra al fallar
            Debug.Log("P2 FALLÓ. Vidas: " + p2Lives);
            if (p2Lives > 0) player_2.ViewWarning();
            else player_2.ViewError();
        }

        if (p1Lives <= 0 || p2Lives <= 0)
        {
            Debug.Log("GAME OVER!!");
            Invoke("RepeatSequence", 3f);
        }
        else
        {
            Invoke("ReintentarMismoTurno", 1.5f);
        }
    }

    void ReintentarMismoTurno()
    {
        gameSequence.Clear();
        currentIndex = 0;
        timeForAnswer = time;
        canPress = true;
    }

    void ChangeTurn()
    {
        playerTurn = (playerTurn == 0) ? 1 : 0;
        canPress = true;
        timeForAnswer = time;
    }

    void RepeatSequence()
    {
        gameSequence.Clear();
        p1Lives = 3;
        p2Lives = 3;
        p1Hits = 0;
        p2Hits = 0;
        if (sliderP1 != null) sliderP1.value = 0; // Reset visual
        if (sliderP2 != null) sliderP2.value = 0; // Reset visual
        currentIndex = 0;
        canPress = true;
        timeForAnswer = time;
    }
}