using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{

    public GameObject mcPrefab;
    private GameObject mainCharater;
    //private GameObject[] mMainCharacters;

    void Start()
    {
        mainCharater = (GameObject)Instantiate(mcPrefab);

        //mMainCharacters = new GameObject[5];
        //for(int i=0; i<5; i++)
        //{
        //    mMainCharacters[i] = (GameObject)Instantiate(mcPrefab);
        //}
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            mainCharater.GetComponent<MainCharacter>().Move();
        }
    }
}
