using UnityEngine;

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
    void OnEnable()
    {
        playerInput.Player.Enable();
    }
    void OnDisable()
    {
        playerInput.Player.Disable();
    }
    void Start()
    {
        ammo_left = MAX_AMMO;
        shoot_direction = new Vector2(1,0); // Vừa spawn nếu bắn sẽ ra velocity = 0 
        gameObject.transform.position = MAIN_GRID.GetCellCenterWorld(GetCellPosition(gameObject));
    }
    void Update()
    {
        if(playerInput.Player.Move.triggered && !IsBlocked(playerInput.Player.Move.ReadValue<Vector2>()))
        {
           RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, playerInput.Player.Move.ReadValue<Vector2>(),1);

            if (hit.collider != null && hit.collider.GetComponent<IPushable>() != null && !IsObjectBlocked(hit.collider.gameObject,  playerInput.Player.Move.ReadValue<Vector2>()))
            {
                ChangeGridPosition(playerInput.Player.Move.ReadValue<Vector2>(), hit.collider.gameObject);
                ChangeShootPosition(playerInput.Player.Move.ReadValue<Vector2>());
            }
            else if (hit.collider == null || hit.collider.GetComponent<IPushable>() == null) // Tranh viec raycast hit voi vien dan
            {
                ChangeGridPosition(playerInput.Player.Move.ReadValue<Vector2>());
                ChangeShootPosition(playerInput.Player.Move.ReadValue<Vector2>());
            }
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
        Vector3Int playerPos = GetCellPosition(gameObject);
        
        gameObject.transform.position = MAIN_GRID.GetCellCenterWorld(playerPos +  new Vector3Int((int)direction.x, (int)direction.y, 0));
    }
    void ChangeGridPosition(Vector2 direction, GameObject push_object)
    {
        Vector3Int playerPos = GetCellPosition(gameObject);
        Vector3Int objPos = GetCellPosition(push_object);
        gameObject.transform.position = MAIN_GRID.GetCellCenterWorld(playerPos +  new Vector3Int((int)direction.x, (int)direction.y, 0));
        push_object.transform.position = MAIN_GRID.GetCellCenterWorld(objPos +  new Vector3Int((int)direction.x, (int)direction.y, 0));
        
    }
    void ChangeShootPosition(Vector2 direction)
    {
        shoot_direction = direction;
    }
    private Vector3Int GetCellPosition(GameObject gameobject)
    {
        return MAIN_GRID.WorldToCell(gameobject.transform.position);
    }
    bool IsBlocked(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position,direction,1);
        if (hit.collider != null && hit.collider.GetComponent<IPushable>() != null)
        return false;  
        if (hit.collider != null && hit.collider.GetComponent<Bullet>())
        return false;     
        return hit;
    }
    bool IsObjectBlocked(GameObject gameobject,Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(gameobject.transform.position,direction,1,~LayerMask.GetMask("Passable")); // Passable sẽ là layer chung cho những vật đẩy duoc;            
        return hit;
    }
}

