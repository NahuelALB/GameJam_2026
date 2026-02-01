using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class GameManagerV2 : MonoBehaviour
{
    // Todo sobre el Player 1
    public Animator Player_Robot;
    public VisualButton[] buttonsPlayer_1;
    public Slider sliderP1; 

    // Todo sobre el Player 2
    public Animator Player_Coquena;
    public VisualButton[] buttonsPlayer_2;
    public Slider sliderP2;

    // Indicador de turno 3D
    public Transform turnIndicator;
    public float indicatorOffset = 3f;
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;
    public Color normalColor = Color.cyan;
    public Color errorColor = Color.red; 

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


    void Start()
    {
        canPress = false;
        Invoke("StartTurn", 3f);
        timeForAnswer = time;

        if (sliderP1 != null) sliderP1.value = 0;
        if (sliderP2 != null) sliderP2.value = 0;
    }

    void Update()
    {
        MoverYRotarIndicador();

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
    }

    void MoverYRotarIndicador()
    {
        if (turnIndicator == null) return;
        Transform targetPlayer = (playerTurn == 0) ? Player_Robot.transform : Player_Coquena.transform;
        Vector3 targetPosition = targetPlayer.position + Vector3.up * indicatorOffset;
        turnIndicator.position = Vector3.Lerp(turnIndicator.position, targetPosition, Time.deltaTime * moveSpeed);
        turnIndicator.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    void CambiarColorIndicador(Color nuevoColor)
    {
        if (turnIndicator != null)
        {
            // Importante: Esto asume que el objeto tiene un MeshRenderer (como un cubo o capsula de Unity)
            turnIndicator.GetComponent<MeshRenderer>().material.color = nuevoColor;
        }
    }
    
    void StartTurn()
    {
        playerTurn = Random.Range(0, 2);
        CambiarColorIndicador(normalColor); // AGREGADO: Color inicial
        Debug.Log("Empieza el Jugador: " + playerTurn);
        canPress = true;
    }

    IEnumerator DetectControls(KeyCode up, KeyCode down, KeyCode left, KeyCode right)
    {
        if (Input.GetKeyDown(up))
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
                yield return new WaitForSeconds(0.2f);
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
                yield return new WaitForSeconds(0.2f);
                canPress = true;
            }
        }
        else if (Input.GetKeyDown(down))
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
                yield return new WaitForSeconds(0.2f);
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
                yield return new WaitForSeconds(0.2f);
                canPress = true;
            }
        }
        else if (Input.GetKeyDown(left))
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
                yield return new WaitForSeconds(0.2f);
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
                yield return new WaitForSeconds(0.2f);
                canPress = true;
            }
        }
        else if (Input.GetKeyDown(right))
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
                yield return new WaitForSeconds(0.2f);
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
                yield return new WaitForSeconds(0.2f);
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

        if (inputKey == gameSequence[currentIndex])
        {
            FindObjectOfType<AudioManager>().PlayGameSound(playerTurn, inputKey, true);
            if (playerTurn == 0)
            {
                p1Hits++;
                if (sliderP1 != null) sliderP1.value = p1Hits; 
                if (p1Hits >= 10)
                {
                    p1Lives++;
                    p1Hits = 0;
                    if (sliderP1 != null) sliderP1.value = 0; 
                }
            }
            else
            {
                p2Hits++;
                if (sliderP2 != null) sliderP2.value = p2Hits; 
                if (p2Hits >= 10)
                {
                    p2Lives++;
                    p2Hits = 0;
                    if (sliderP2 != null) sliderP2.value = 0; 
                }
            }

            currentIndex++;
            if (currentIndex >= gameSequence.Count)
            {
                Debug.Log("CORRECTOOO!!");
                waitingNewInput = true;
            }
        }
        else
        {
            FindObjectOfType<AudioManager>().PlayGameSound(playerTurn, inputKey, false);
            ManejarFallo();
        }
    }

    void ManejarFallo()
    {
        canPress = false;
        CambiarColorIndicador(errorColor); // AGREGADO: Se pone rojo al fallar

        if (playerTurn == 0)
        {
            p1Lives--;
            p1Hits = 0; 
            if (sliderP1 != null) sliderP1.value = 0; 
            Debug.Log("P1 FALLÓ. Vidas: " + p1Lives);
        }
        else
        {
            p2Lives--;
            p2Hits = 0; 
            if (sliderP2 != null) sliderP2.value = 0; 
            Debug.Log("P2 FALLÓ. Vidas: " + p2Lives);
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
        CambiarColorIndicador(normalColor); // AGREGADO: Vuelve al color normal
        canPress = true;
    }

    void ChangeTurn()
    {
        playerTurn = (playerTurn == 0) ? 1 : 0;
        CambiarColorIndicador(normalColor); // AGREGADO: Color normal para el siguiente
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
        if (sliderP1 != null) sliderP1.value = 0; 
        if (sliderP2 != null) sliderP2.value = 0; 
        currentIndex = 0;
        CambiarColorIndicador(normalColor); // AGREGADO: Reset de color
        canPress = true;
        timeForAnswer = time;
    }
}