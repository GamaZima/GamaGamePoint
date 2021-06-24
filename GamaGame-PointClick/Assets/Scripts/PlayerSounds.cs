using System.Collections;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    Animator m_animator;

    // Start is called before the first frame update
    void Start()
    {
        m_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FootstepPlay()
    {
        AkSoundEngine.PostEvent("FootstepPlay", gameObject);
    }

    public void AttackPlay()
    {
        AkSoundEngine.PostEvent("AttackPlay", gameObject);
    }
}
