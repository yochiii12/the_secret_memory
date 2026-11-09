// Sugerencia general de Clean Code: usar if de una sola línea con llaves cuando sea legible.

using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueManager : MonoBehaviour  // Revisar: la clase DialogueManager también está declarada en otros archivos
{
    public static DialogueManager Instance;
    [Header("UI Text")]
    public GameObject DialoguePanel; 
    public TextMeshProUGUI DialogueText;
    [Header("Text Settings")]
    public float TextSpeed = 0.03f; // Usar PascalCase; Usar inglés
    [Header("External Dependencies")]
    [SerializeField] private PlayerInteraction _playerInteraction; // Separar los grupos de acceso con una línea en blanco
    private bool _isDialogueActive = false;
    private bool _hasTextFinished = false;
    private string _fullMessage;

    void Awake()
    {
        Instance = this;
        DialoguePanel.SetActive(false);
    }

    void Update()
    {
        if (!_isDialogueActive)
        {
            return;
        }

        if (!Input.GetKeyDown(KeyCode.E))
        {
            return;
        }

        if (!_hasTextFinished)
        {
            StopAllCoroutines();
            DialogueText.text = _fullMessage;
            _hasTextFinished = true;
            return;
        }
    }
    public void ShowDialogue(string message)
    {
        StopAllCoroutines();

        DialoguePanel.SetActive(true);
        ActiveDialogue = true;
        FinishedText = false;

        CompleteMessage = message;

        StartCoroutine(AnimateText());
    }

    IEnumerator AnimateText() 
    {
        DialogueText.text = "";

        foreach (char letter in CompleteMessage)
        {
            DialogueText.text += letter;
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
