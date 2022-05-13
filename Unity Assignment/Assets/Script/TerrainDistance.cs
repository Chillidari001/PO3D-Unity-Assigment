using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainDistance : MonoBehaviour
{
    public Terrain m_terrain;  //reference to terrain
    public float DrawDistance =  1000; // intended detail distance

    // Use this for initialization
    void Start()
    {

        m_terrain.detailObjectDistance = DrawDistance;

    }
}
