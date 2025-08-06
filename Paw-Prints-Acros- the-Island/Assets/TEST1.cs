using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST1 : MonoBehaviour
{

    public GameObject objectToDuplicate;
    public GameObject objectSpawn;

    public int amountToSpawn;


    public void upadteAmount(){
        amountToSpawn++;

        Debug.Log("amountToSpawn: " + amountToSpawn);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void other(){
        Debug.Log("other");
        if(amountToSpawn > 0){

            GameObject newCopy = Instantiate(objectToDuplicate);
            newCopy.transform.position = objectSpawn.transform.position; // Spawn at host's position
            newCopy.transform.localScale = new Vector3(2, 2, 2);
            amountToSpawn--;
            // Destroy(objectToDuplicate);
            Debug.Log(newCopy.transform.position);
        }
    }
}
