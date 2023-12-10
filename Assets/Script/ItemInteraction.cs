using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteraction : MonoBehaviour
{
    private Material material;
    private Color originalEmissionColor;

    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        material = renderer.material;
        originalEmissionColor = material.GetColor("_EmissionColor");

        TurnOffInteraction(); // Emission �ʱ�ȭ
    }

    public void TurnOnInteraction()
    {
        material.EnableKeyword("_EMISSION");
        material.SetColor("_EmissionColor", Color.white); // Emission �Ͼ������ ����
    }

    public void TurnOffInteraction()
    {
        //material.DisableKeyword("_EMISSION");
        material.SetColor("_EmissionColor", originalEmissionColor); // ���� Emission �������� ����
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
