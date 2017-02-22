using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class forest : MonoBehaviour {

    float amountKilometers = 0f;
    int animalCounter = 0;

    public List<GameObject> trees = new List<GameObject>();
    public List<int> PosX = new List<int>();
    public List<int> PosY = new List<int>();
    public List<Vector3> treePositions = new List<Vector3>();

    public List<GameObject> animals = new List<GameObject>();
    public List<int> AnPosX = new List<int>();
    public List<int> AnPosY = new List<int>();
    public List<Vector3> animalPositions = new List<Vector3>();

    int animalPosSize = 0;
    int treePosSize = 0;
    
    void Start () {
        //create tree coordinates
        for (int i = -17; i < 17; i = i + 3)
        {
            PosX.Add(i);
        }

        for (int i = -29; i < 29; i = i + 5)
        {
            PosY.Add(i);
        }
        for (int i = 0; i < PosY.Count; i++)
        {
            for (int j = 0; j < PosX.Count; j++)
            {
                treePositions.Add(new Vector3(PosX[j], PosY[i], 0));
            }
        }
        //create animal coordinates
        for (int i = -15; i < 15; i = i + 3)
        {
            AnPosX.Add(i);
        }

        for (int i = -28; i < 29; i = i + 5)
        {
            AnPosY.Add(i);
        }
        for (int i = 0; i < AnPosY.Count; i++)
        {
            for (int j = 0; j < AnPosX.Count; j++)
            {
                animalPositions.Add(new Vector3(AnPosX[j], AnPosY[i], 0));
            }
        }
        animalPosSize = animalPositions.Count;
        treePosSize = treePositions.Count;
    }
	
	void Update () {
	
	}

    public void GrowTree(float lifeTimeDist)
    {
        while (amountKilometers < lifeTimeDist)
        {
            if (treePosSize > 0)
            {
                GameObject clone;

                int treeToGrow = Random.Range(0, 4);
                GameObject tree = trees[treeToGrow];

                int treePos = Random.Range(0, treePosSize);
                float treePosX = treePositions[treePos].x;
                float treePosY = treePositions[treePos].y;
                float treePosZ = 0f;

                //Vector3 temp1 = treePositions[treePos];
                Vector3 temp1 = treePositions[treePosSize - 1];

                treePositions[treePos] = temp1;

                Quaternion treeRot = new Quaternion(0, 0, 0, 0);

                Vector3 treePosV = new Vector3(treePosX, treePosY, treePosZ);
                clone = Instantiate(tree, treePosV, treeRot) as GameObject;
                treePosSize--;
            }
            amountKilometers = amountKilometers + 500f;

            animalCounter++;

            if(animalCounter > 5)
            {
                animalCounter = Random.Range(0, 3);
                if(animalCounter == 1)
                {
                    //spawn animal
                    SpawnAnimal();
                    animalCounter = 0;
                }
            }
            /* int treePosX = Random.Range(0, PosX.Count);
             treePosX = PosX[treePosX];
             int treePosY = Random.Range(0, PosY.Count);
             treePosY = PosY[treePosY];
             int treePosZ = 0;*/



            /*int d = treePositions.Count;
            bool b = true;
            bool addPos = false;
            if (d < 200)
            {
                while (b)
                {
                    for (int i = 0; i < d; i++)
                    {
                        if (treePosX == treePositions[i].x && treePosY == treePositions[i].y)
                        {
                            addPos = true;
                        }
                    }
                    if (!addPos)
                    {
                        treePositions.Add(new Vector3(treePosX, treePosY, treePosZ));
                        b = false;
                    }
                    if (addPos)
                    {
                        treePosX = Random.Range(0, PosX.Count);
                        treePosX = PosX[treePosX];
                        treePosY = Random.Range(0, PosY.Count);
                        treePosY = PosY[treePosY];
                    }
                }

                
            }*/
            
            
        }
    }
    //Animal spawn
    public void SpawnAnimal()
    {
        if (animalPosSize > 0)
        {
            GameObject cloneAnim;

            int animalToSpawn = Random.Range(0, 4);
            GameObject animal = animals[animalToSpawn];

            int animalPos = Random.Range(0, animalPosSize);
            float animalPosX = animalPositions[animalPos].x;
            float animalPosY = animalPositions[animalPos].y + 0.3f;
            float animalPosZ = 0f;

            Vector3 temp2 = animalPositions[animalPosSize - 1];
            animalPositions[animalPos] = temp2;

            Quaternion animalRot = new Quaternion(0, 0, 0, 0);

            Vector3 animalPosV = new Vector3(animalPosX, animalPosY, animalPosZ);
            cloneAnim = Instantiate(animal, animalPosV, animalRot) as GameObject;
            animalPosSize--;
        }
    } 
}
