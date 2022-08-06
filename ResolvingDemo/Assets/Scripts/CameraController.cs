using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //获得player的transform
    public Transform player;
    public Transform playerRig;
    //获取鼠标移动的值
    private float mouseX, mouseY;
    private Vector3 distance_player;
    //添加鼠标灵敏度
    public float mouseSensitivity;
    // Start is called before the first frame update
    void Start()
    {
        distance_player = -player.transform.position + transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //获得鼠标左右移动的值
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

        player.Rotate(Vector3.up * mouseX);
        // playerRig.Rotate(Vector3.back * mouseX);

        transform.rotation = Quaternion.Lerp(transform.rotation,player.transform.rotation, Time.deltaTime * 5.0f);

        transform.position = player.transform.position + transform.rotation*distance_player;

        // Quaternion mRotation = Quaternion.Euler(0,mouseX,0);
        // Vector3 mPosition = mRotation*new Vector3(0.0f,2.0f,distance_player.z)+player.position;

        // transform.rotation = Quaternion.Slerp(transform.rotation,mRotation,Time.deltaTime*5.0f);
        // transform.position = Vector3.Lerp(transform.position,mPosition,Time.deltaTime*5.0f);

    }
}
