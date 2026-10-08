using System;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class Player_Movement : MonoBehaviour {

    Grid MAIN_GRID;
    public static event Action Move; // Dung de dem tick khi nguoi choi di chuyen
    public static event Action EnterWarpGate; // Dùng để gọi những hàm xử lí winning.
    public Player_Input pI {
        get; private set;
    }

    // Cache the raycast length from the grid's actual cell size,
    // instead of using a fixed value of 1.
    float MoveDistance;
    [SerializeField] float moveSpeed;

    void Awake() {
        pI = new Player_Input();
    }
    void OnEnable() {
        pI.Player.Enable();
    }
    void OnDisable() {
        pI.Player.Disable();
    }
    void Start()
    {
        MAIN_GRID = FindAnyObjectByType<Grid>();
        MoveDistance = MAIN_GRID.cellSize.x;
        gameObject.transform.position =  MAIN_GRID.GetCellCenterWorld(GetCellPosition(gameObject));
    }

    void Update() {
        if (pI.Player.Move.triggered) {
            Vector2 moveDir = pI.Player.Move.ReadValue<Vector2>();
            if (!IsBlocked(moveDir)) {
                Physics2D.queriesStartInColliders = false;
                RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, moveDir, 1,~LayerMask.GetMask("Bullet"));
                Physics2D.queriesStartInColliders = true;
                if(hit.collider != null && hit.collider.GetComponent<WarpGate>() != null)
                {
                    EnterWarpGate?.Invoke();
                }
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
        Vector3 targetPos = MAIN_GRID.GetCellCenterWorld(playerPos + new Vector3Int((int)direction.x, (int)direction.y, 0));
        gameObject.transform.DOMove(targetPos,1f/moveSpeed).OnComplete(() => Move?.Invoke());
        Move?.Invoke(); // Dem tick khi nguoi coi di chuyen
        if (push_object != null) {
            Vector3Int objPos = GetCellPosition(push_object);
            push_object.transform.DOMove(MAIN_GRID.GetCellCenterWorld(objPos + new Vector3Int((int)direction.x, (int)direction.y, 0)),1f/moveSpeed);
        }
    }
    private Vector3Int GetCellPosition(GameObject gameobject) {
        return MAIN_GRID.WorldToCell(gameobject.transform.position);
    }
    bool IsBlocked(Vector2 direction) {
        Physics2D.queriesStartInColliders = false; // Avoid raycast into object own collider
        RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, direction, MoveDistance, ~LayerMask.GetMask("Bullet"));
        Physics2D.queriesStartInColliders = true;
        if (hit.collider != null && (hit.collider.GetComponent<IPushable>() != null || hit.collider.GetComponent<WarpGate>() != null))
            return false;
        return hit;
    }
    bool IsObjectBlocked(GameObject gameobject, Vector2 direction) {
        Physics2D.queriesStartInColliders = false; // Avoid raycast into object own collider
        RaycastHit2D hit = Physics2D.Raycast(gameobject.transform.position, direction, MoveDistance);
        Physics2D.queriesStartInColliders = true;
        return hit;
    }
}
