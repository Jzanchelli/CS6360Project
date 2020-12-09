using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonTransitioner : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    private Image m_image = null;
    [SerializeField]private Sprite m_up_button = null;

    [SerializeField]private Sprite m_down_button = null;

    [SerializeField]private Sprite m_hover_button = null;

    private void Awake()
    {
        m_image = GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //m_Image.color = m_HoverColor;
        m_image.sprite = m_hover_button;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //m_Image.color = m_NormalColor;
        m_image.sprite = m_up_button;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
       // m_Image.color = m_DownColor;
       m_image.sprite = m_down_button;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //Do Nothing
    }

    public void OnPointerClick(PointerEventData eventData)
    {
       // m_Image.color = m_HoverColor;
       m_image.sprite = m_hover_button;
    }
}
