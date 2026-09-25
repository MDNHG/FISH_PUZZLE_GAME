using UnityEngine;

[RequireComponent(typeof(Player_Movement))]
public class Player_ : MonoBehaviour
{
    

    void Start()
    {
        Player_Movement.EnterWarpGate += Winning;
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
        
    }
    void BeingRoastedbyLaser()
    {
        
    }
}
