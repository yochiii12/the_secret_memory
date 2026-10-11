using UnityEngine;

public class Interactuable : MonoBehaviour // Usar inglés
{
    [TextArea]
    public string dialogo; // Usar PascalCase; Usar inglés

    private SpriteRenderer sr; // Usar _camelCase; Sin abreviaciones
    private Color colorOriginal; // Usar _camelCase

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        colorOriginal = sr.color;
    }

    public void Interactuar() // Usar inglés
    {
        DialogoManager.Instance.MostrarDialogo(dialogo);
    }

    public void CambiarColor(Color nuevoColor) // Usar inglés
    {
        sr.color = nuevoColor;
    }

    public void RestaurarColor() // Usar inglés
    {
        sr.color = colorOriginal;
    }
}