using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public Interactables focus;

    public LayerMask movementMask;

    Camera cam;
    PlayerMotor motor;


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        // If we press left mouse
        if (Input.GetMouseButtonDown(0))
        {
            // We create a ray
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // If the ray hits
            if(Physics.Raycast(ray, out hit, 100, movementMask))
            {
                motor.MoveToPoint(hit.point);

                RemoveFocus();

            }
        }

        // If we press right mouse
        if (Input.GetMouseButtonDown(1))
        {
            // We create a ray
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // If the ray hits
            if (Physics.Raycast(ray, out hit, 100))
            {
                Interactables interactables = hit.collider.GetComponent<Interactables>();
                if (interactables != null)
                {
                    SetFocus(interactables);
                }
            }
        }
    }

    void SetFocus (Interactables newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
            focus.OnDefocused();

            focus = newFocus;
            motor.FollowTarget(newFocus);
            
        }

        newFocus.OnFocused(transform);
    }


    void RemoveFocus()
    {
        if (focus != null)
           focus.OnDefocused();

        focus = null;
        motor.StopFollowingTarget();
    }
}
