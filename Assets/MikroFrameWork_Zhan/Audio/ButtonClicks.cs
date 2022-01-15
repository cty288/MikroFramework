using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonClicks : MonoBehaviour , IPointerEnterHandler, IPointerClickHandler
{
    public AudioClip hoverSound;
    public AudioClip clickSound;
   
    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.Instance.PlaySFX(hoverSound);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        AudioManager.Instance.PlaySFX(clickSound);
    }
}

