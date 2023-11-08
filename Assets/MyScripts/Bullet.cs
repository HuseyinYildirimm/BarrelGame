using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject blood;

    public void Start()
    {
        Destroy(gameObject, 2f);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Barrel"))
        {
            other.gameObject.transform.parent.GetComponent<BarrelController>().armorValue--;
        }
        if (other.gameObject.CompareTag("Zombie"))
        {
            other.gameObject.GetComponent<ZombieController>().TakeDamge(50f);

            Vector3 bloodTransform = new Vector3(other.gameObject.transform.position.x,
                other.gameObject.transform.position.y + 1.5f, other.gameObject.transform.position.z);

            GameObject bloodClone = Instantiate(blood, bloodTransform, Random.rotation,other.gameObject.transform);

            Destroy(bloodClone, 2f);
        }
        Destroy(gameObject);
    }
}
