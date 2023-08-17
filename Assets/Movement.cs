using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

[RequireComponent(typeof(BoxCollider2D))]
public class Movement : MonoBehaviour
{
   

    PhotonView pw;
    private BoxCollider2D bxc;
    private RaycastHit2D hit;
    // Start is called before the first frame update
    void Start()
    {

        pw = GetComponent<PhotonView>();
        bxc = GetComponent<BoxCollider2D>();

       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (pw.IsMine)
        {
            if (Input.GetAxis("Horizontal")!=0 || Input.GetAxis("Vertical")!=0)
            {
                haraket();
            }
          
        }

  

    }

    void haraket()
    {
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * 300;
        float y = Input.GetAxis("Vertical") * Time.deltaTime * 300;
        Vector3 MoveDelta = new Vector3(x, y, 0);

        hit = Physics2D.BoxCast(transform.position, bxc.size, 0, new Vector2(0, MoveDelta.y), Mathf.Abs(MoveDelta.y * Time.deltaTime), LayerMask.GetMask("block","actor"));
        if (hit.collider==null)
        {
         
            transform.Translate(0,MoveDelta.y*Time.deltaTime,0);
        }

        hit = Physics2D.BoxCast(transform.position, bxc.size, 0, new Vector2(MoveDelta.x,0), Mathf.Abs(MoveDelta.x * Time.deltaTime), LayerMask.GetMask("block","actor"));
        if (hit.collider == null)
        {
            transform.Translate( MoveDelta.x * Time.deltaTime,0, 0);
        }

    }
}
