using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreCubes : MonoBehaviour
{
    public GameObject CubePrefabs;
    public void Cube()
    {
        Instantiate(CubePrefabs);
    }
}
