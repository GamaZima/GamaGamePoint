using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInteract : Interactables
{
    private Transform target;

    void Start()
    {
        target = PlayerManager.instance.player.transform;
    }

    public override void Interact()
    {
        base.Interact();
        FaceTarget();
        transform.GetChild(1).gameObject.SetActive(true);
    }

    void FaceTarget()
    {
        Debug.Log("Facing target");
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}
