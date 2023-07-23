using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TerrainEditor : MonoBehaviour
{

    [SerializeField] Terrain originalTerrain;
    [SerializeField] Terrain targetTerrain;

    private TerrainData originalTerrainData;

    // Start is called before the first frame update
    void Start()
    {

        // Pobierz dane oryginalnego terenu
        TerrainData sourceTerrainData = originalTerrain.terrainData;
        TerrainData targetTerrainData = targetTerrain.terrainData;

        // Zmieñ pozycjê nowego terenu, aby unikn¹æ nak³adania siê na oryginalny teren
        Vector3 newPosition = new Vector3(originalTerrain.transform.position.x, -5f, originalTerrain.transform.position.z);
        targetTerrain.transform.position = newPosition;

        // Pobierz wysokoœci wierzcho³ków nowego terenu
        float[,] newHeights = targetTerrainData.GetHeights(0, 0, sourceTerrainData.heightmapResolution, sourceTerrainData.heightmapResolution);


        // Pomnó¿ wysokoœæ ka¿dego wierzcho³ka przez -1 i ustaw minimaln¹ wysokoœæ
        for (int x = 0; x < targetTerrainData.heightmapResolution; x++)
        {
            for (int z = 0; z < targetTerrainData.heightmapResolution; z++)
            {
                
                newHeights[x, z] = 0.01f - newHeights[x, z];
            }
            //Debug.Log(debugMessage);
        }



        // Ustaw zmienione wysokoœci wierzcho³ków na nowym terenie
        targetTerrainData.SetHeights(0, 0, newHeights);

    }
}
