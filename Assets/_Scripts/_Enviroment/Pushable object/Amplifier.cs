using UnityEngine;

public class Amplifier : MonoBehaviour, IPushable
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.GetComponent<Bullet>() is Bullet temp)
        {
            temp.IncreaseEValue(1);
        }

    }
}
