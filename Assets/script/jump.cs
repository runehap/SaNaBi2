using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jump : MonoBehaviour
{
    public Rigidbody2D PlayerRigidbody;
    public float jumpforce = 10f;
    public float speed = 1000f;
    public bool isjump = false;
    public float GizRadius = 0.4f;
    public float boostpower = 100f;
    public float prevhor;
    private float hor;
    private float rawhor;
    public bool isUseBoost = false;
    public bool SetActiveBoost = false;


    [SerializeField]
    private LayerMask groundlayer;
    private bool isgrounded;
    private BoxCollider2D boxcollider2D;
    private Vector3 footPosition;
    grapling grap;


    void Awake()
    {
        Application.targetFrameRate = 60;
    }


    // Start is called before the first frame update
    void Start()
    {
        PlayerRigidbody = GetComponent<Rigidbody2D>();
        boxcollider2D = GetComponent < BoxCollider2D>();
        grap = GetComponent<grapling>();

    }

    // Update is called once per frame
    void Update()
    {
        hor = Input.GetAxis("Horizontal");
        rawhor = Input.GetAxisRaw("Horizontal");
        Jump();
        boost();

    }
    private void FixedUpdate()
    {
        walk();

        // 점프중인지 확인
        Bounds bounds = boxcollider2D.bounds;
       

        footPosition = new Vector2(bounds.center.x, bounds.min.y-0.15f);

        isgrounded = Physics2D.OverlapCircle(footPosition, GizRadius, groundlayer);
        
    }

    void walk()
    {
        //이동
        if (hor != 0)
        {
            if (grap.isAttach && !SetActiveBoost )
            {
                PlayerRigidbody.AddForce(new Vector2((hor) * speed, 0));
            }
            else if(!grap.isAttach && !SetActiveBoost)
            {
                PlayerRigidbody.velocity = new Vector2(rawhor * speed, PlayerRigidbody.velocity.y);
            }

        }
    
        if(rawhor != 0 )
        {
            prevhor = rawhor;
        }
    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space)){
            if (isgrounded)
            {
                PlayerRigidbody.AddForce(transform.up * jumpforce, ForceMode2D.Impulse);
            }
        }
    }
    void boost()
    {
        if(Input.GetMouseButtonDown(1))
        {
            if (grap.isAttach && !isUseBoost)
            {
                if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
                {
                        PlayerRigidbody.gravityScale = 2f;
                        PlayerRigidbody.velocity = Vector3.zero;
                        SetActiveBoost = true;
                        PlayerRigidbody.velocity = new Vector2(prevhor * boostpower, 0);
                        PlayerRigidbody.gravityScale = 5f;
                        isUseBoost = true;
                        SetActiveBoost = false;
                }

                //if(Input.GetKeyDown(KeyCode.A) && Input.GetKeyDown(KeyCode.D))
                //{
                //    PlayerRigidbody.gravityScale = 0f;
                //    PlayerRigidbody.velocity = Vector3.zero;
                //    SetActiveBoost = true;
                //    PlayerRigidbody.velocity = new Vector2(prevhor * boostpower, 0);
                //    isUseBoost = true;
                //}
                //else if (rawhor != 0)
                //{
                //    PlayerRigidbody.gravityScale = 0f;
                //    PlayerRigidbody.velocity = Vector3.zero;
                //    SetActiveBoost = true;
                //    PlayerRigidbody.velocity = new Vector2(prevhor * boostpower, 0);
                //    isUseBoost = true;
                //}

                //Invoke("OriginGravity", 0.1f);
            }
        }
       
    }
    void OriginGravity()
    {
        PlayerRigidbody.gravityScale = 5f;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(footPosition, GizRadius);
    }

}
