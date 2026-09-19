using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    [SerializeField] Grid MAIN_GRID;
    [SerializeField] int MAX_AMMO;
    [SerializeField] GameObject bullet_prefab;
    private int ammo_left, bullet_speed = 1;
    Player_Input playerInput;
    Vector2 shoot_direction;



    void Awake()
    {
        playerInput = new Player_Input();
        playerInput.Player.Enable();
    }
    void Start()
    {
        ammo_left = MAX_AMMO;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInput.Player.Move.triggered && !checkValid(playerInput.Player.Move.ReadValue<Vector2>()))
        {
            ChangeGridPosition(playerInput.Player.Move.ReadValue<Vector2>());
            ChangeShootPosition(playerInput.Player.Move.ReadValue<Vector2>());
        }
        if(playerInput.Player.ChangeDirection.triggered)
        {
            ChangeShootPosition(playerInput.Player.ChangeDirection.ReadValue<Vector2>());
        }
        if(playerInput.Player.Shoot.triggered && ammo_left > 0)
        {
            Shoot(bullet_speed);
        }
        if(playerInput.Player.ChangeGunState.triggered)
        {
            bullet_speed = (bullet_speed + 1)%3 + 1;
        }
    }
    private void Shoot(int speed)
    {
        GameObject bullet = Instantiate(bullet_prefab, gameObject.transform.position, Quaternion.identity);
            Rigidbody2D b_rb = bullet.GetComponent<Rigidbody2D>();
            b_rb.linearVelocity = speed*shoot_direction;
            ammo_left--;
    }
    void ChangeGridPosition(Vector2 direction)
    {
        Vector3Int playerPos = GetPlayerCellPosition();
        
        gameObject.transform.position = MAIN_GRID.GetCellCenterWorld((Vector3Int)playerPos +  new Vector3Int((int)direction.x, (int)direction.y, 0));
    }
    void ChangeShootPosition(Vector2 direction)
    {
        shoot_direction = direction;
    }
    private Vector3Int GetPlayerCellPosition ()
    {
        return MAIN_GRID.WorldToCell(gameObject.transform.position);
    }
    bool checkValid(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position,direction,1,1<<LayerMask.GetMask("Passable"));
        return hit;
    }
    
}
