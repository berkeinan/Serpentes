using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject point;
    public float xsense, ysense;
    void Update()
    {
        Touch t1=new Touch();
        Touch t2=new Touch();
        int valid_touches = 0;
        for (int i = 0; i < Input.touchCount; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.touches[i].rawPosition), Vector2.zero);
            if (hit.collider != null)
            {
                if (valid_touches == 0) {
                    t1 = Input.touches[i];
                    valid_touches++;
                }
                else {
                    t2 = Input.touches[i];
                    break;
                }
            }
        }
        if (valid_touches != 0)
        {
            if (valid_touches != 2) t2 = t1;
            Vector2 v;
            v.x = (t1.deltaPosition.x + t2.deltaPosition.x) / 2;
            v.y = (t1.deltaPosition.y + t2.deltaPosition.y) / 2;
            point.transform.rotation = Quaternion.Euler(point.transform.rotation.eulerAngles.x+v.y*ysense,point.transform.rotation.eulerAngles.y+v.x*xsense,0);
        }
    }
}
