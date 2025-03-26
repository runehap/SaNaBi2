using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aim : MonoBehaviour
{
    DistanceJoint2D joint;
    public LineRenderer line;
    Vector2 mousedir;
    Vector2 mousepos;
    Rigidbody2D rigid;
    Transform player;
    grapling grap;
    void Start()
    {
        joint = GetComponent<DistanceJoint2D>();
        rigid = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").GetComponent<Transform>();
        grap = GameObject.Find("Player").GetComponent<grapling>();
        line.positionCount = 2;
        line.endWidth = line.startWidth = 0.05f;
        line.SetPosition(0, player.position);
        line.SetPosition(1,transform.position);
        line.useWorldSpace = true;
    }

    
    void Update()
    {
        line.SetPosition(0, player.position);
        line.SetPosition(1, transform.position);

        mousedir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousepos;

        //RaycastHit2D[] hit;
        //hit = Physics2D.RaycastAll(player.position, mousedir, grap.MaxLength);

        //    if(hit.Length > 1)
        //    {
        //        Debug.Log(hit[1].collider.name);
        //        if (hit[1].transform.CompareTag("wall"))
        //        {
        //            grap.CanUseGrap = true;
        //        }
        //    }   
        //    else
        //    {
        //        grap.CanUseGrap = false;
        //    }
            



        //if (Physics.Raycast(player.position, mousedir, out hit, grap.MaxLength))
        //{
        //    if (hit.transform.CompareTag("wall"))
        //    {
        //        grap.CanUseGrap = true;
        //    }
        //    else
        //    {
        //        grap.CanUseGrap = false;
        //    }
        //}
        //else
        //{
        //    grap.CanUseGrap = false;
        //}
    }
}
