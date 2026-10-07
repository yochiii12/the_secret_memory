using UnityEngine;
using TMPro;
using System.Collections;

public class DialogoManager : MonoBehaviour
{
    public static DialogoManager Instance;

    public GameObject panelDialogo;
    public TextMeshProUGUI textoDialogo;

    public float velocidadTexto = 0.03f;

    private bool dialogoActivo = false;
    private bool textoTerminado = false;

    private string mensajeCompleto;

    void Awake()
    {
        Instance = this;
        panelDialogo.SetActive(false);
    }

    public void MostrarDialogo(string mensaje)
    {
        StopAllCoroutines();

        panelDialogo.SetActive(true);
        dialogoActivo = true;
        textoTerminado = false;

        mensajeCompleto = mensaje;

        StartCoroutine(AnimarTexto());
    }

    IEnumerator AnimarTexto()
    {
        textoDialogo.text = "";

        foreach (char letra in mensajeCompleto)
        {
            textoDialogo.text += letra;
            yield return new WaitForSeconds(velocidadTexto);

            // Si el jugador presiona E mientras se escribe → completar texto
            if (Input.GetKeyDown(KeyCode.E))
            {
                textoDialogo.text = mensajeCompleto;
                textoTerminado = true;
                yield break;
            }
        }

        textoTerminado = true;
    }

    void Update()
    {
        if (!dialogoActivo) return;

        if (Input.GetKeyDown(KeyCode.E))
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

                // ← DESBLOQUEAR INTERACCIÓN DEL JUGADOR
                InteraccionJugador jugador = FindObjectOfType<InteraccionJugador>();
                jugador.DesbloquearInteraccion();
            }
        }
    }
}
