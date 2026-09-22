using UnityEngine;

public class Player_Movement : MonoBehaviour {
    [SerializeField] Grid MAIN_GRID;
    public Player_Input pI {
        get; private set;
    }

    void Awake() {
        pI = new Player_Input();
    }
    void OnEnable() {
        pI.Player.Enable();
    }
    void OnDisable() {
        pI.Player.Disable();
    }

    void Update() {
        if (pI.Player.Move.triggered) {
            Vector2 moveDir = pI.Player.Move.ReadValue<Vector2>();
            if (!IsBlocked(moveDir)) {

                RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, moveDir, 1);

                if (hit.collider != null && hit.collider.GetComponent<IPushable>() != null && !IsObjectBlocked(hit.collider.gameObject, moveDir)) {
                    ChangeGridPosition(moveDir, hit.collider.gameObject);
                }
                else if (hit.collider == null || hit.collider.GetComponent<IPushable>() == null) // Tranh viec raycast hit voi vien dan
                {
                    ChangeGridPosition(moveDir);
                }
            }
        }
    }

    void ChangeGridPosition(Vector2 direction, GameObject push_object = null) {
        Vector3Int playerPos = GetCellPosition(gameObject);
        gameObject.transform.position = MAIN_GRID.GetCellCenterWorld(playerPos + new Vector3Int((int)direction.x, (int)direction.y, 0));

        if (push_object != null) {
            Vector3Int objPos = GetCellPosition(push_object);
            push_object.transform.position = MAIN_GRID.GetCellCenterWorld(objPos + new Vector3Int((int)direction.x, (int)direction.y, 0));
        }

    }
    private Vector3Int GetCellPosition(GameObject gameobject) {
        return MAIN_GRID.WorldToCell(gameobject.transform.position);
    }
    bool IsBlocked(Vector2 direction) {
        RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, direction, 1);
        if (hit.collider != null && hit.collider.GetComponent<IPushable>() != null)
            return false;
        if (hit.collider != null && hit.collider.GetComponent<Bullet>())
            return false;
        return hit;
    }
    bool IsObjectBlocked(GameObject gameobject, Vector2 direction) {
        RaycastHit2D hit = Physics2D.Raycast(gameobject.transform.position, direction, 1, ~LayerMask.GetMask("Passable")); // Passable sẽ là layer chung cho những vật đẩy duoc;            
        return hit;
    }
}
