using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BotonVisual : MonoBehaviour
{
    public Image arrow; //Referencia de la imagen
    private Color originalColor;
    public Color brightColor;
    public float velocidadDesvanecimiento = 3f; // Qué tan rápido se apaga

    void Awake()
    {
        //Tomamos el color original del botón
        arrow = GetComponent<Image>();
        originalColor = arrow.color;
    }

    public void Brilla()
    {
        // Detenemos cualquier brillo previo para que no se encimen
        StopAllCoroutines();
        StartCoroutine(EfectoBrilloSuave());
    }

    IEnumerator EfectoBrilloSuave()
    {
        // 1. Encendido instantáneo al color brillante
        arrow.color = brightColor;

        // 2. Apagado progresivo (Fade out)
        float t = 0;
        while (t < 1f)
        {
            t += Time.deltaTime * velocidadDesvanecimiento;
            // Mezclamos el color brillante con el original poco a poco
            arrow.color = Color.Lerp(brightColor, originalColor, t);
            yield return null;
        }

        // Aseguramos que quede el color original al final
        arrow.color = originalColor;
    }
}