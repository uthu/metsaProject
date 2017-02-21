using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gps : MonoBehaviour
{
    //TODO
    //-If speed is too high -> dont grow tree
    //

    public string loc;
    Text coordinates;
    Text avarageSpeedText;
    Text totalDistanceText;
    Text locationText;

    Text aMinuteText;
    Text bMinuteText;
    Text cMinuteText;
    Text dMinuteText;

    Image starImage;

    public float lifeTimeDist = 0.0f;
    public float lifeTimeDistTemp = 0.0f;
    public float totalDist = 0.0f;
    float travelDist = 0.0f;
    float timerDist = 0.0f;

    float speed = 0.0f;
    float avarageSpeed = 0.0f;


    bool activateGps = true;

    public float tempLatiA;
    public float tempLongA;
    public float latiA = 31.997976f;
    public float latiB = 31.99212f;
    public float longA = 115.762877f;
    public float longB = 115.763228f;
    public bool test = false;

    int tempTimer = 0;
    float tempTravel = 0;

    //health info and runtime
    public float runTime = 0f;


    private IEnumerator coroutine1;
    //private IEnumerator coroutine2;
    //birds
    public List<GameObject> birds = new List<GameObject>();

    //TextMesh coordinates;
    //public float coord;

    void Start()
    {
        Input.location.Start();
        coordinates = GameObject.Find("Canvas3/coordinates").GetComponent<Text>();
        avarageSpeedText = GameObject.Find("Canvas1/Menu/AvarageSpeed").GetComponent<Text>();
        totalDistanceText = GameObject.Find("Canvas1/Menu/TotalDistance").GetComponent<Text>();
        locationText = GameObject.Find("Canvas3/Location").GetComponent<Text>();
        aMinuteText = GameObject.Find("Canvas1/Menu/HealthInfo/10min").GetComponent<Text>();
        bMinuteText = GameObject.Find("Canvas1/Menu/HealthInfo/20min").GetComponent<Text>();
        cMinuteText = GameObject.Find("Canvas1/Menu/HealthInfo/60min").GetComponent<Text>();
        dMinuteText = GameObject.Find("Canvas1/Menu/HealthInfo/120min").GetComponent<Text>();

        starImage = GameObject.Find("Canvas1/Menu/HealthInfo/star").GetComponent<Image>();

        aMinuteText.enabled = false;
        bMinuteText.enabled = false;
        cMinuteText.enabled = false;
        dMinuteText.enabled = false;
        starImage.enabled = false;

        coroutine1 = ShowLocation(5.0f);
        //coroutine2 = Timer(600.0f);
        StartCoroutine(coroutine1);
        //StartCoroutine(coroutine2);
    }

   /* private IEnumerator Timer(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            if (travelDist > 6000)
            {
                totalDist = totalDist - travelDist + 6000;
                travelDist = 0f;
            }
        }
    }*/
    private IEnumerator ShowLocation(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);

            runTime = runTime + 5f;

            // First, check if user has location service enabled
            /////////////////////////////////////////////////////////////////////////////////////
            //if (!Input.location.isEnabledByUser)
            //    yield break;
            /////////////////////////////////////////////////////////////////////////////////////
            // Start service before querying location

            // Wait until service initializes

            // Access granted and location value could be retrieved
            /////////////////////////////////////////////////////////////////////////////////////
            //loc = "Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp;

            //health timer info
            if (runTime >= 600)
            {
                starImage.enabled = true;
                aMinuteText.enabled = true;
            }
            if (runTime >= 1200)
            {

                aMinuteText.enabled = false;
                cMinuteText.enabled = false;
                dMinuteText.enabled = false;

                bMinuteText.enabled = true;
            }
            if (runTime >= 3600)
            {
                aMinuteText.enabled = false;
                bMinuteText.enabled = false;
                dMinuteText.enabled = false;

                cMinuteText.enabled = true;
            }

            if (runTime >= 7200)
            {
                aMinuteText.enabled = false;
                bMinuteText.enabled = false;
                cMinuteText.enabled = false;

                dMinuteText.enabled = true;
            }


            if (activateGps)
            {
                if (!test)
                {
                    latiA = Input.location.lastData.latitude;
                    longA = Input.location.lastData.longitude;
                }
                activateGps = false;
            }
            if (!test)
            {
                latiB = Input.location.lastData.latitude;
                longB = Input.location.lastData.longitude;
            }
            loc ="Leveysaste: "+latiA + " Pituusaste: " + longA;

            //ALTERNATIVE METHOD
            //totalDist = totalDist + distance(latiA, longA, latiB, longB, 'K');

            //calculate distance
            travelDist = HaversineInM(latiA, longA, latiB, longB);
            speed = travelDist / 5 *3.6f;
            if (speed > 35)
                travelDist = 48.0f;
            avarageSpeed = totalDist/Time.time;
            timerDist = timerDist + travelDist;
            totalDist = totalDist + travelDist;
            tempTravel = tempTravel + travelDist;

            if (tempTimer == 0)
            {
                tempLatiA = latiA;
                tempLongA = longA;
                tempTimer++;
            }
            else
            {
                tempTimer++;
            }
            if (tempTimer > 60)
            {
                travelDist = HaversineInM(tempLatiA, tempLongA, latiB, longB);
                totalDist = totalDist - tempTravel + travelDist;
                tempTravel = 0f;
                tempTimer = 0;
            }
            //Set texboxes texts
            locationText.text ="Sijainti: " + "\n" +loc;
            totalDistanceText.text = "Yhteensä kuljettu matka: " + (lifeTimeDist + totalDist) + " Metriä";
            avarageSpeedText.text = "Keskinopeus: " + Mathf.Round(avarageSpeed) + " Km/h";
            coordinates.text ="Kuljettu matka: " + ((Mathf.Round(totalDist / 100))*100) + " Metriä" +"\nNopeus: " + Mathf.Round(speed) + " Km/h";
            
            if (!test)
            {
                latiA = Input.location.lastData.latitude;
                longA = Input.location.lastData.longitude;
            }
            else
            {
                latiA = latiB;
                longA = longB;
            }
            // Stop service if there is no need to query location updates continuously
            //Input.location.Stop();

            //add lifetime dist (saved data)
            lifeTimeDistTemp = totalDist + lifeTimeDist;
            if (lifeTimeDistTemp > 0)
                this.gameObject.GetComponent<forest>().GrowTree(lifeTimeDistTemp);

            //bird spawn
            int spawn = Random.Range(0, 2);

            if (spawn == 1)
            {
                int birdToSpawn = Random.Range(0, 2);
                GameObject bird = birds[birdToSpawn];
                int d = Random.Range(0, 3);
                for (int i = 0; i < d; i++)
                {
                    if (birdToSpawn == 0)
                    {
                        Vector3 birdPos = new Vector3(40f, Random.Range(0f, 70f), 1f);
                        GameObject clone = Instantiate(bird, birdPos, bird.transform.rotation) as GameObject;
                    }

                    if (birdToSpawn == 1)
                    {
                        Vector3 birdPos = new Vector3(-50f, Random.Range(4f, 100f), 1f);
                        GameObject clone = Instantiate(bird, birdPos, bird.transform.rotation) as GameObject;
                    }
                }               
               
            }
        }


    }


    static float _eQuatorialEarthRadius = 6378.1370f;
    static float _d2r = (Mathf.PI / 180f);

    private static int HaversineInM(float lat1, float long1, float lat2, float long2)
    {
        return (int)(1000f * HaversineInKM(lat1, long1, lat2, long2));
    }

    private static float HaversineInKM(float lat1, float long1, float lat2, float long2)
    {
        float dlong = (long2 - long1) * _d2r;
        float dlat = (lat2 - lat1) * _d2r;
        float a = Mathf.Pow(Mathf.Sin(dlat / 2f), 2f) + Mathf.Cos(lat1 * _d2r) * Mathf.Cos(lat2 * _d2r) * Mathf.Pow(Mathf.Sin(dlong / 2f), 2f);
        float c = 2f * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1f - a));
        float d = _eQuatorialEarthRadius * c;

        return d;
    }
//Method 2
/*
    private float distance(float lat1, float lon1, float lat2, float lon2, char unit)
    {
        float theta = lon1 - lon2;
        float dist = Mathf.Sin(deg2rad(lat1)) * Mathf.Sin(deg2rad(lat2)) + Mathf.Cos(deg2rad(lat1)) * Mathf.Cos(deg2rad(lat2)) * Mathf.Cos(deg2rad(theta));
        dist = Mathf.Acos(dist);
        dist = rad2deg(dist);
        dist = dist * 60f * 1.1515f;
        if (unit == 'K')
        {
            dist = dist * 1.609344f;
        }
        else if (unit == 'N')
        {
            dist = dist * 0.8684f;
        }
        return (dist);
    }

    //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    //::  This function converts decimal degrees to radians             :::
    //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    private float deg2rad(float deg)
    {
        return (deg * (Mathf.PI / 180.0f));
    }

    //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    //::  This function converts radians to decimal degrees             :::
    //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    private float rad2deg(float rad)
    {
        return (rad / Mathf.PI * 180.0f);
    }*/
}
