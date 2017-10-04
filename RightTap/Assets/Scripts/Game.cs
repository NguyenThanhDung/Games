using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    const int MAX_CHARACTERS = 10;

    public GameObject mcPrefab;
    private GameObject[] mMainCharacters;
    private int mMovingIndex;

    void Start()
    {
        mMainCharacters = new GameObject[MAX_CHARACTERS];
        for (int i = 0; i < MAX_CHARACTERS; i++)
        {
            mMainCharacters[i] = (GameObject)Instantiate(mcPrefab);
        }
        mMovingIndex = -1;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            mMovingIndex = (mMovingIndex >= MAX_CHARACTERS) ? 0 : mMovingIndex + 1;
            mMainCharacters[mMovingIndex].GetComponent<MainCharacter>().Move();
        }
    }
}
