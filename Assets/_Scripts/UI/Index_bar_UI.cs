using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Index_bar_UI : MonoBehaviour
{
    WarpReciever warpReciever;
    Player_Shoot playerShoot;
    [SerializeField] Image WG_capacity_sprite; // Warp Gate fill up rate
    [SerializeField] TextMeshProUGUI WG_capacity_text;
    [SerializeField] TextMeshProUGUI bullet_num_text;
    int max_bullet;
    int require_capacity;
    void Awake()
    {
        playerShoot = FindAnyObjectByType<Player_Shoot>();
        warpReciever = FindAnyObjectByType<WarpReciever>();
    }
    void Start()
    {
        max_bullet = playerShoot.GetMaxAmmo();
        require_capacity = warpReciever.GetRequiredEnergy();

        WG_capacity_text.text = "0" + "/" + require_capacity;
        bullet_num_text.text = max_bullet + "/" + max_bullet;
        WG_capacity_sprite.fillAmount = 0;
    }
    void OnEnable()
    {
        playerShoot.decreaseBullet += UpdateBulletCountUI;
        playerShoot.changeGunState += UpdateGunStateUI;
        warpReciever.IncreaseCurrentEnergy += UpdateWarpGateCapacity;
    }
    void OnDisable()
    {
        playerShoot.decreaseBullet -= UpdateBulletCountUI;
        playerShoot.changeGunState -= UpdateGunStateUI;
    }
    void Update()
    {
        
    }
    void UpdateBulletCountUI(int ammo_left)
    {
        bullet_num_text.text = ammo_left + "/" + max_bullet;
    }
    void UpdateGunStateUI(int mode)
    {
    
    }
    void UpdateWarpGateCapacity(float amount)
    {
        WG_capacity_sprite.fillAmount = amount/require_capacity;
        WG_capacity_text.text = amount + "/" + require_capacity;

    }
}
