using UnityEngine;
using System.Collections;

public class IndicadorPlayer : MonoBehaviour
{
    //Material normal
    public Material baseColor;
    //Materiales a cambiar
    public Material correctColor;
    public Material incorrectColor;

    //Tomar materiales del Mesh
    public Renderer rendMaterials;

    void Start()
    {
        rendMaterials.material = baseColor;
    }

    public void ViewCorrect()
    {
        StartCoroutine(FlashGreen());
    }

    IEnumerator FlashGreen()
    {
        rendMaterials.material = correctColor; // Se pone verde
        yield return new WaitForSeconds(0.8f); // Espera casi un segundo
        rendMaterials.material = baseColor; // Vuelve al color base
    }

    public void ViewError()
    {
        StartCoroutine(FlashRed());
    }

    IEnumerator FlashRed()
    {
        rendMaterials.material = incorrectColor; // Se pone verde
        yield return new WaitForSeconds(0.8f); // Espera casi un segundo
        rendMaterials.material = baseColor; // Vuelve al color base
    }
}