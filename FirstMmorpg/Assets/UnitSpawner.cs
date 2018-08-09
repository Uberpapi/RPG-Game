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

    Vector3 returnVector;
    Vector3 RandomPositionOnMesh()
    {
        returnVector.x = transform.position.x + Random.insideUnitCircle.x * maxDistance;
        returnVector.z = transform.position.z + Random.insideUnitCircle.y * maxDistance;
        returnVector.y = RayHitOnMesh(returnVector.x, returnVector.z);
        return returnVector;

    }


    //Fires a ray and returnsa where it hits the terrain mesh on X,Z coordinates
    float RayHitOnMesh(float x, float z)
    {
        Physics.Linecast(new Vector3(x, 0, z), transform.up, out hit);
        return hit.point.y + 0.2f;
    }

    GameObject clone;
    Vector3 spawnVector;
    IEnumerator InitSpawn()
    {
        unitList = new List<GameObject>();
        while (unitList.Count < spawnCap)
        {
            spawnVector = RandomPositionOnMesh();
            clone = Instantiate(unit, RandomPositionOnMesh(), Quaternion.identity, transform);
            clone.name = unit.name;
            unitList.Add(clone);
            yield return new WaitForSeconds(spawnCooldown);
        }
    }
}
