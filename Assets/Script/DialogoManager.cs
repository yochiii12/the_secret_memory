using UnityEngine;
using TMPro;
using System.Collections;

public class DialogoManager : MonoBehaviour
{
    public static DialogoManager Instance;

    public GameObject panelDialogo;
    public TextMeshProUGUI textoDialogo;

    public float velocidadTexto = 0.03f; // Velocidad de animación

    private bool dialogoActivo = false;
    private bool textoTerminado = false;

    void Awake()
    {
        Instance = this;
        panelDialogo.SetActive(false);
    }

    public void MostrarDialogo(string mensaje)
    {
        StopAllCoroutines(); // Por si se llama dos veces
        panelDialogo.SetActive(true);
        dialogoActivo = true;
        textoTerminado = false;

        StartCoroutine(AnimarTexto(mensaje));
    }

    IEnumerator AnimarTexto(string mensaje)
    {
        textoDialogo.text = "";

        foreach (char letra in mensaje)
        {
            textoDialogo.text += letra;
            yield return new WaitForSeconds(velocidadTexto);
        }

        textoTerminado = true;
    }

    void Update()
    {
        if (dialogoActivo && Input.GetKeyDown(KeyCode.E))
        {
            if (textoTerminado)
            {
                // Cerrar diálogo
                panelDialogo.SetActive(false);
                dialogoActivo = false;
            }
            else
            {
                // Mostrar texto completo instantáneamente
                StopAllCoroutines();
                textoDialogo.text = textoDialogo.text = textoDialogo.text = textoDialogo.text; // redundante pero seguro
                textoTerminado = true;
            }
        }
    }
}
