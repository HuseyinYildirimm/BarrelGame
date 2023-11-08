using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BarrelController : BarrelManager
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private BarrelType barrelType;
    [SerializeField] private GameObject explosionEffect;

    public float armorValue;
    [SerializeField] private TextMeshProUGUI armorText;
    [SerializeField] private float barrelSpeed;

    GameObject barrelObj;

    public void Start()
    {
        barrelObj = transform.GetChild(0).gameObject;
    }

    public void Update()
    {
        armorText.text = armorValue.ToString();

        transform.Translate(transform.right * barrelSpeed * Time.deltaTime);

        barrelObj.transform.Rotate(-transform.forward);

        if (armorValue <= 0)
        {
            armorValue = 0;
            Vector3 expVector = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + 1f);
            GameObject cloneEffect = Instantiate(explosionEffect,expVector,Quaternion.identity);

            Destroy(cloneEffect, 2f);
            Destroy(gameObject);
            BarrelValue();
        }
    }

    public void BarrelValue()
    {
        switch (barrelType)
        {
            case BarrelType.OneSoldier:
                gameManager.SpawnSoldier(BarrelType.OneSoldier);
                break;
            case BarrelType.TwoSoldier:
                gameManager.SpawnSoldier(BarrelType.TwoSoldier);
                break;
            case BarrelType.Rifle:
                gameManager.GunChange(BarrelType.Rifle);
                break;
            case BarrelType.Machine:
                gameManager.GunChange(BarrelType.Machine);
                break;
            default:
                break;
        }
    }
}
