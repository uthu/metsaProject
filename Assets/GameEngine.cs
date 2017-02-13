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
	
	}

    public void LoadGame()
    {      
        gpsObj.lifeTimeDist = PlayerPrefs.GetFloat("WalkedDistance");
    }
    public void SaveGame()
    {
        PlayerPrefs.SetFloat("WalkedDistance", gpsObj.lifeTimeDist);
    }
}
