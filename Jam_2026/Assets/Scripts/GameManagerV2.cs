using NUnit.Framework;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

public class GameManagerV2 : MonoBehaviour
{
    /*  Todo sobre el Player 1  */
    public IndicatorPlayer player_1; //Tomamos el objeto Player_1
    public VisualButton[] buttonsPlayer_1; //Tomamos las imagenes del canvas del Player_1 para los controles

    /*  Todo sobre el Player 2  */
    public IndicatorPlayer player_2; //Tomamos el objeto Player_1
    public VisualButton[] buttonsPlayer_2; //Tomamos las imagenes del canvas del Player_1 para los controles

    /*  En esta lista se almacenará la secuencia que los jugadores irán agregando hasta que uno pierda  */
    private List<int> gameSequence = new List<int>();
    private int currentIndex = 0; //Replicar la secuencia desde el inicio

    private bool canPress; //Saber si el jugador puede interactuar con los controles
    private int playerTurn; //Que jugador empezará primero
    private bool waitingNewInput = false; //Saber si se debe agregar un nuevo input a la secuencia

    public float time = 10;
    private float timeForAnswer;

    void Start()
    {
        canPress = false;
        Invoke("StartTurn", 3f);
        timeForAnswer = time;
    }

    void Update()
    {
        if (canPress == false) return;//Evitamos que se toque los controles en momentos muertos del juego

        if (playerTurn == 0) DetectControls(KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D);
        else DetectControls(KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow);

        if(gameSequence.Count >= 1) //Iniciamos un Timer
        {
            timeForAnswer -= Time.deltaTime;
        }

        if (timeForAnswer < 1)
        {
            canPress = false;
            Invoke("RepeatSequence", 3f);
       
        };

        Debug.LogWarning(Mathf.FloorToInt(timeForAnswer));
    }


    /* En este Método se realiza el ""Sorteo de cual es el jugador que comienza la partida"" */
    void StartTurn()
    {
        playerTurn = Random.Range(0, 2);
        Debug.Log(playerTurn);
        canPress = true;
    }

    /* Método para poder detectar los controles y el procesamiento que tendrá para identificar los inputs */
    void DetectControls(KeyCode up, KeyCode down, KeyCode left, KeyCode right)
    {
        if (Input.GetKeyUp(up)) ProcessInput(0);
        else if (Input.GetKeyUp(down)) ProcessInput(1);
        else if (Input.GetKeyUp(left)) ProcessInput(2);
        else if (Input.GetKeyUp(right)) ProcessInput(3);
    }

    /* En este metodo lo que se realiza es la lógica del juego en sí un player será el que empiece */
    void ProcessInput(int inputKey)
    {
        //El botón del jugador actual parpadea al tocarlo
        if (playerTurn == 0) buttonsPlayer_1[inputKey].Brilla();
        else buttonsPlayer_2[inputKey].Brilla();

        //Agregar un nuevo input si la repetición fue exitosa
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

        //Dar el primer input del juego para poder iniciar el Versus
        if (gameSequence.Count == 0)
        {
            gameSequence.Add(inputKey);
            canPress = false;
            Invoke("ChangeTurn", 0.5f);
            return;
        }

        //Realizar la secuencia mostrada
        if (inputKey == gameSequence[currentIndex])
        {
            currentIndex++; //Pasaremos al siguiente indice repitiendo esto

            if (currentIndex >= gameSequence.Count)
            {
                //El indicador de quien acaba de jugar se pone verde
                if (playerTurn == 0) player_1.ViewCorrect();
                else player_2.ViewCorrect();

                Debug.Log("CORRECTOOO!! Agrega una nueva interacción");

                waitingNewInput = true;
            }
        }
        else
        {
            //El indicador de quien falló se pone rojo
            if (playerTurn == 0) player_1.ViewError();
            else player_2.ViewError();
            Debug.Log("FALLASTE!! Se reinicia la secuencia");
            canPress = false;
            Invoke("RepeatSequence", 3f);
        }
    }

    /* Método para cambiar de turno de los jugadores */
    void ChangeTurn() 
    {
        playerTurn = (playerTurn == 0) ? 1 : 0;
        canPress = true;
    }

    /* Método para poder reiniciar una secuencia cuando unos de los jugadores se equivocaron */
    void RepeatSequence() 
    {
        gameSequence.Clear();
        Debug.Log("Empieza el Jugador: " + playerTurn);
        canPress = true;
        timeForAnswer = time;
    }
}
