using UnityEngine;

public class Reciever : MonoBehaviour
{
    public Sprite open;
    public Sprite close;
    [SerializeField] GameObject Door;
    private SpriteRenderer sr;
    private int e_supply = 10;

    void Awake() {
        sr = GetComponent<SpriteRenderer>();
    }

    void Start() {
        sr.sprite = close;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.GetComponent<Bullet>() is Bullet bullet)
        {
            Door.GetComponent<Door>().ChangeTick(e_supply);
            Destroy(collider.gameObject);
            StartCoroutine(ChangeSprite());
        }
    }

    System.Collections.IEnumerator ChangeSprite() {
        sr.sprite = open;
        yield return new WaitForSeconds(1f);
        sr.sprite = close;
    }
}
