using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

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
        GameObject.Find("Canvas1/Menu/TotalDistance").GetComponent<Text>().text = "Yhteensä kuljettu matka: " + (gpsObj.lifeTimeDist + gpsObj.totalDist) + " Metriä";
        GameObject.Find("Canvas3/coordinates").GetComponent<Text>().text = "Kuljettu matka: " + ((Mathf.Round(gpsObj.totalDist / 100)) * 100) + "Metriä" + "\nNopeus: " + Mathf.Round(gpsObj.speed) + "Km/h";
    }

    public void TestRuntime()
    {
        gpsObj.runTime = gpsObj.runTime + 600f;
        GameObject.Find("Canvas1/Menu/HealthInfo/RunTime").GetComponent<Text>().text = "Kulunut aika: " + Mathf.Round(gpsObj.runTime / 60) + " minuuttia";

    }


    public void TestReset()
    {
        gpsObj.totalDist = 0f;
        gpsObj.lifeTimeDist = 0f;
        gpsObj.lifeTimeDistTemp = 0f;
        PlayerPrefs.SetFloat("WalkedDistance", 0f);
    }
}