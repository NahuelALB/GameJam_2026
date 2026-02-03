using System.Collections;
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
    public Color colorError;
    public Color normalColor;
    public int playerLives = 3;
    public TextMeshProUGUI healthPlayer;

    // Contadores de teclas acertadas
    public int playerHits = 0;

    public IEnumerator ResetColor()
    {
        gameObject.GetComponent<SpriteRenderer>().color = colorError;
        yield return new WaitForSeconds(1f);
        gameObject.GetComponent<SpriteRenderer>().color = normalColor;
    }
}
