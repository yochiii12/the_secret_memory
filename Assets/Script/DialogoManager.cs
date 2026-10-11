using UnityEngine;
using TMPro;
using System.Collections;

public class DialogoManager : MonoBehaviour // Usar inglés
{
    public static DialogoManager Instance;

    public GameObject panelDialogo; // Usar PascalCase; Usar inglés
    public TextMeshProUGUI textoDialogo; // Usar PascalCase; Usar inglés

    public float velocidadTexto = 0.03f; // Usar PascalCase; Usar inglés

    private bool dialogoActivo = false; // Usar _camelCase; Revisar: usar un verbo de estado (is, has o can); Usar inglés
    private bool textoTerminado = false; // Usar _camelCase; Revisar: usar un verbo de estado (is, has o can); Usar inglés

    private string mensajeCompleto; // Usar _camelCase; Usar inglés

    void Awake()
    {
        Instance = this;
        panelDialogo.SetActive(false);
    }

    public void MostrarDialogo(string mensaje) // Usar inglés
    {
        StopAllCoroutines();

        panelDialogo.SetActive(true);
        dialogoActivo = true;
        textoTerminado = false;

        mensajeCompleto = mensaje;

        StartCoroutine(AnimarTexto());
    }

    IEnumerator AnimarTexto() // Usar inglés
    {
        textoDialogo.text = "";

        foreach (char letra in mensajeCompleto) // Usar inglés
        {
            textoDialogo.text += letra;
            yield return new WaitForSeconds(velocidadTexto); // Aclarar el significado o las unidades con Tooltip

            // Si el jugador presiona E mientras se escribe → completar texto
            if (Input.GetKeyDown(KeyCode.E)) // Revisar si se puede usar retorno temprano
            {
                textoDialogo.text = mensajeCompleto;
                textoTerminado = true;
                yield break;
            }
        }

        textoTerminado = true;
    }

    void Update() // Respetar el orden de los miembros
    {
        if (!dialogoActivo) return; // Agregar llaves al if

        if (Input.GetKeyDown(KeyCode.E)) // Revisar si se puede usar retorno temprano
        {
            if (!textoTerminado)
            {
                StopAllCoroutines();
                textoDialogo.text = mensajeCompleto;
                textoTerminado = true;
            }
            else
            {
                panelDialogo.SetActive(false);
                dialogoActivo = false;

                // ← DESBLOQUEAR INTERACCIÓN DEL JUGADOR // Explicar el porqué, no el cómo
                InteraccionJugador jugador = FindObjectOfType<InteraccionJugador>(); // Revisar la inyección de dependencias; Usar inglés
                jugador.DesbloquearInteraccion();
            }
        }
    }
}
