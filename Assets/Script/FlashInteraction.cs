using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashInteraction : MonoBehaviour
{
    public Transform targetParent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickupFlash()
    {
        transform.SetParent(targetParent);
        transform.localPosition = Vector3.zero;
        Destroy(GetComponent<BoxCollider>());
    }
}
