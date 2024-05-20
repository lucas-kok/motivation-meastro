using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoverFunctionallity : MonoBehaviour
{

    public GameObject infoIcon;
    public Sprite stillInfoButton;
    
    // Start is called before the first frame update
    public void OnMouseEnter()
    {
        if (infoIcon != null)
        {
            infoIcon.GetComponent<Animator>().enabled = false;
            infoIcon.GetComponent<Animator>().speed = 0;
            infoIcon.GetComponent<Image>().sprite = stillInfoButton;
        }
    }

    public void OnMouseExit()
    {
        if (infoIcon != null)
        {
            infoIcon.gameObject.GetComponent<Animator>().enabled = true;
            infoIcon.gameObject.GetComponent<Animator>().speed = 1;
            //infoIcon.gameObject.GetComponent<Image>().sprite = null;
        }
    }
}
