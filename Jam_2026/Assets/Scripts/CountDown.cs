using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    public Image countdownImage;
    public List<Sprite> sprites; // [3,2,1,YA]
    
    void Start()
    {
        StartCoroutine(Countdown());

    }

    IEnumerator Countdown()
    {
        countdownImage.gameObject.SetActive(true);

        for (int i = 0; i < sprites.Count; i++)
        {
            countdownImage.sprite = sprites[i];

            if (i > 2)
            {
                countdownImage.rectTransform.sizeDelta = new Vector2(1000, 500);
            }
            yield return new WaitForSeconds(1f);
        }

        countdownImage.gameObject.SetActive(false);
    }
}
