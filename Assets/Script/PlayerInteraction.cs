using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float maxDistance = 3f; // Raycast �ִ� �Ÿ�
    public Transform controllerTransform; // ��Ʈ�ѷ� Transform

    private LineRenderer lineRenderer; // ���� ������ ����
    private GameObject lastIneractionObject; // ���������� ��ȣ�ۿ��� ������Ʈ
    private bool flashCheck = false; // �������� �ֿ� ����

    private Vector3 rayStart; // Ray�� ����
    private Vector3 rayEnd; // Ray�� ��

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // ������ ���ο� ���� ray ���� ����
        if (!flashCheck)
        {
            rayStart = controllerTransform.position + controllerTransform.forward * 0.1f; // �ణ�� ������ �߰�
            rayEnd = rayStart + controllerTransform.forward * maxDistance;
        }
        //else
        //{
        //    rayStart = controllerTransform.position + controllerTransform.up * 0.1f;
        //    rayEnd = rayStart + controllerTransform.up * maxDistance;
        //}

        RaycastHit hit;
        if(Physics.Raycast(rayStart, controllerTransform.forward, out hit, maxDistance))
        {
            rayEnd = hit.point; // Ray�� ������Ʈ�� ����� ���

            if (hit.collider.CompareTag("Item") || hit.collider.CompareTag("Flash"))
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
                    if (hitObject.CompareTag("Item"))
                    {
                        Destroy(hitObject);
                    }
                    else if(hitObject.CompareTag("Flash"))
                    {
                        hitObject.GetComponent<FlashInteraction>().PickupFlash();
                        controllerTransform = hitObject.GetComponentInParent<Transform>();
                        //flashCheck = true;
                    }
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
    }
}
