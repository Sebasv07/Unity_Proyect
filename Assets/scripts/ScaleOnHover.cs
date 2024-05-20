using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ScaleOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float hoverScale = 1.1f; // Factor de escala al pasar el mouse por encima
    private Vector3 originalScale; // Escala original de la imagen

    private void Start()
    {
        originalScale = transform.localScale; // Guarda la escala original al iniciar
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Escala la imagen cuando el cursor del mouse entra
        transform.localScale = originalScale * hoverScale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Restaura la escala original cuando el cursor del mouse sale
        transform.localScale = originalScale;
    }
}
