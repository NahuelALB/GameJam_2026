using UnityEngine;
using System.Collections;

public class IndicatorPlayer : MonoBehaviour
{
    //Material normal
    public Material baseColor;
    //Materiales a cambiar
    public Material correctColor;
    public Material incorrectColor;
    public Material warningColor; // AGREGADO: Material para el fallo naranja

    //Tomar materiales del Mesh
    public Renderer rendMaterials;

    void Start()
    {
      //  rendMaterials.material = baseColor;
    }

    public void ViewCorrect()
    {
       // StartCoroutine(FlashGreen());
    }

    // AGREGADO: Método para mostrar el color naranja al perder una vida
    public void ViewWarning()
    {
       // StartCoroutine(FlashOrange());
    }

    public void ViewError()
    {
       // StartCoroutine(FlashRed());
    }

    IEnumerator FlashGreen()
    {
        rendMaterials.material = correctColor; // Se pone verde
        yield return new WaitForSeconds(0.8f); // Espera casi un segundo
        rendMaterials.material = baseColor; // Vuelve al color base
    }

    // AGREGADO: Corrutina para el flash naranja
    IEnumerator FlashOrange()
    {
        rendMaterials.material = warningColor;
        yield return new WaitForSeconds(0.8f);
        rendMaterials.material = baseColor;
    }

    IEnumerator FlashRed()
    {
        rendMaterials.material = incorrectColor; // Se pone rojo
        yield return new WaitForSeconds(0.8f); // Espera casi un segundo
        rendMaterials.material = baseColor; // Vuelve al color base
    }
}