using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform Player;
    private Vector3 viewPortPoint;
    private Camera mCamera;

    private void Start()
    {
        mCamera = gameObject.GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        viewPortPoint = mCamera.WorldToScreenPoint(Player.position);

        if (viewPortPoint.y >= Screen.height / 2)
        {
            transform.position = Player.transform.position + new Vector3(0, 0, -10);
        }
    }
}
