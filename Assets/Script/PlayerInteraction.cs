using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private const float HIGHLIGHT_DURATION = 3f;

    [Header("Settings")]
    public float InteractionRange = 1f;
    public LayerMask InteractableLayer;

    [Header("Hidden Objects")]
    public List<GameObject> HiddenObjects;

    private bool _canUseX = true;
    private float _cooldownX = 30f;
    private bool _canInteract = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckInteraction();
        }

        if (Input.GetKeyDown(KeyCode.X) && _canUseX)
        {
            StartCoroutine(HighlightInteractables());
            StartCoroutine(StartCooldownX());
        }
    }

    public void UnlockInteraction()
    {
        _canInteract = true;
    }

    private void CheckInteraction()
    {
        if (!_canInteract)
        {
            return;
        }

        Collider2D collider = Physics2D.OverlapCircle(transform.position, InteractionRange, InteractableLayer);

        if (collider == null)
        {
            return;
        }

        Interactable interactable = collider.GetComponent<Interactable>();

        if (interactable == null)
        {
            return;
        }

        _canInteract = false;
        interactable.Interact();
    }

    private IEnumerator HighlightInteractables()
    {
        Interactable[] interactableObjects = Object.FindObjectsByType<Interactable>(FindObjectsSortMode.None);

        foreach (Interactuable interactableObject in interactableObjects)
        {
            interactableObject.ChangeColor(Color.yellow);
        }

        foreach (GameObject hiddenObject in HiddenObjects)
        {
            hiddenObject.SetActive(true);
        }

        yield return new WaitForSeconds(HIGHLIGHT_DURATION);

        foreach (Interactable interactableObject in interactableObjects)
        {
            interactableObject.RestoreColor();
        }

        foreach (GameObject hiddenObject in HiddenObjects)
        {
            hiddenObject.SetActive(false);
        }
    }

    private IEnumerator StartCooldownX()
    {
        _canUseX = false;
        yield return new WaitForSeconds(_cooldownX);
        _canUseX = true;
    }
}