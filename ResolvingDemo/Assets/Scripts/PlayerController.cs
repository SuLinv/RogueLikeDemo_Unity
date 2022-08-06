using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    //获得Player的CharacterController组件
    private CharacterController cc;
    [Header("移动参数")]
    //定义player的移动速度
    public float moveSpeed;
    public float turnspeed;
    [Header("跳跃参数")]
    //定义player的跳跃速度
    public float jumpSpeed;

    //定义获得按键值的两个变量
    private float horizontalMove, verticalMove;
    private float hor, ver;

    //定义三位变量控制方向
    private Vector3 dir;
    //定义重力变量
    public float gravity;
    //定义y轴的加速度
    private Vector3 velocity;
    //检测点的半径
    public float checkRadius;
    //定义需要检测的图层
    public LayerMask groundLayer;
    public Transform playerRig;
    public Transform cam;
    Vector3 mDir;
    void Start()
    {
        //用GetComponent<>()方法获得CharacterController
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {

        if(velocity.y < 0)
        {
            velocity.y = -1f;
        }

        //用Input.GetAxis()方法获取按键左右移动的值
        hor = Input.GetAxis("Horizontal");
        horizontalMove = hor * moveSpeed;
        //用Input.GetAxis()方法获取按键前后移动的值
        ver = Input.GetAxis("Vertical");
        verticalMove = ver * moveSpeed;

        Vector3 screenRight = cam.right;             //以屏幕为参考系移动
        Vector3 screenForward = cam.forward;
        screenForward.y = 0;                            //不能有竖直分量
 
        Vector3 sumVector = screenForward * ver + screenRight * hor;                //矢量之和
 
        if (!(hor==0&&ver==0))
        {
            Quaternion newRotation = Quaternion.LookRotation(sumVector);
            transform.rotation = Quaternion.Slerp(transform.rotation,newRotation,Time.deltaTime*5.0f);
        }
        transform.Translate(sumVector * moveSpeed * Time.deltaTime, Space.World);
    }
}
