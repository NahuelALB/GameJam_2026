using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    //player 1
    public BotonVisual[] botonesP1;
    public IndicadorPlayer indicadorP1;

    //Player 2
    public BotonVisual[] botonesP2;
    public IndicadorPlayer indicadorP2;

    private List<int> secuenciaMaster = new List<int>();
    private int turnoJugador = 1;
    private int indicePasoActual = 0;
    private bool puedePresionar = false;

    void Start()
    {
        IniciarNuevoTurno();
    }

    void IniciarNuevoTurno()
    {
        puedePresionar = false;
        indicePasoActual = 0;
        secuenciaMaster.Add(Random.Range(0, 4));
        StartCoroutine(MostrarSecuencia());
    }

    IEnumerator MostrarSecuencia()
    {
        yield return new WaitForSeconds(1f);

        BotonVisual[] botonesActuales = (turnoJugador == 1) ? botonesP1 : botonesP2;

        foreach (int index in secuenciaMaster)
        {
            botonesActuales[index].Brilla();
            yield return new WaitForSeconds(0.5f);
        }

        puedePresionar = true;
    }

    void Update()
    {
        if (!puedePresionar) return;

        if (turnoJugador == 1) DetectarTecla(KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D);
        else DetectarTecla(KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow);
    }

    void DetectarTecla(KeyCode u, KeyCode d, KeyCode l, KeyCode r)
    {
        if (Input.GetKeyDown(u)) ProcesarEntrada(0);
        else if (Input.GetKeyDown(d)) ProcesarEntrada(1);
        else if (Input.GetKeyDown(l)) ProcesarEntrada(2);
        else if (Input.GetKeyDown(r)) ProcesarEntrada(3);
    }

    void ProcesarEntrada(int id)
    {
        //El botón del jugador actual parpadea al tocarlo
        if (turnoJugador == 1) botonesP1[id].Brilla();
        else botonesP2[id].Brilla();

        if (id == secuenciaMaster[indicePasoActual])
        {
            indicePasoActual++;
            if (indicePasoActual >= secuenciaMaster.Count)
            {
                //El indicador de quien acaba de jugar se pone veRDE
                if (turnoJugador == 1) indicadorP1.ViewCorrect();
                else indicadorP2.ViewCorrect();

                CambiarDeTurno();
            }
        }
        else
        {
            //El indicador de quien falló se pone rojo
            if (turnoJugador == 1) indicadorP1.ViewError();
            else indicadorP2.ViewError();

            Invoke("ReiniciarTodo", 1.5f);
        }
    }

    void CambiarDeTurno()
    {
        turnoJugador = (turnoJugador == 1) ? 2 : 1;
        IniciarNuevoTurno();
    }

    void ReiniciarTodo()
    {
        secuenciaMaster.Clear();
        turnoJugador = 1;
        IniciarNuevoTurno();
    }
}