using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public GameObject _obj; // ������ ������Ʈ
    public float maxDistance = 3f; // Raycast �ִ� �Ÿ�
    public Transform controllerTransform; // ��Ʈ�ѷ� Transform

    private LineRenderer lineRenderer; // ���� ������ ����
    private GameObject lastIneractionObject; // ���������� ��ȣ�ۿ��� ������Ʈ

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        if(_obj == null)
        {
            _obj = GetComponent<GameObject>();
            Debug.LogError("���� ������Ʈ�� �����ϴ�.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rayStart = controllerTransform.position + controllerTransform.forward * 0.2f; // �ణ�� ������ �߰�
        RaycastHit hit;
        Vector3 rayEnd = rayStart + controllerTransform.forward * maxDistance;
        
        if(Physics.Raycast(rayStart, controllerTransform.forward, out hit, maxDistance))
        {
            rayEnd = hit.point; // Ray�� ������Ʈ�� ����� ���

            if (hit.collider.CompareTag("Item"))
            {
                GameObject hitObject = hit.collider.gameObject;
                hitObject.GetComponent<ItemInteraction>().TurnOnInteraction();

                if(lastIneractionObject != null && lastIneractionObject != hitObject)
                {
                    lastIneractionObject.GetComponent<ItemInteraction>().TurnOffInteraction();
                }

                lastIneractionObject = hitObject;

                if (OVRInput.GetDown(OVRInput.Button.One))
                {
                    DestroyInteractWithObject(hitObject);
                }
            }
            else
            {
                if (lastIneractionObject != null)
                {
                    lastIneractionObject.GetComponent<ItemInteraction>().TurnOffInteraction();
                    lastIneractionObject = null;
                }
            }
        }
        else
        {
            if(lastIneractionObject != null)
            {
                lastIneractionObject.GetComponent<ItemInteraction>().TurnOffInteraction();
                lastIneractionObject = null;
            }
        }
        lineRenderer.SetPosition(0, rayStart);
        lineRenderer.SetPosition(1, rayEnd);


        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            CreateObjectInDirection();
        }
    }

    void DestroyInteractWithObject(GameObject hitObject)
    {
        Destroy(hitObject);
    }

    void CreateObjectInDirection()
    {
        Vector3 rayStart = controllerTransform.position + controllerTransform.forward * 0.2f; // �ణ�� ������ �߰�
        RaycastHit hit;
        if (Physics.Raycast(rayStart, controllerTransform.forward, out hit, maxDistance))
        {
            Vector3 createPosition = hit.point + hit.normal * 0.1f;
            Instantiate(_obj, createPosition, Quaternion.identity);
        }
    }
}
