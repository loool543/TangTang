using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Target;

    void Start()
    {
        
    }

    void LateUpdate()
    {
        if (Target == null)
            return;

        //TODO
        transform.position = new Vector3(Target.transform.position.x, Target.transform.position.y, -10);

    }
}
