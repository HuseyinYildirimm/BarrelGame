using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject soldierParent;
    [SerializeField] private GameObject soldier;
    [SerializeField] private GameObject text;

    [SerializeField] private SoldierHealth soldierHealth;
    [SerializeField] private CharacterMovement charMovement;
    bool machine = false;
    bool rifle = false;
    bool pistol = true;


    public void Update()
    {
        if (soldierHealth.death && charMovement != null)
        {
            text.SetActive(true);
            charMovement.GetComponent<CapsuleCollider>().enabled = false;
            charMovement.GetComponent<CharacterController>().enabled = false;
        }
        else return;
    }

    public void SpawnSoldier(BarrelManager.BarrelType barrelType)
    {
        if (soldierHealth.death) return;

        if (barrelType == BarrelManager.BarrelType.OneSoldier)
        {
            GameObject soldierClone = Instantiate(soldier, soldierParent.transform.position, Quaternion.identity, soldierParent.transform);
            soldierClone.transform.localRotation = Quaternion.Euler(0, 0, 0);
            charMovement.SoldierAddList(soldierClone);
            
            if(machine) soldierClone.GetComponent<SoldierHandControl>().Machine();
            if(rifle) soldierClone.GetComponent<SoldierHandControl>().Rifle();
            if (pistol) soldierClone.GetComponent<SoldierHandControl>().Pistol();
        }
        if (barrelType == BarrelManager.BarrelType.TwoSoldier)
        {
            GameObject soldierClone = Instantiate(soldier, soldierParent.transform.position, Quaternion.identity, soldierParent.transform);
            soldierClone.transform.localRotation = Quaternion.Euler(0, 0, 0);
            charMovement.SoldierAddList(soldierClone);
           
            GameObject soldierClone2 = Instantiate(soldier, soldierParent.transform.position, Quaternion.identity, soldierParent.transform);
            soldierClone2.transform.localRotation = Quaternion.Euler(0, 0, 0);
            charMovement.SoldierAddList(soldierClone2);

            if (machine) 
            {
                soldierClone.GetComponent<SoldierHandControl>().Machine();
                soldierClone2.GetComponent<SoldierHandControl>().Rifle();
            }
            if (rifle)
            {
                soldierClone.GetComponent<SoldierHandControl>().Rifle();
                soldierClone2.GetComponent<SoldierHandControl>().Rifle();
            }
            if (pistol)
            {
                soldierClone.GetComponent<SoldierHandControl>().Pistol();
                soldierClone2.GetComponent<SoldierHandControl>().Rifle();
            }
        }
    }

    public void GunChange(BarrelManager.BarrelType barrelType)
    {
        if (soldierHealth.death) return;

        foreach (var characters in charMovement.anims)
        {
            SoldierHandControl soldierHand = characters.GetComponent<SoldierHandControl>();

            if(barrelType == BarrelManager.BarrelType.Pistol)
            {
                soldierHand.Pistol();
                pistol = true;
                machine = false;
                rifle = false;
            }

            if (barrelType == BarrelManager.BarrelType.Rifle)
            {
                soldierHand.Rifle();
                rifle = true;
                machine = false;
                pistol = false;
            }

            if (barrelType == BarrelManager.BarrelType.Machine)
            {
                soldierHand.Machine();
                machine = true;
                rifle = false;
                pistol = false;
            }
        }
    }

    public void Restart()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(activeScene.name);
    }
}
