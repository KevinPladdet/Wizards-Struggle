using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLockX : MonoBehaviour
{

    void LateUpdate()
    {
        transform.position = new Vector3(-20, transform.position.y, transform.position.z);
    }

}
