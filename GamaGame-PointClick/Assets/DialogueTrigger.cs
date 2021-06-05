using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    Transform target;
    //Transform agent;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player entered dialogue trigger.");
            transform.GetChild(0).gameObject.SetActive(true);
            FaceTarget();
        }

        else
        {
            return;
        }

    }

    void FaceTarget()
    {
        target = PlayerManager.instance.player.transform;

        Debug.Log("Facing target");
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }

        else
        {
            return;
        }
    }
}
