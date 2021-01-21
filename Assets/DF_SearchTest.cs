using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DF_SearchTest : MonoBehaviour
{
    [SerializeField]
    private string itemName;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var searchedObject = transform.DepthFirst(itemName);
            Debug.Log(searchedObject == null ? itemName + " Not Found" : itemName + " found");
        }
    }
}
