using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scoreCounter : MonoBehaviour
{
    private int score = 0;


    [Header("UI")]
    [SerializeField] private GameObject keyObject;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // if()
    }

    public void print()
    {
        Debug.Log("hi there");

        score += 1;

        if(score == 4){
            keyObject.SetActive(true);
        }

        Debug.Log("Current Score: " + score);
    }
}
