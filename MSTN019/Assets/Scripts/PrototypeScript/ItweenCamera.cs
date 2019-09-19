using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItweenCamera : MonoBehaviour
{
    public Transform Target;

    void Update()
    {
        //iTween.MoveUpdate(this.gameObject, iTween.Hash(
        //    "position", Target.position,
        //    "time", 3.0f)
        //);
        iTween.RotateUpdate(this.gameObject, iTween.Hash(
            "rotation", Target.rotation.eulerAngles,
            "time", 3.0f)
        );

        Target.position = new Vector3(Target.position.x, Target.position.y + 25.0f, Target.position.z - 20.0f);
        this.transform.LookAt(Target.position);
    }
}