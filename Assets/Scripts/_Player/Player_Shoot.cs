using UnityEngine;

[RequireComponent(typeof(Player_Movement))]
public class Player_Shoot : MonoBehaviour {
    [SerializeField] Grid MAIN_GRID;
    [SerializeField] int MAX_AMMO;
    [SerializeField] GameObject bullet_prefab;
    private int ammo_left;
    private int bullet_speed = 1;
    Player_Input pI;
    Vector2 shoot_direction;

    void Awake() {

    }
    void Start() {
        pI = GetComponent<Player_Movement>().pI;

        ammo_left = MAX_AMMO;
        shoot_direction = new Vector2(1, 0); // Vừa spawn nếu bắn sẽ ra velocity = 0
        gameObject.transform.position = MAIN_GRID.GetCellCenterWorld(MAIN_GRID.WorldToCell(gameObject.transform.position));
    }
    void Update() {
        if (pI.Player.ChangeDirection.triggered) {
            ChangeShootPosition(pI.Player.ChangeDirection.ReadValue<Vector2>());
        }
        if (pI.Player.Shoot.triggered && ammo_left > 0) {
            Shoot(bullet_speed);
        }
        if (pI.Player.ChangeGunState.triggered) {
            bullet_speed = (bullet_speed + 1) % 3 + 1;
        }
    }
    private void Shoot(int speed) {
        GameObject bullet = Instantiate(bullet_prefab, gameObject.transform.position, Quaternion.identity);
        Rigidbody2D b_rb = bullet.GetComponent<Rigidbody2D>();
        b_rb.linearVelocity = speed * shoot_direction;
        ammo_left--;
    }
    void ChangeShootPosition(Vector2 direction) {
        shoot_direction = direction;
    }
}