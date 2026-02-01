using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Música de Fondo")]
    public AudioSource backgroundMusic; // Arrastra aquí tu canción
    [Range(0f, 1f)] public float musicVolume = 0.5f;

    [Header("Sonidos Player 1 (W, S, A, D)")]
    public AudioSource soundW;
    public AudioSource soundS;
    public AudioSource soundA;
    public AudioSource soundD;
    public AudioSource failP1;

    [Header("Sonidos Player 2 (Flechas)")]
    public AudioSource soundUp;
    public AudioSource soundDown;
    public AudioSource soundLeft;
    public AudioSource soundRight;
    public AudioSource failP2;

    private void Start()
    {
        // Configuración de la música al iniciar
        if (backgroundMusic != null)
        {
            backgroundMusic.loop = true;      // Para que se repita siempre
            backgroundMusic.volume = musicVolume;
            backgroundMusic.Play();           // Empieza a sonar
        }
    }

    public void PlayGameSound(int player, int keyIndex, bool isCorrect)
    {
        if (!isCorrect)
        {
            if (player == 0) failP1.Play();
            else failP2.Play();
            return;
        }

        if (player == 0) // Player 1
        {
            switch (keyIndex)
            {
                case 0: soundW.Play(); break;
                case 1: soundS.Play(); break;
                case 2: soundA.Play(); break;
                case 3: soundD.Play(); break;
            }
        }
        else // Player 2
        {
            switch (keyIndex)
            {
                case 0: soundUp.Play(); break;
                case 1: soundDown.Play(); break;
                case 2: soundLeft.Play(); break;
                case 3: soundRight.Play(); break;
            }
        }
    }
}