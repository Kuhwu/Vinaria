using System.Numerics;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform player;
    public UnityEngine.Vector3 offset;


    void LateUpdate(){
        if(player != null){
            transform.position = player.position;
        }
    }

}
