using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField]
    public GameObject CameraPoint;
    [SerializeField]
    public GameObject CameraRotation;
    [SerializeField]
    public float CameraRotationX;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(CameraPoint.transform.position.x, this.transform.position.y, CameraPoint.transform.position.z);

        this.transform.rotation = CameraRotation.transform.rotation;

        this.transform.Rotate(new Vector3(1.0f, 0.0f, 0.0f), CameraRotationX);
    }
}
