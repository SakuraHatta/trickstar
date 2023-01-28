using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField]
    private Transform Target;
    private Vector3 offset = new Vector3(0.0f, 0.0f, -10.0f);

    void Update()
    {
        this.transform.position = Target.position + offset;        
    }
}
