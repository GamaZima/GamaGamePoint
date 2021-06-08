using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOverHighlight : MonoBehaviour
{
    Color startColor;
    Renderer rendererColor;

    void OnMouseEnter()
    {
        Debug.Log("Mouse over");
        gameObject.GetComponent<Color>();


        rendererColor.material.color = Color.green;
    }

    void OnMouseExit()
    {
        rendererColor.material.color = startColor;
    }
}
