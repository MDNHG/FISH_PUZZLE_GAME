using UnityEngine;

[RequireComponent(typeof(Player_Movement))]
public class Player_ : MonoBehaviour
{
    

    void Start()
    {
        Player_Movement.EnterWarpGate += Winning;
        Door.crushPlayer += BeingCrushedbyDoor;
        Laser.roastPlayer += BeingRoastedbyLaser;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Winning()
    {
    }
    void BeingCrushedbyDoor()
    {
        Debug.Log("Get crushed");
        Destroy(gameObject);
    }
    void BeingRoastedbyLaser()
    {
        Debug.Log("Get roast");
        Destroy(gameObject);
    }
}
