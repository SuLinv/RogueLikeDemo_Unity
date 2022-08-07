using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("移动参数")]
    //定义player的移动速度
    public float moveSpeed;
    public float turnspeed;

    //定义获得按键值的两个变量
    private float horizontalMove, verticalMove;
    private float hor, ver;
    public Transform cam;
    public Transform enemy;

    GameObject go;
    MeshFilter mf;
    MeshRenderer mr;
    Shader shader;
    void Start()
    {

    }

    void Update()
    {
        playerMove();
        if(Input.GetMouseButtonDown(0)){
            ToDrawSectorSolid(transform, transform.localPosition, 180, 4);
            if(umbrellaAttact(transform,enemy.transform,180,4)){
                Debug.Log("打到了");
            }else{
                Debug.Log("没打到");
            }
        }
    }

    private void playerMove(){
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
            transform.rotation = Quaternion.Slerp(transform.rotation,newRotation,Time.deltaTime*turnspeed);
        }
        transform.Translate(sumVector * moveSpeed * Time.deltaTime, Space.World);
    }

    public bool umbrellaAttact(Transform player,Transform target,float angle,float radius)
    {

        Vector3 deltaA = target.position - player.position;
 
        //Mathf.Rad2Deg : 弧度值到度转换常度
        //Mathf.Acos(f) : 返回参数f的反余弦值
        float tmpAngle = Mathf.Acos(Vector3.Dot(deltaA.normalized, player.forward)) * Mathf.Rad2Deg;
        if (tmpAngle <= angle * 0.5f && deltaA.magnitude < radius)
        {
            return true;
        }
        return false;
    }

    public void ToDrawSectorSolid(Transform t, Vector3 center, float angle, float radius)
    {
        int pointAmmount = 100;
        float eachAngle = angle / pointAmmount;
 
        Vector3 forward = t.forward;
        List<Vector3> vertices = new List<Vector3>();
 
        vertices.Add(center);
        for (int i = 0; i < pointAmmount; i++)
        {
            Vector3 pos = Quaternion.Euler(0f, -angle / 2 + eachAngle * (i - 1), 0f) * forward * radius + center;
            vertices.Add(pos);
        }
        CreateMesh(vertices);
    }
 
    private GameObject CreateMesh(List<Vector3> vertices)
    {
        int[] triangles;
        Mesh mesh = new Mesh();
 
        int triangleAmount = vertices.Count - 2;
        triangles = new int[3 * triangleAmount];
 
        //根据三角形的个数，来计算绘制三角形的顶点顺序
        for (int i = 0; i < triangleAmount; i++)
        {
            triangles[3 * i] = 0;
            triangles[3 * i + 1] = i + 1;
            triangles[3 * i + 2] = i + 2;
        }
 
        if (go == null)
        {
            go = new GameObject("mesh");
            go.transform.position = new Vector3(0f, 0.1f, 0.5f);
 
            mf = go.AddComponent<MeshFilter>();
            mr = go.AddComponent<MeshRenderer>();
 
            shader = Shader.Find("Unlit/Color");
        }
 
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles;
 
        mf.mesh = mesh;
        mr.material.shader = shader;
        mr.material.color = Color.red;
 
        return go;
    }
}
