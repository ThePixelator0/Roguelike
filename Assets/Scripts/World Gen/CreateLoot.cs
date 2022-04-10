using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLoot : MonoBehaviour
{
    [SerializeField]
    private int entityCount;  // How many things are created
    public GameObject[] entities;   // Array of what can be created

    void Awake()
    {
        entityCount += Random.Range(-entityCount / 2, entityCount / 2);

        for (int i = 0; i < entityCount; i++) {
            int posX = Random.Range((int)transform.parent.transform.position.x - 4, (int)transform.parent.transform.position.x + 4);
            int posY = Random.Range((int)transform.parent.transform.position.y - 4, (int)transform.parent.transform.position.y + 4);

            int rand = Random.Range(0, entities.Length);
            transform.position = new Vector3(posX, posY, entities[rand].transform.position.z);

            Instantiate(entities[rand], transform.position, Quaternion.identity);
        }
    }
}
