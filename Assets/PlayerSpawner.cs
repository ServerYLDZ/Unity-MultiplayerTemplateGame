using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefabs = null;
    [SerializeField] private Vector3[] SpawnPoints;
  
    void Start()
    {
       // GameObject obj = PhotonNetwork.Instantiate(playerPrefabs.name, Vector3.zero, Quaternion.identity, 0); // silinceği silip aktif edicepim
        


        if (GameManager.Instance.index % 2 == 0)
        {
            GameObject obj = PhotonNetwork.Instantiate(playerPrefabs.name, SpawnPoints[0], Quaternion.identity, 0); // silinceği silip aktif edicepim
             obj.GetComponent<Gammer>().my_rol = Gammer.rol.killer;
           
        }

        else
        {
            GameObject obj = PhotonNetwork.Instantiate(playerPrefabs.name, SpawnPoints[1], Quaternion.identity, 0); // silinceği silip aktif edicepim
            obj.GetComponent<Gammer>().my_rol = Gammer.rol.victom;
            
        }
        GameManager.Instance.index++;
    }

  
}
