using UnityEngine;

public class Interactuable : MonoBehaviour
{
    [TextArea]
    public string dialogo;

    private SpriteRenderer sr;
    private Color colorOriginal;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        colorOriginal = sr.color;
    }

    public void Interactuar()
    {
        DialogoManager.Instance.MostrarDialogo(dialogo);
    }

    public void CambiarColor(Color nuevoColor)
    {
        sr.color = nuevoColor;
    }

    public void RestaurarColor()
    {
        sr.color = colorOriginal;
    }
}