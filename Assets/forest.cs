using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class forest : MonoBehaviour {
    // x = -50 - 50
    // z = 15 - 100
    float amountKilometers = 0f;

    public List<GameObject> trees = new List<GameObject>();
    public List<int> PosX = new List<int>();
    public List<int> PosY = new List<int>();
    public List<Vector3> treePositions = new List<Vector3>();
    


    /*//Here you add 3 BadGuys to the List
    badguys.Add( new BadGuy("Harvey", 50));
    badguys.Add( new BadGuy("Magneto", 100));
    badguys.Add( new BadGuy("Pip", 5));*/
    // Use this for initialization
    void Start () {
        for (int i = -17; i < 17; i = i +3)
        {
            PosX.Add(i);
        }

        for (int i = -29; i < 29; i = i + 3)
        {
            PosY.Add(i);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void GrowTree(float lifeTimeDist)
    {
        while (amountKilometers < lifeTimeDist)
        {
            GameObject clone;

            int treeToGrow = Random.Range(0, 3);
            GameObject tree = trees[treeToGrow];


            int treePosX = Random.Range(0, PosX.Count);
            treePosX = PosX[treePosX];
            int treePosY = Random.Range(0, PosY.Count);
            treePosY = PosY[treePosY];
            int treePosZ = 0;

            Vector3 treePos = new Vector3(treePosX, treePosY, treePosZ);
            int d = treePositions.Count;
            bool b = true;
            bool addPos = false;
            if (d < 200)
            {
                while (b)
                {
                    for (int i = 0; i < d; i++)
                    {
                        if (treePosX != treePositions[i].x || treePosY != treePositions[i].y)
                        {
                            addPos = true;
                        }
                    }
                    if (addPos)
                    {
                        treePositions.Add(new Vector3(treePosX, treePosY, treePosZ));
                        b = false;
                    }
                    if (!addPos)
                    {
                        treePosX = Random.Range(0, PosX.Count);
                        treePosX = PosX[treePosX];
                        treePosY = Random.Range(0, PosY.Count);
                        treePosY = PosY[treePosY];
                    }
                }

                Quaternion treeRot = new Quaternion(0, 0, 0, 0);

                clone = Instantiate(tree, treePos, treeRot) as GameObject;
            }
            amountKilometers = amountKilometers + 500f;
            
        }
    } 
}
