using UnityEngine;

public class Bullet : MonoBehaviour 
{
    [SerializeField] private int e_value = 1;
    public int GetEValue() // Use for Reciever
    {
        return e_value;
    }
    public void IncreaseEValue(int amount) // Use for Amplifer
    {
        e_value+=amount;
        gameObject.transform.localScale*=2;
    }
    private void OnTriggerEnter2D( Collider2D colldier)
    {
        if(colldier.gameObject.GetComponent<Wall>())
        {
            Destroy(gameObject);
        }
    }
}
