using UnityEngine;
using System.Collections;

public class GameEngine : MonoBehaviour {
    gps gpsObj;
	// Use this for initialization
	void Start () {
        gpsObj = GameObject.Find("gps").GetComponent<gps>();
        LoadGame();
	}
	
	// Update is called once per frame
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

    }


    public void TestReset()
    {
        gpsObj.totalDist = 0f;
        gpsObj.lifeTimeDist = 0f;
        gpsObj.lifeTimeDistTemp = 0f;
        PlayerPrefs.SetFloat("WalkedDistance", 0f);
    }
}
