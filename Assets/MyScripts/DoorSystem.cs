using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSystem : MonoBehaviour
{
    enum Process
    {
        addition,
        multiplication,
        division,
        subtraction,
        removeSoldier
    }

    [SerializeField] private Process processType;
    [SerializeField] private CharacterMovement characters;
    [SerializeField] private int doorValue;
    [SerializeField] private float speed = 2f;
    float value;

    public void Update()
    {
        transform.Translate(transform.right * speed * Time.deltaTime);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SoldierParent"))
        {
            WeaponDelayValueChange();

            Destroy(gameObject);
        }
    }

    public void WeaponDelayValueChange()
    {
        if (processType == Process.removeSoldier)
            characters.RemoveSoldier(doorValue);

        foreach (var c in characters.anims)
        {
            SoldierHandControl soldierHandControl = c.gameObject.GetComponent<SoldierHandControl>();
            GunController gunController = soldierHandControl.machine.GetComponent<GunController>();
            GunController gunController1 = soldierHandControl.rifle.GetComponent<GunController>();
            GunController gunController2 = soldierHandControl.pistol.GetComponent<GunController>();

            if (processType == Process.addition)
            {
                gunController.speed += doorValue;
                gunController1.speed += doorValue;
                gunController2.speed += doorValue;
            }
            if (processType == Process.multiplication)
            {
                gunController.speed *= doorValue;
                gunController1.speed *= doorValue;
                gunController2.speed *= doorValue;
            }
            if (processType == Process.division)
            {
                gunController.speed /= doorValue;
                gunController1.speed /= doorValue;
                gunController2.speed /= doorValue;
            }
            if (processType == Process.subtraction)
            {
                gunController.speed -= doorValue;
                gunController1.speed -= doorValue;
                gunController2.speed -= doorValue;
            }
        }
    }
}
