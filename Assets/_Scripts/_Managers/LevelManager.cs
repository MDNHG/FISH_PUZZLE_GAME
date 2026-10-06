using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    int levelID;

    Player_Shoot playerShoot;
    WarpReciever warpReceiver;

    private void Awake()
    {
        playerShoot = FindAnyObjectByType<Player_Shoot>();
        warpReceiver = FindAnyObjectByType<WarpReciever>();
        LoadLevelData();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LoadLevelData()
    {
        string sceneName;
        TextAsset file = Resources.Load<TextAsset>("Levels");
        sceneName = SceneManager.GetActiveScene().name;
        try
        {
            levelID = int.Parse(sceneName.Replace("Level", ""));
        }
        catch (FormatException e)
        {
            Debug.Log(e);
            return;
        }
        string[] lines = file.text.Split('\n');

        string[] values = lines[levelID].Split(',');
        int max_ammo = int.Parse(values[1]);
        int require_e = int.Parse(values[2]);
        playerShoot.SetMaxAmmo(max_ammo);
        warpReceiver.SetRequiredEnergy(require_e);
        Debug.Log(
            $"Level: {levelID}, Ammo: {max_ammo}, Power: {require_e}"
        );
    }
}
