using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WwAmbientArea_Script : MonoBehaviour
{
    // All our physical needs
    public bool Enable_Debug = false;
    public GameObject AudioEmitter;
    public GameObject Character;
    public AkAudioListener Listener;
    private BoxCollider AmbientCollider;
    private Vector3 ListenerPosition;

    // All our calculation needs
    private Vector3 ColliderSizeMax;
    private Vector3 ColliderSizeMin;
    private float emitterX;
    private float emitterY;
    private float emitterZ;

    public bool IsnInArea = false;

    // Start is called before the first frame update
    void Start()
    {
        // Define that the collider we want to use is the one on this game object
        AmbientCollider = GetComponent<BoxCollider>();

        // Define the colliders size, scale and set it's border locations
        ColliderSizeMax = AmbientCollider.transform.position;
        ColliderSizeMin = AmbientCollider.transform.position;
        ColliderSizeMax += (transform.localScale / 2);
        ColliderSizeMin -= (transform.localScale / 2);

    }

    // Update is called once per frame
    void Update()
    {
        ListenerPosition = Character.transform.position;

        if(ListenerPosition.x > ColliderSizeMin.x && ListenerPosition.x < ColliderSizeMax.x) { emitterX = ListenerPosition.x; }
        if (ListenerPosition.x < ColliderSizeMin.x) { emitterX = ColliderSizeMin.x; }
        if(ListenerPosition.x > ColliderSizeMax.x) { emitterX = ColliderSizeMax.x; }

        if (ListenerPosition.y > ColliderSizeMin.y && ListenerPosition.y < ColliderSizeMax.y) { emitterY = ListenerPosition.y; }
        if (ListenerPosition.y < ColliderSizeMin.y) { emitterX = ColliderSizeMin.y; }
        if (ListenerPosition.y > ColliderSizeMax.y) { emitterX = ColliderSizeMax.y; }

        if (ListenerPosition.z > ColliderSizeMin.z && ListenerPosition.z < ColliderSizeMax.z) { emitterX = ListenerPosition.z; }
        if (ListenerPosition.z < ColliderSizeMin.z) { emitterX = ColliderSizeMin.z; }
        if (ListenerPosition.z > ColliderSizeMax.z) { emitterX = ColliderSizeMax.z; }

        if (!IsnInArea) { AudioEmitter.transform.position = new Vector3(emitterX, emitterY, emitterZ); }

    }

    private void OnTriggerStay(Collider other)
    {
        // If our player enters the collider and stays - check if is IsInArea is false and set it to true
        // Set our emitters position to be the same as our listener position
       if (other.tag != "Player") { return; }
       if (!IsnInArea) { IsnInArea = true; }
       AudioEmitter.transform.position = ListenerPosition;

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Player") { return; }
        IsnInArea = false;
    }


}
