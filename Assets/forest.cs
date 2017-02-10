using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class forest : MonoBehaviour {
    // x = -50 - 50
    // z = 15 - 100
    float amountKilometers = 0f;

    public List<GameObject> trees = new List<GameObject>();
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void GrowTree(float totalDist)
    {
        while (amountKilometers < totalDist)
        {
            GameObject clone;
            int treeToGrow = Random.Range(0, 3);

            GameObject tree = trees[treeToGrow];

            int treePosX = Random.Range(-50, 50);
            int treePosZ = Random.Range(15, 100);
            int treePosY = 0;

            Vector3 treePos = new Vector3(treePosX, treePosY, treePosZ);
            Quaternion treeRot = new Quaternion(0, 0, 0, 0);

            clone = Instantiate(tree, treePos, treeRot) as GameObject;
            amountKilometers = amountKilometers + 500f;
        }
    } 
}
