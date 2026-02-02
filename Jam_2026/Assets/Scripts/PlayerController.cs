using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Todo sobre el Player
    public Animator playerAnimator;
    public VisualButton[] buttonsPlayer;
    public Slider sliderPowerUp;

    // Control de vidas
    public int playerLives = 3;
    public TextMeshProUGUI healthPlayer;

    // Contadores de teclas acertadas
    public int playerHits = 0;

    void PlayAnimation()
    {
        
    }
}
