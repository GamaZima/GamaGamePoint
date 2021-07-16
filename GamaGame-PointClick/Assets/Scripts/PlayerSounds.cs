using System.Collections;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    public bool Debug_Enabled = false;
    public string LeftFootRun = "fs_char_leftfoot_run";
    public string RightFootRun = "fs_char_rightfoot_run";
    public string LeftFootWalk = "fs_char_leftfoot_walk";
    public string RightFootWalk = "fs_char_rightfoot_walk";

    //Animator m_animator;

    void Start()
    {
        // m_animator = GetComponent<Animator>();
        AkSoundEngine.RegisterGameObj(gameObject);
    }

    void Update()
    {
        
    }

    void fs_char_leftfoot_run()
    {
        if (Debug_Enabled) { Debug.Log("Left Foot Triggered"); }
        AkSoundEngine.PostEvent(LeftFootRun, gameObject);
    }

    void fs_char_rightfoot_run()
    {
        if (Debug_Enabled) { Debug.Log("Right Foot Triggered"); }
        AkSoundEngine.PostEvent(RightFootRun, gameObject);
    }

    void fs_char_walk_left()
    {
        if (Debug_Enabled) { Debug.Log("Left Foot Triggered"); }
        AkSoundEngine.PostEvent(LeftFootWalk, gameObject);
    }

    void fs_char_walk_right()
    {
        if (Debug_Enabled) { Debug.Log("Right Foot Triggered"); }
        AkSoundEngine.PostEvent(RightFootWalk, gameObject);
    }

    public void AttackPlay()
    {
        AkSoundEngine.PostEvent("AttackPlay", gameObject);
    }
}
