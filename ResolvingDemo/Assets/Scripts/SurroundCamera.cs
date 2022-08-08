using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class SurroundCamera : MonoBehaviour
{
    //视野中心
    public Transform focus;
 
    //相机相对角色的位置
    Vector3 RelativePosition;
 
    void Start()
    {
        RelativePosition = transform.position - focus.position;     //以人为原点A，相机为B，向量AB=B-A，B的坐标等于向量AB。
    }
 
    void Update()
    {
        Follow();               //相机跟随          
        DragToRotateView_Velocity();//调整视角
        ScrollToScaleDistance(); //鼠标滚轮调整视野
    }
 
    /*-----------------相机跟随------------------*/
 
    void Follow()
    {
        transform.position = focus.position + RelativePosition;             //每一帧都跟随移动
    }
 
    /*-----------------调整视角------------------*/
 
    //最小水平夹角
    public float MinimumDegree = 0;
    //最大水平夹角
    public float MaximumDegree = 60;
    //两点连线与水平方向的夹角
    float currentAngleY;
 
    float mouseVelocityX;
    float mouseVelocityY;
    Vector3? point1;
    //旋转每度，在一帧中需要的速度
    public int DragVelocityPerAngle = 370;
 
    void DragToRotateView_Velocity()
    {
        if(Input.GetMouseButton(1)){
            var point2 = Input.mousePosition;
            if (point1 != null)
            {
                mouseVelocityX = -(point1.Value.x - point2.x) / Time.deltaTime;
                mouseVelocityY = -(point1.Value.y - point2.y) / Time.deltaTime;
            }

            point1 = point2;

            float anglex = mouseVelocityX / DragVelocityPerAngle;                   //将鼠标在屏幕上拖拽的速度转化为角度
            float angley = mouseVelocityY / DragVelocityPerAngle;

            currentAngleY = 90 - Vector3.Angle(-RelativePosition, Vector3.down);            //计算两点连线与水平方向的夹角

            if (currentAngleY - angley > MaximumDegree || currentAngleY - angley < MinimumDegree)
                angley = 0;

            transform.RotateAround(focus.position, Vector3.up, anglex);
            transform.RotateAround(focus.position, -transform.right, angley);

            transform.LookAt(focus);                    //如果没有这一句，摄像头转着转着就会歪

            RelativePosition = transform.position - focus.position;                 //更新相对位置
        }
    }

    public float mouseWheelSensitivity = 30;
    public float MinViewDistance = 2;
    public float MaxViewDistance = 6;
 
    private void ScrollToScaleDistance()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (RelativePosition.magnitude <= MinViewDistance) return;
 
            transform.Translate(-RelativePosition / mouseWheelSensitivity, Space.World);
            RelativePosition = transform.position - focus.position;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (RelativePosition.magnitude >= MaxViewDistance) return;
 
            transform.Translate(RelativePosition / mouseWheelSensitivity, Space.World);
            RelativePosition = transform.position - focus.position;
        }
    }
} 