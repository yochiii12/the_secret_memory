using UnityEngine;

public class Interactuable : MonoBehaviour
{
    [TextArea]
    public string dialogo;

    public void Interactuar()
    {
        // Llama al sistema de diálogo
        DialogoManager.Instance.MostrarDialogo(dialogo);
    }
}
