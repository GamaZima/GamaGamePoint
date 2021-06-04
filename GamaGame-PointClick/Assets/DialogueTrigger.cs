using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Canvas myCanvas;
    private void OnTriggerEnter(Collider other)

    {

        if (other.tag == "Player")
        {
            myCanvas.enabled = true;
            //Cursor.lockState = CursorLockMode.None;
        }

        else
        {
            return;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            myCanvas.enabled = false;
            //Cursor.lockState = CursorLockMode.None;
            Destroy(gameObject);
        }

        else
        {
            return;
        }
    }
}
