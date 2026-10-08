using UnityEngine;

[RequireComponent(typeof(Player_Movement))]
public class Player_WinLoseHandler : MonoBehaviour
{
    private GameObject player;

    void Awake() {
        player = gameObject;
    }
    void Start()
    {
        Door.killPlayer += Die;
        Laser.killPlayer += Die;
    }

    public void Die() {
        Destroy(player);
        // Nếu muốn anim cho từng kiểu chết thì để 1 param ở hàm này, rồi so sánh if với 1 string anim prefab theo tên
    }
}
