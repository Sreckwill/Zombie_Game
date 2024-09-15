using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoOFFunction : MonoBehaviour
{
    public float timer;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitOFTHEFUNTION(timer));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator WaitOFTHEFUNTION(float timer)
    {
        Debug.Log("Enter the IEnumerator");
        yield return new WaitForSeconds(timer);
        Debug.Log("Print After 5 seconds");
    }
}
