using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResulationSetting : MonoBehaviour
{
#if UNITY_STANDALONE
    public int ResulationWidth = 500;
    public int ResulationHeight = 800;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void FixedUpdate ()
    {
        Screen.SetResolution(ResulationWidth, ResulationHeight, false);
    }
#endif

}
