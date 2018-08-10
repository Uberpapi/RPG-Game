using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitSpawner : MonoBehaviour
{

    [SerializeField]
    public int maxDistance;
    [SerializeField]
    GameObject unit;
    [SerializeField]
    int spawnCooldown;
    [SerializeField]
    int spawnCap;
    RaycastHit hit;
    NavMeshHit hit2;
    List<GameObject> unitList;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(InitSpawn());
    }

    public Vector3 RandomPositionOnMesh()
    {
        Vector3 randomDirection = Random.insideUnitSphere * maxDistance;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, maxDistance, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }


    GameObject clone;
    IEnumerator InitSpawn()
    {
        unitList = new List<GameObject>();
        while (unitList.Count < spawnCap)
        {
            clone = Instantiate(unit, RandomPositionOnMesh(), Quaternion.identity, transform);
            clone.name = unit.name;
            unitList.Add(clone);
            yield return new WaitForSeconds(spawnCooldown);
        }
    }
}
