using UnityEngine;

public class Interactable : MonoBehaviour
{
    [TextArea]
    public string Dialogue;

    private SpriteRenderer _spriteRenderer;
    private Color _originalColor;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _originalColor = _spriteRenderer.color;
    }

    public void Interact()
    {
        DialogueManager.Instance.ShowDialogue(Dialogue);
    }

    public void ChangeColor(Color newColor)
    {
        _spriteRenderer.color = newColor;
    }

    public void RestoreColor()
    {
        _spriteRenderer.color = _originalColor;
    }
}
}