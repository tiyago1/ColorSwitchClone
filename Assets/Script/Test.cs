using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Transform Target1;
    public Transform Target2;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Angle : " + Vector3.Angle(Target1.position, Target2.position));
            Debug.Log("Distance : " + Vector3.Distance(Target1.position, Target2.position));
            Debug.Log("Dot : " + Vector3.Dot(Target1.position, Target2.position));
        }
    }
}
