using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Visual : MonoBehaviour {
    [SerializeField] Sprite front;
    [SerializeField] Sprite back;
    [SerializeField] Sprite side;

    private Player_Input pI;
    private SpriteRenderer sprite;
    private Vector2Int lastDirection = Vector2Int.zero;

    void Start() {
        pI = GetComponentInParent<Player_Movement>().pI;
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update() {
        if (TryReadDirection(pI.Player.Move, out Vector2Int dir) ||
            TryReadDirection(pI.Player.ChangeDirection, out dir)) {
            ChangeSprite(dir);
        }
    }

    private bool TryReadDirection(InputAction action, out Vector2Int direction) {
        direction = Vector2Int.zero;

        if (!action.triggered)
            return false;
        Vector2 raw = action.ReadValue<Vector2>();
        if (raw.x != 0f && raw.y != 0f)
            return false;

        direction = new Vector2Int(Mathf.RoundToInt(raw.x), Mathf.RoundToInt(raw.y));
        return direction != Vector2Int.zero;
    }

    private void ChangeSprite(Vector2Int direction) {
        if (direction == lastDirection)
            return;

        lastDirection = direction;

        if (direction.x != 0) {
            sprite.sprite = side;
            sprite.flipX = direction.x < 0;
        }
        else if (direction.y > 0) {
            sprite.sprite = back;
            sprite.flipX = false;
        }
        else {
            sprite.sprite = front;
            sprite.flipX = false;
        }
    }
}