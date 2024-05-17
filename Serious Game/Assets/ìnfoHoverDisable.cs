using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ìnfoHoverDisable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject stillInfoIcon;
    public GameObject infoIcon;

    public void OnPointerEnter(PointerEventData eventData)
    {
        infoIcon.SetActive(false);
        stillInfoIcon.SetActive(true);

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        stillInfoIcon.SetActive(false);
        infoIcon.SetActive(true);
    }
}
