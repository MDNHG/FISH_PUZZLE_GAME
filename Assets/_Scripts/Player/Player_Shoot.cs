using System;
using System.ComponentModel;
using UnityEngine;

[RequireComponent(typeof(Player_Movement))]
public class Player_Shoot : MonoBehaviour {
    public event Action<int> changeGunState;
    public event Action<int> decreaseBullet;
    private Grid MAIN_GRID;
    [SerializeField] private int MAX_AMMO;
    [SerializeField] GameObject bullet_prefab;
    private int ammo_left;
    private int bullet_speed = 1;
    Player_Input pI;
    Vector2 shoot_direction;

    void Start() {
        pI = GetComponent<Player_Movement>().pI;
        MAIN_GRID = FindAnyObjectByType<Grid>();
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
            decreaseBullet?.Invoke(ammo_left);
        }
        if (pI.Player.ChangeGunState.triggered) {
            bullet_speed = (bullet_speed) % 3+1;
            Debug.Log(bullet_speed);
            changeGunState?.Invoke(bullet_speed);
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
    public void SetMaxAmmo(int m_ammo)
    {
        MAX_AMMO = m_ammo;
    }
    public int GetMaxAmmo()
    {
        return MAX_AMMO;
    }
}