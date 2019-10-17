using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseupShot : MonoBehaviour
{

    public GameObject[] obj;//观察对象集合
    private int num = 0;

    //  public Transform target;
    public float distance = 3.0f;//摄像机正对物体的距离
    public float height = 1.0f;//摄像机正对物体的高度
    public float damping = 5.0f;//摄像机位移速度
    private bool smppthRotation = true;//是否平滑转动角度
    public float rotationDamping = 10.0f;//摄像机角度转动的速度
    public float x_ = 0f;//摄像机距离物体x轴的距离
    private Vector3 targetLookAtOffset;//

    public float bumperDistanceCheck = 2.5f;
    public float bumperCameraHeight = 1.0f;
    private Vector3 bumperRayOffset;
    private bool isFar;


    // Update is called once per frame
    void Update()
    {

        lookatobj(obj[num].transform);
        Zoom();
    }

    void lookatobj(Transform target)
    {
        Vector3 wantedPosition = target.TransformPoint(x_, height, -distance);


        RaycastHit hit;
        //若摄像机和物体之间有障碍物 则将摄像机拉到障碍物之前

        //Vector3 back = target.transform.TransformDirection(-1 * Vector3.forward);
        //if (Physics.Raycast(target.TransformPoint(bumperRayOffset), back, out hit, bumperDistanceCheck))
        //{

        //    // clamp wanted position to hit position  
        //    wantedPosition.x = hit.point.x;
        //    wantedPosition.z = hit.point.z;
        //  //  wantedPosition.y = Mathf.Lerp(wantedPosition.y, hit.point.y + bumperCameraHeight,  Time.deltaTime * damping);
        //}

        transform.position = Vector3.Lerp(transform.position, wantedPosition, Time.deltaTime * damping);//摄像机位移
        Vector3 lookPosition = target.position;//target.TransformPoint(targetLookAtOffset);

        Quaternion wantedRotation = Quaternion.LookRotation(lookPosition - transform.position, target.up);
        if (smppthRotation)
        {
            //平滑转动摄像机
            transform.rotation = Quaternion.Slerp(transform.rotation, wantedRotation, Time.deltaTime * rotationDamping);
        }
        else transform.rotation = wantedRotation;

        //将障碍物隐藏
        if (Vector3.Distance(transform.position, wantedPosition) <= 2f)
        {
            Vector3 dir = transform.TransformDirection(Vector3.forward);
            if (Physics.Raycast(transform.position, dir, out hit))
            {

                if (hit.collider.gameObject.name != target.name)
                {
                    hit.collider.gameObject.SetActive(false);

                }

            }
        }

        if (transform.position == wantedPosition && transform.rotation == wantedRotation)//循环观察
        {
            num++;
            num %= obj.Length;
            obj[num].SetActive(true);//显示对象
        }
  
    }


    private void Zoom()
    {
            //修改缩放等级
        if (isFar)
        {
            //拉远 20  --》 60        Lerp(起点、终点、比例)
            gameObject.GetComponent<Camera>().fieldOfView = Mathf.Lerp(gameObject.GetComponent<Camera>().fieldOfView, 60, 0.1f);
            //Vector3.Lerp
            //Quaternion.Lerp
            //Color.Lerp
        }
        else
        {
            //拉近 60 --》 20
            gameObject.GetComponent<Camera>().fieldOfView = Mathf.Lerp(gameObject.GetComponent<Camera>().fieldOfView, 20, 0.1f);
        }
       // isFar = !isFar;

    }

}
