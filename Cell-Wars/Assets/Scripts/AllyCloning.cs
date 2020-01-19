using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyCloning : MonoBehaviour
{
    public GameObject Ally;

    [SerializeField]
    int numClones = 10;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numClones; i++)
        {
            Quaternion angle = Quaternion.AngleAxis(i  * (360 / numClones), Vector3.forward);
            Vector3 relativePos = angle * Vector3.up;
            Instantiate(Ally, transform.position + relativePos, transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
