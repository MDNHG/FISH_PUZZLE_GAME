using UnityEngine;

public class Reciever : MonoBehaviour
{
    [SerializeField] GameObject Door;
    private int cur_e;
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
            Door.GetComponent<Door>().ChangeTick(10);
            Destroy(collider.gameObject);
        }
    }
}
