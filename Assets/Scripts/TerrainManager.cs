using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TerrainManager : MonoBehaviour
{
    [Header("References")]
    public Transform player;

    [Tooltip("Terrains in order: terrain1 -> terrain5")]
    public Terrain[] terrains;

    [Header("Terrain Settings")]
    public float terrainLength = 100f;

    [Tooltip("چند terrain پشت سر پلیر فعال بمونه")]
    public int safeBackTerrains = 2;

    private int frontIndex;
    private int backIndex;

    void Start()
    {
        backIndex = 0;
        frontIndex = terrains.Length - 1;
    }

    void Update()
    {
        HandleForwardMovement();
        HandleBackwardMovement();
    }

    void HandleForwardMovement()
    {
        // اگر پلیر خیلی جلو رفت
        float backTerrainEndZ =
            terrains[backIndex].transform.position.z +
            terrainLength * safeBackTerrains;

        if (player.position.z > backTerrainEndZ)
        {
            MoveBackTerrainToFront();
        }
    }

    void HandleBackwardMovement()
    {
        // اگر پلیر برگشت عقب
        float frontTerrainStartZ =
            terrains[frontIndex].transform.position.z -
            terrainLength * safeBackTerrains;

        if (player.position.z < frontTerrainStartZ)
        {
            MoveFrontTerrainToBack();
        }
    }

    void MoveBackTerrainToFront()
    {
        Terrain backTerrain = terrains[backIndex];

        // انتقال به جلو
        backTerrain.transform.position =
            terrains[frontIndex].transform.position +
            Vector3.forward * terrainLength;

        // شیفت اندکس‌ها
        frontIndex = backIndex;
        backIndex = (backIndex + 1) % terrains.Length;
    }

    void MoveFrontTerrainToBack()
    {
        Terrain frontTerrain = terrains[frontIndex];

        // انتقال به عقب
        frontTerrain.transform.position =
            terrains[backIndex].transform.position -
            Vector3.forward * terrainLength;

        // شیفت اندکس‌ها
        backIndex = frontIndex;
        frontIndex = (frontIndex - 1 + terrains.Length) % terrains.Length;
    }
}