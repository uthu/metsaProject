using UnityEngine;
using System.Collections;

public class GameEngine : MonoBehaviour {

    gps gpsObj;
	void Start () {
        gpsObj = GameObject.Find("gps").GetComponent<gps>();
        LoadGame();
	}
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SaveGame();
            Application.Quit();
        }
    }

    public void LoadGame()
    {      
        gpsObj.lifeTimeDist = PlayerPrefs.GetFloat("WalkedDistance");
    }
    public void SaveGame()
    {
        PlayerPrefs.SetFloat("WalkedDistance", gpsObj.lifeTimeDist + gpsObj.totalDist);
    }

    public void TestDistance()
    {
        gpsObj.totalDist = gpsObj.totalDist + 1000f;
    }

    public void TestRuntime()
    {
        gpsObj.runTime = gpsObj.runTime + 600f;

    }


    public void TestReset()
    {
        gpsObj.totalDist = 0f;
        gpsObj.lifeTimeDist = 0f;
        gpsObj.lifeTimeDistTemp = 0f;
        PlayerPrefs.SetFloat("WalkedDistance", 0f);
    }
}
