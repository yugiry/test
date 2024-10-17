using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Camera_Operation : MonoBehaviour
{
    public GameObject CCollider;
    BoxCollider2D CameraCollider;
    private Camera mainCamera;
    Transform ct;
    private float CameraSize;

    private bool hit_overmap_UP;
    private bool hit_overmap_DOWN;
    private bool hit_overmap_RIGHT;
    private bool hit_overmap_LEFT;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        ct = this.gameObject.GetComponent<Transform>();
        CameraSize = mainCamera.orthographicSize;
        //CameraCollider = CCollider.GetComponent<BoxCollider2D>();
        Debug.Log(CCollider.GetComponent<BoxCollider2D>().size);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "overmapUP")
        {
            Debug.Log("UPhit");
            hit_overmap_UP = true;
        }
        else
        {
            hit_overmap_UP = false;
        }
        if (collision.gameObject.tag == "overmapDOWN")
        {
            Debug.Log("DOWNhit");
            hit_overmap_DOWN = true;
        }
        else
        {
            hit_overmap_DOWN= false;
        }
        if (collision.gameObject.tag == "overmapRIGHT")
        {
            Debug.Log("RIGHThit");
            hit_overmap_RIGHT = true;
        }
        else
        {
            hit_overmap_RIGHT= false;
        }
        if (collision.gameObject.tag == "overmapLEFT")
        {
            Debug.Log("LEFThit");
            hit_overmap_LEFT = true;
        }
        else
        {
            hit_overmap_LEFT = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        var scroll = Input.mouseScrollDelta.y * Time.deltaTime * 100;
        //Debug.Log(scroll);

        if (mainCamera.orthographicSize < 56.6)
        {
            if (mainCamera.orthographicSize > 0)
            {
                mainCamera.orthographicSize += scroll;
                CCollider.GetComponent<BoxCollider2D>().size += new Vector2(scroll * 2, scroll * 2);
            }
            else
            {
                mainCamera.orthographicSize -= scroll;
                CCollider.GetComponent<BoxCollider2D>().size -= new Vector2(scroll * 2, scroll * 2);
            }
        }
        else
        {
            mainCamera.orthographicSize -= scroll;
            CCollider.GetComponent<BoxCollider2D>().size -= new Vector2(scroll * 2, scroll * 2);
        }

        if (Input.GetKey(KeyCode.LeftArrow) && !hit_overmap_LEFT)
        {
            ct.position = ct.position + new Vector3(-0.1f, 0.0f, 0.0f);
        }
        else
        {
            ct.position = ct.position - new Vector3(-0.1f, 0.0f, 0.0f);
        }
        if (Input.GetKey(KeyCode.RightArrow) && !hit_overmap_RIGHT)
        {
            ct.position = ct.position + new Vector3(0.1f, 0.0f, 0.0f);
        }
        else
        {
            ct.position = ct.position - new Vector3(0.1f, 0.0f, 0.0f);
        }
        if (Input.GetKey(KeyCode.UpArrow) && !hit_overmap_UP)
        {
            ct.position = ct.position + new Vector3(0.0f, 0.1f, 0.0f);
        }
        else
        {
            ct.position = ct.position - new Vector3(0.0f, 0.1f, 0.0f);
        }
        if (Input.GetKey(KeyCode.DownArrow) && !hit_overmap_DOWN)
        {
            ct.position = ct.position + new Vector3(0.0f, -0.1f, 0.0f);
        }
        else
        {
            ct.position = ct.position - new Vector3(0.0f, -0.1f, 0.0f);
        }
    }
}
