using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinLoseUI : MonoBehaviour
{
    [SerializeField] GameObject losingMenu;
    [SerializeField] GameObject winningMenu;
    
    void Start()
    {
        Door.crushPlayer += Losing;
        Player_Movement.EnterWarpGate += Winning;
        Laser.roastPlayer += Losing;
        losingMenu.SetActive(false);
        winningMenu.SetActive(false);
    }
    void Update()
    {
        
    }
    void Losing()
    {
        losingMenu.SetActive(true);   
    }
    void Winning()
    {
        losingMenu.SetActive(true);
    }
    void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
