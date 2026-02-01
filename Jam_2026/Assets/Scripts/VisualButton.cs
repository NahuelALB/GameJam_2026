using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VisualButton : MonoBehaviour
{

    public Image arrow; //Referencia de la imagen
    private Color originalColor;
    public Color brightColor;
    public float velocidadDesvanecimiento = 3f; // Qu tan repido se apaga

    void Awake()
    {
        //Tomamos el color original del botón
        arrow = GetComponent<Image>();
        originalColor = arrow.color;
    }

    void Start()
    {
        arrow.enabled = false;
    }

    public void Brilla()
    {
        // Detenemos cualquier brillo previo para que no se encimen
        StopAllCoroutines();
        StartCoroutine(EfectoBrilloSuave());
    }

    IEnumerator EfectoBrilloSuave()
    {
        //0. Activar imagen para que sea visible
        arrow.enabled = true;
        // 1. Encendido instant neo al color brillante
        arrow.color = brightColor;
        
        //2. Apagado progresivo (Fade out)
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
        arrow.enabled = false;
    }
}