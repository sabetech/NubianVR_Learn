using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendBulbToPlace : MonoBehaviour
{
    public Transform bulbLocation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void sendBulbToPlace()
    {
        transform.position = bulbLocation.transform.position;
    }
}
