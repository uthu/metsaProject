using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gps : MonoBehaviour
{
    //TODO
    //-Check if there is movement in specific time -> grow tree
    //
    //-If speed is too high -> dont grow tree
    //
    //
    //
    //
    //
    //
    //
    //
    //


    public string loc;
    Text coordinates;
    Text avarageSpeedText;
    Text totalDistanceText;
    Text locationText;

    public float lifeTimeDist = 0.0f;
    public float lifeTimeDistTemp = 0.0f;
    public float totalDist = 0.0f;
    float travelDist = 0.0f;
    float timerDist = 0.0f;

    float speed = 0.0f;
    float avarageSpeed = 0.0f;


    bool activateGps = true;

    public float latiA = 31.997976f;
    public float latiB = 31.99212f;
    public float longA = 115.762877f;
    public float longB = 115.763228f;
    public bool test = false;


    private IEnumerator coroutine1;
    private IEnumerator coroutine2;

    //TextMesh coordinates;
    //public float coord;

    // Update is called once per frame
    /*void Update()
    {
        coordinates.text = ("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
    }*/

    void Start()
    {
        Input.location.Start();
        coordinates = GameObject.Find("Canvas/coordinates").GetComponent<Text>();
        avarageSpeedText = GameObject.Find("Canvas1/Menu/AvarageSpeed").GetComponent<Text>();
        totalDistanceText = GameObject.Find("Canvas1/Menu/TotalDistance").GetComponent<Text>();
        locationText = GameObject.Find("Canvas1/Menu/Location").GetComponent<Text>();
        coroutine1 = ShowLocation(5.0f);
        coroutine2 = Timer(600.0f);
        StartCoroutine(coroutine1);
        StartCoroutine(coroutine2);
    }

    private IEnumerator Timer(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            if (travelDist > 6000)
                totalDist = totalDist - travelDist + 6000;
        }
    }
    private IEnumerator ShowLocation(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);

            /*print("WaitAndPrint " + Time.time);*/

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
            loc =latiA +"\n"+ longA;
            //METHOD1
            /*
            var point1 = new GeoCoordinate(latiA, longA);
            var point2 = new GeoCoordinate(latiB, longB);

            return point1.GetDistanceTo(point2d);
            */
            //ALTERNATIVE METHOD
            //totalDist = totalDist + distance(latiA, longA, latiB, longB, 'K');
            travelDist = HaversineInM(latiA, longA, latiB, longB);
            speed = travelDist / 5 *3.6f;
            if (speed > 35)
                travelDist = 48.0f;
            avarageSpeed = totalDist/Time.time;
            timerDist = timerDist + travelDist;
            totalDist = totalDist + travelDist;

            locationText.text ="Location: " + "\n" +loc;
            totalDistanceText.text = "Total Distance: " + lifeTimeDist;
            avarageSpeedText.text = "Avarage Speed: " + Mathf.Round(avarageSpeed) + "Km/h";
            coordinates.text ="Walked distance: " + ((Mathf.Round(totalDist / 100))*100) + "Meters" +"\nSpeed: " + Mathf.Round(speed) + "Km/h";
            // Stop service if there is no need to query location updates continuously
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

            //Input.location.Stop();
            lifeTimeDistTemp = totalDist + lifeTimeDist;
            if (lifeTimeDistTemp > 0)
                this.gameObject.GetComponent<forest>().GrowTree(lifeTimeDistTemp);
            
        }


    }

   /* public static void Main()
    {
        Console.WriteLine("Hello World");

        float meLat = 65.01124f;
        float meLong = 25.46673f;


        float result1 = HaversineInM(meLat, meLong, 65.00193f, 25.44416f);
        float result2 = HaversineInM(meLat, meLong, 64.9966681f, 150.77061469270831f);

        Console.WriteLine(result1);
        Console.WriteLine(result2);
    }*/

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
