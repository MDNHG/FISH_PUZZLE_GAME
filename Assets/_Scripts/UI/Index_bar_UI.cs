using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Index_bar_UI : MonoBehaviour
{
    WarpReciever warpReciever;
    Player_Shoot playerShoot;
    [SerializeField] Image WG_capacity_image; // Warp Gate fill up rate
    [SerializeField] Image bullet_num_image; // Warp Gate fill up rate
    [SerializeField] TextMeshProUGUI WG_capacity_text;
    [SerializeField] TextMeshProUGUI bullet_num_text;
    [SerializeField] List<Image> gun_mode_image; 
    float max_bullet;
    float require_capacity;
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
        WG_capacity_image.fillAmount = 0.1f;
        UpdateGunStateUI(1);
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
    void UpdateBulletCountUI(float ammo_left)
    {
        bullet_num_image.DOFillAmount(ammo_left/max_bullet, 0.5f);
        bullet_num_text.text = ammo_left + "/" + max_bullet;
    }
    void UpdateGunStateUI(int mode)
    {
        for(int i = 1 ; i <= gun_mode_image.Count; i++)
        {
            if(i == mode)
            {
                gun_mode_image[i-1].color = Color.cyan;
            }
            else
            {
                gun_mode_image[i-1].color = Color.black;
            }
        }
    }
    void UpdateWarpGateCapacity(float amount)
    {
        WG_capacity_image.DOFillAmount(amount/require_capacity,0.5f);
        WG_capacity_text.text = amount + "/" + require_capacity;

    }
}
