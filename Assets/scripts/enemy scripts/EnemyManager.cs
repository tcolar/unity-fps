using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    [SerializeField]
    private GameObject boarPrefab, canibalPrefab;
    [SerializeField]
    public int boarCount = 75;
    //[SerializeField]
    //private int cannibalCount, boarCount;
    private GameObject[] boars;
    
    void Awake()
    {
        MakeInstance();
        boars = new GameObject[boarCount];
        for (int i=0;i!= boarCount; i++)
        {
            int x = Random.Range(0, 255);
            int z = Random.Range(0, 255);
            GameObject boar = Instantiate(boarPrefab,
                new Vector3(x, 19, z),
                Quaternion.identity);
            boars[i] = boar;
        }
    }

    void MakeInstance()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
}
