using System;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Player_Movement))]
public class Player_WinLoseHandler : MonoBehaviour
{
    public static event Action Show_Win_Menu;
    private GameObject player;
    [SerializeField] private ParticleSystem burst;

    void Awake() {
        player = gameObject;
    }
    void OnEnable()
    {
        Door.killPlayer += Die;
        Laser.killPlayer += Die;
        Player_Movement.EnterWarpGate += Win;
    
    }
    void OnDisable()
    {
        Door.killPlayer -= Die;
        Laser.killPlayer -= Die;
        Player_Movement.EnterWarpGate -= Win;
    
    }
    void Start()
    {
    }

    public void Die() {
        Destroy(player);
        // Nếu muốn anim cho từng kiểu chết thì để 1 param ở hàm này, rồi so sánh if với 1 string anim prefab theo tên
    }
    public void Win(Vector2 dir)
    {
        Sequence enter = DOTween.Sequence().SetRelative();

        enter.Append(gameObject.transform.DOLocalMove(dir.normalized*-0.5f, 0.5f));
        
        enter.Append(gameObject.transform.DOLocalMove(dir.normalized*1.5f, 0.1f));
        enter.OnComplete(()=>
        {
            Destroy(player);
            Instantiate(burst, gameObject.transform.position, Quaternion.identity);
            Show_Win_Menu?.Invoke();  
        }
        );
    }
}
