using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierHandControl : MonoBehaviour
{
    public GameObject machine;
    public GameObject rifle;
    public GameObject pistol;

    public void Machine()
    {
        machine.SetActive(true);
        pistol.SetActive(false);
        rifle.SetActive(false);
    }

    public void Rifle()
    {
        machine.SetActive(false);
        rifle.SetActive(true);
        pistol.SetActive(false);
    }

    public void Pistol()
    {
        machine.SetActive(false);
        pistol.SetActive(true);
        rifle.SetActive(false);
    }
}
