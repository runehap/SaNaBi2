using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grapling : MonoBehaviour
{
    public LineRenderer line;
    public LineRenderer line2;
    public Transform hook;
    public jump playermove;
    Vector2 mousedir;
    [SerializeField]
    public float hookspeed = 30f;
    public bool CanUseGrap = true;
    
    public bool isHookActive;
    public bool isLineMax = false;
    public bool isAttach;
    public float MaxLength = 8f;

    void Start()
    {
        playermove = GetComponent<jump>();
        line.positionCount = 2;
        line.endWidth = line.startWidth = 0.05f;
        line.SetPosition(0, transform.position);
        line.SetPosition(1, hook.position);
        line.useWorldSpace = true;
        line.enabled = false;
        isAttach = false;
    }

    
    void Update()
    {
        line.SetPosition(0, transform.position);
        line.SetPosition(1, hook.position);

        if (Input.GetMouseButtonDown(0) && !isHookActive && CanUseGrap)
        {
            line2.enabled = false;
            hook.gameObject.SetActive(true);
            hook.position = transform.position;
            mousedir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            playermove.isUseBoost = false;
            isHookActive = true;
            isLineMax = false;
            line.enabled = true;
        }

        if (isHookActive && !isLineMax && !isAttach)
        {
                
                hook.Translate(mousedir.normalized * Time.deltaTime * hookspeed);
                

            if (Vector2.Distance(transform.position, hook.position) > MaxLength) // 플레이어와 후크거리
            {
                isLineMax = true;
            }

        }
        else if(isHookActive && isLineMax && !isAttach)
        {
            hook.position = Vector2.MoveTowards(hook.position, transform.position, Time.deltaTime * hookspeed);
            if (Vector2.Distance(transform.position, hook.position) < 0.1f) // 플레이어와 후크거리
            {
                line2.enabled = true;
                //CanUseGrap = false;
                isHookActive = false;
                isLineMax = false;
                line.enabled = false;
                hook.gameObject.SetActive(false);
            }
        }
        else if (isAttach)
        {
            if (Input.GetMouseButtonDown(0))
            {
                line2.enabled = true;
                isAttach = false;
                isHookActive = false;
                isLineMax = false;
                hook.GetComponent<Hookg>().joint2D.enabled = false;
                hook.gameObject.SetActive(false);
                hook.position = transform.position;
            }
        }
        else
        {
            hook.position = transform.position;
        }
    }

}
