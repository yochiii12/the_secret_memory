using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueManager : MonoBehaviour 
{
    public static DialogueManager Instance;

    public GameObject DialoguePanel; // Usar PascalCase; Usar inglés
    public TextMeshProUGUI DialogueText; // Usar PascalCase; Usar inglés

    public float TextSpeed = 0.03f; // Usar PascalCase; Usar inglés

    private bool ActiveDialogue = false; // Usar _camelCase; Revisar: usar un verbo de estado (is, has o can); Usar inglés
    private bool FinishedText = false; // Usar _camelCase; Revisar: usar un verbo de estado (is, has o can); Usar inglés

    private string CompleteMessage; // Usar _camelCase; Usar inglés

    void Awake()
    {
        Instance = this;
        DialoguePanel.SetActive(false);
    }

    public void ShowDialogue(string mensaje)
    {
        StopAllCoroutines();

        DialoguePanel.SetActive(true);
        ActiveDialogue = true;
        FinishedText = false;

        CompleteMessage = mensaje;

        StartCoroutine(AnimateText());
    }

    IEnumerator AnimateText() 
    {
        DialogueText.text = "";

        foreach (char letra in CompleteMessage)
        {
            DialogueText.text += letra;
            yield return new WaitForSeconds(TextSpeed); // Aclarar el significado o las unidades con Tooltip

            // Si el jugador presiona E mientras se escribe → completar texto
            if (Input.GetKeyDown(KeyCode.E)) // Revisar si se puede usar retorno temprano
            {
                DialogueText.text = CompleteMessage;
                FinishedText = true;
                yield break;
            }
        }

        FinishedText = true;
    }

    void Update() // Respetar el orden de los miembros
    {
        if (!ActiveDialogue) return; // Agregar llaves al if

        if (Input.GetKeyDown(KeyCode.E)) // Revisar si se puede usar retorno temprano
        {
            if (!FinishedText)
            {
                StopAllCoroutines();
                DialogueText.text = CompleteMessage;
                FinishedText = true;
            }
            else
            {
                DialoguePanel.SetActive(false);
                ActiveDialogue = false;

                // ← DESBLOQUEAR INTERACCIÓN DEL JUGADOR // Explicar el porqué, no el cómo
                InteraccionJugador jugador = FindObjectOfType<InteraccionJugador>(); // Revisar la inyección de dependencias; Usar inglés
                jugador.DesbloquearInteraccion();
            }
        }
    }
}
