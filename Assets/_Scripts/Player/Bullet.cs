using UnityEngine;
using DG.Tweening;
using Unity.Mathematics;

public class Bullet : MonoBehaviour 
{
    [SerializeField] private int e_value = 1;
    [SerializeField] ParticleSystem burst; 
    public int GetEValue() // Use for Reciever
    {
        return e_value;
    }
    public void IncreaseEValue(int amount) // Use for Amplifer
    {
        e_value += amount;
        Vector3 cur_scale = gameObject.transform.localScale;
        gameObject.transform.DOScale(cur_scale*2, 0.1f);
    }
    private void OnTriggerEnter2D(Collider2D colldier)
    {
        if(colldier.gameObject.GetComponent<Wall>() || colldier.gameObject.GetComponent<Door>() ||
            colldier.gameObject.GetComponent<BasicPushableBox>())
        {
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        Instantiate(burst, gameObject.transform.position, quaternion.identity);
    }
}
