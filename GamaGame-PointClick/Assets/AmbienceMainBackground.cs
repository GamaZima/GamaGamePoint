using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceMainBackground : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AkSoundEngine.PostEvent("ExteriorBackground", gameObject);
    }

}
