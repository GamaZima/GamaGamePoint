using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ww_CharacterSpeed : MonoBehaviour
{

    NavMeshAgent Character;
    public float SpeedOfCharacter;
    public string RTPC_SpeedOfCharacter = "RTPC_SpeedOfCharacter";

    // Start is called before the first frame update
    void Start()
    {
        Character = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        SpeedOfCharacter = Character.velocity.magnitude;
        AkSoundEngine.SetRTPCValue(RTPC_SpeedOfCharacter, SpeedOfCharacter);

    }
}
