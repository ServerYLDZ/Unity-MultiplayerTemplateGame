using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
public class Gammer : MonoBehaviour
{
    private PhotonView pw;
    [SerializeField] private Vector3[] SpawnPoints;
    [SerializeField] private Text NameText;

    public enum state
    {
        inside,
        outside,
        unvisible
    }
  public   enum rol
    {
        killer,
        victom
    }
    public bool visible;

    [SerializeField] public state my_state=state.outside;
    [SerializeField] public rol my_rol = rol.victom;

    private state otherstate;
    private void Awake()
    {
        pw = GetComponent<PhotonView>();
    }

    private void Start()
    {
   
      /*  if (GameManager.Instance.index% 2 == 0)
        {
           GetComponent<Gammer>().my_rol = Gammer.rol.killer;
          transform.position = SpawnPoints[0];
        }

        else
        {
          GetComponent<Gammer>().my_rol = Gammer.rol.victom;
          transform.position = SpawnPoints[1];
        }

        GameManager.Instance.index++;
      */
        if (pw.IsMine)
        {
            GetComponent<SpriteRenderer>().color = Color.blue;
            GetComponent<Gammer>().NameText.text = pw.Owner.NickName;
        }
        else
        {
            
            GetComponent<Gammer>().NameText.text = pw.Owner.NickName;
            if (GetComponent<Gammer>().my_rol == Gammer.rol.killer)
            {
                GetComponent<SpriteRenderer>().color = Color.red;
            }
            else
                GetComponent<SpriteRenderer>().color = Color.green;
        }
    }

    private void Update()
    {
        SetVisibility(visible);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
       

        if (pw.IsMine)
        {
            if (collision.tag == "inside")
            {
                my_state = state.inside;

            }
            if (collision.tag == "outside")
            {
                my_state = state.outside;

            }
        }
     
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
         
    }

    

    public void SetVisibility(bool visibilty)
    {

        gameObject.SetActive(visibilty);
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }

}
