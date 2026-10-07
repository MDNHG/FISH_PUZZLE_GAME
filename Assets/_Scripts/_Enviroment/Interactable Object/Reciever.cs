using UnityEngine;

public class Reciever : MonoBehaviour
{
    [SerializeField] GameObject Door;
    private int e_supply = 10;
    void Start()
    {   
    }
    void Update()
    {
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.GetComponent<Bullet>() is Bullet bullet)
        {
            Door.GetComponent<Door>().ChangeTick(e_supply);
            Destroy(collider.gameObject);
        }
    }
}
