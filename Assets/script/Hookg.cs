using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hookg : MonoBehaviour
{
    grapling grappling;
    public DistanceJoint2D joint2D;
    
    void Start()
    {
        grappling = GameObject.Find("Player").GetComponent<grapling>();
        joint2D = GetComponent<DistanceJoint2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("wall"))
        {
            joint2D.enabled = true;
            grappling.isAttach = true;
        }
    }
}
