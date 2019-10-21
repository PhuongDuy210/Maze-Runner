using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetecter : MonoBehaviour
{
    private BoxCollider box;
    private TimeMaster timeMaster;
    private ClassicMaster classicMaster;
    // Start is called before the first frame update
    void Start()
    {
        box = gameObject.AddComponent<BoxCollider>();
        box.isTrigger = true;
        box.size = new Vector3(box.size.x, 10f, box.size.z);
        timeMaster = GameObject.FindGameObjectWithTag("GameController").GetComponent<TimeMaster>();
        classicMaster = GameObject.FindGameObjectWithTag("GameController").GetComponent<ClassicMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(timeMaster != null)
                timeMaster.EndLevel();
            if (classicMaster != null)
                StartCoroutine(classicMaster.EndLevel());
        }
    }

}
