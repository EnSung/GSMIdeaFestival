using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float h, z;
    public float speed;
    bool isWalk;
    Vector2 moveDir;
    Vector2 dirVec;

    Collider2D scanObj;
    Animator anim;
    SpriteRenderer sp;
    public Item ownItem;

    public float radius;

    public LayerMask scanningMask;
    void Start()
    {
        anim = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        playerInput();
        Move();
        AnimationControl();
    }

    private void FixedUpdate()
    {
        #region ½ºÄ³´× ·¹ÀÌ
        Debug.DrawRay(transform.position, dirVec, new Color(0, 1, 0));
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dirVec, 0.7f, scanningMask);

        if(hit.collider != null)
        {
            scanObj = hit.collider;
        }
        else
        {
            scanObj = null;
        }

        #endregion

    }



    void playerInput()
   {
        h = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");
        Vector2 dir = new Vector2(h, z).normalized;

        if (dir.magnitude == 0)
        {

        }

        else if (h != 0)
        {
            dirVec = h == 1 ? Vector2.right : Vector2.left;
        }
        else if (h == 0)
        {
            dirVec = z == 1 ? Vector2.up : Vector2.down;
        }


        isWalk = !(h == 0 && z == 0);
        
        
        if (Input.GetKeyDown(KeyCode.J))
        {
            Scanning();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            Drop();
        }
   }

   void Move()
   {

       moveDir = Vector2.zero;
       moveDir += new Vector2(h, z);     

       transform.Translate(moveDir * speed * Time.deltaTime);
   }


    void Scanning()
    {
        if (scanObj != null)
        {
            if (scanObj.GetComponent<ScanningObject>() != null)
            {                
                scanObj.GetComponent<ScanningObject>().Scan(this);
            }
        }
    }

    void Drop()
    {
        if (ownItem != null)
        {
            Collider2D hits = Physics2D.OverlapCircle((Vector2)transform.position + dirVec + dirVec * 0.5f, radius);

            if (hits == null)
            {
                ownItem.gameObject.transform.parent = null;
                ownItem.transform.position = (Vector2)transform.position + dirVec + dirVec * 0.5f;
                ownItem.gameObject.SetActive(true);
                ownItem = null;
            }
        }
    }
    void AnimationControl()
    {
        anim.SetFloat("h", h != 0 ? 1 : -1);
        anim.SetFloat("v", z);


        anim.SetLayerWeight(1, isWalk ? 1:0);
        if(h == 1)
        {
            if (sp.flipX == false)
            {
                sp.flipX = true;
            }
        }
        else if(h == -1)
        {
            if(sp.flipX == true)
            {
                sp.flipX = false;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + dirVec + dirVec * 0.5f, radius);
    }
}
