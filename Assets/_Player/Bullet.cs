using UnityEngine;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour
{
    private int e_value = 1;
    public int GetValue() // Use for Reciever
    {
        return e_value;
    }
    public void IncreaseValue(int amount) // Use for Amplifer
    {
        e_value+=amount;
        gameObject.transform.localScale+= new Vector3(amount,amount,1);
    }
}
