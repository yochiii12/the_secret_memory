using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("UI Elements")]
    public GameObject DialoguePanel;
    public TextMeshProUGUI DialogueText;

    [Header("Text Settings")]
    [Tooltip("Delay in seconds between each character written to screen")]
    public float TextSpeed = 0.03f;

    [Header("External Dependencies")]
    [SerializeField] private PlayerInteraction _playerInteraction;

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

        DialoguePanel.SetActive(false);
        _isDialogueActive = false;

        if (_playerInteraction != null)
        {
            _playerInteraction.UnlockInteraction();
        }
    }

    public void ShowDialogue(string message)
    {
        StopAllCoroutines();

        DialoguePanel.SetActive(true);
        _isDialogueActive = true;
        _hasTextFinished = false;

        _fullMessage = message;

        StartCoroutine(AnimateText());
    }

    private IEnumerator AnimateText()
    {
        DialogueText.text = "";

        foreach (char letter in _fullMessage)
        {
            DialogueText.text += letter;
            yield return new WaitForSeconds(TextSpeed);

            if (!Input.GetKeyDown(KeyCode.E))
            {
                continue;
            }

            DialogueText.text = _fullMessage;
            _hasTextFinished = true;
            yield break;
        }

        _hasTextFinished = true;
    }
}
