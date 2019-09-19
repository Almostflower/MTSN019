using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCenterPoint : MonoBehaviour
{
    public List<Transform> transList = new List<Transform>();   //カメラ範囲内におさめたいオブジェクトのリスト
    private Transform cameraPos;
    public Camera usingCamera;
    private Vector3 pos = new Vector3();
    private Vector3 center = new Vector3();
    private float radius;
    private float margin = 1.0f;        //半径を少し余分にとるための値
    private float distance;
    private float cameraHeight = 1.5f;  //カメラが地面にめり込まないようにカメラを浮かせる高さ

    private float angle;
    private float timer = 0.0f;
    private float interval = 5.0f;

    void Start()
    {
        cameraPos = GameObject.Find("CameraPosition").GetComponent<Transform>();
    }

    void Update()
    {
        pos = new Vector3(0, 0, 0);
        radius = 0.0f;
        foreach (Transform trans in transList)
        {     //オブジェクトのポジションの平均値を算出
            pos += trans.position;
        }
        center = pos / transList.Count;
        this.transform.position = center;           //CenterPointのポジションを中心に配置
        foreach (Transform trans in transList)
        {     //中心から最も遠いオブジェクトとの距離を算出
            radius = Mathf.Max(radius, Vector3.Distance(center, trans.position));
        }
        distance = (radius + margin) / Mathf.Sin(usingCamera.fieldOfView * 0.5f * Mathf.Deg2Rad);   //カメラの距離を算出
        cameraPos.localPosition = new Vector3(0, Mathf.MoveTowards(cameraPos.transform.position.y, cameraHeight, 1.0f * Time.deltaTime), -distance);  //CameraPositionをカメラの距離をもとに配置
        cameraPos.LookAt(this.transform);           //CameraPositionを中心の方向に向かせる

        timer += Time.deltaTime;

        if (timer > interval)
        {
            angle += Random.Range(-30.0f, 30.0f);
            cameraHeight = Random.Range(1.5f, 2.5f);
            timer = 0.0f;
        }

        this.transform.eulerAngles = new Vector3(0.0f, Mathf.MoveTowardsAngle(this.transform.eulerAngles.y, angle, 10.0f * Time.deltaTime), 0.0f);
    }
}