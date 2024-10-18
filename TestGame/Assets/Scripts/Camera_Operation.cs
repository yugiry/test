using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Camera_Operation : MonoBehaviour
{
    public GameObject CCollider;
    public GameObject UI1;
    public GameObject UI2;
    public GameObject infantry;
    public GameObject archer;
    public GameObject catapult;

    BoxCollider2D CameraCollider;
    private Camera mainCamera;
    Transform ct;
    private float CameraSize;

    private bool ZoomIn;
    private bool ZoomOut;
    public float ZoomPct;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        ct = this.gameObject.GetComponent<Transform>();
        CameraSize = mainCamera.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        //カメラのズーム関係
        {
            if (Input.GetKeyDown(KeyCode.X) && !ZoomIn)
            {
                ZoomIn = true;
            }
            else
            {
                ZoomIn = false;
            }
            if (Input.GetKeyDown(KeyCode.Z) && !ZoomOut)
            {
                ZoomOut = true;
            }
            else
            {
                ZoomOut = false;
            }

            if (ZoomIn)
            {
                ZoomPct -= 0.2f;
                if (ZoomPct < 0.2f)
                {
                    ZoomPct = 0.2f;
                }
                UI1.transform.position = CCollider.transform.position + new Vector3(56.5f * ZoomPct + 25 * ZoomPct, 0.0f, 10.0f);
                UI2.transform.position = CCollider.transform.position - new Vector3(56.5f * ZoomPct + 25 * ZoomPct, 0.0f, -10.0f);
            }
            if (ZoomOut)
            {
                ZoomPct += 0.2f;
                if (ZoomPct > 1.0f)
                {
                    ZoomPct = 1.0f;
                }
                UI1.transform.position = CCollider.transform.position + new Vector3(56.5f * ZoomPct + 25 * ZoomPct, 0.0f, 10.0f);
                UI2.transform.position = CCollider.transform.position - new Vector3(56.5f * ZoomPct + 25 * ZoomPct, 0.0f, -10.0f);
            }

            mainCamera.orthographicSize = 56.5f * ZoomPct;
            UI1.transform.localScale = new Vector3(50 * ZoomPct, 113 * ZoomPct, 1.0f);
            UI2.transform.localScale = new Vector3(50 * ZoomPct, 113 * ZoomPct, 1.0f);
        }

        //カメラの移動関係
        {
            if (ZoomPct != 1.0f)
            {
                float x = 5 - ZoomPct * 5;
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    ct.position = ct.position + new Vector3(-0.1f, 0.0f, 0.0f);
                    Debug.Log("左");
                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    ct.position = ct.position + new Vector3(0.1f, 0.0f, 0.0f);
                    Debug.Log("右");
                }
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    ct.position = ct.position + new Vector3(0.0f, 0.1f, 0.0f);
                    Debug.Log("上");
                }
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    ct.position = ct.position + new Vector3(0.0f, -0.1f, 0.0f);
                    Debug.Log("下");
                }

                if (ct.position.x > 0.6f + 11 * x)
                {
                    ct.position = new Vector3(0.5f + 11 * x, ct.position.y, ct.position.z);
                }
                if (ct.position.x < -0.6f - 11 * x)
                {
                    ct.position = new Vector3(-0.5f - 11 * x, ct.position.y, ct.position.z);
                }
                if (ct.position.y > 0.6f + 11 * x)
                {
                    ct.position = new Vector3(ct.position.x, 0.5f + 11 * x, ct.position.z);
                }
                if (ct.position.y < -0.6f - 11 * x)
                {
                    ct.position = new Vector3(ct.position.x, -0.5f - 11 * x, ct.position.z);
                }
            }
            else if(ZoomPct == 1.0f)
            {
                ct.position -= new Vector3(ct.position.x, ct.position.y, 0.0f);
            }
        }
    }

    void Set_UI(GameObject obj, float zoompct)
    {
        float x = obj.transform.position.x;
        obj.transform.position = CCollider.transform.position + new Vector3(x * zoompct, obj.transform.position.y, obj.transform.position.z);
    }
}
