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

        // Zmie� pozycj� nowego terenu, aby unikn�� nak�adania si� na oryginalny teren
        Vector3 newPosition = new Vector3(originalTerrain.transform.position.x, -5f, originalTerrain.transform.position.z);
        targetTerrain.transform.position = newPosition;

        // Pobierz wysoko�ci wierzcho�k�w nowego terenu
        float[,] newHeights = targetTerrainData.GetHeights(0, 0, sourceTerrainData.heightmapResolution, sourceTerrainData.heightmapResolution);


        // Pomn� wysoko�� ka�dego wierzcho�ka przez -1 i ustaw minimaln� wysoko��
        for (int x = 0; x < targetTerrainData.heightmapResolution; x++)
        {
            for (int z = 0; z < targetTerrainData.heightmapResolution; z++)
            {
                
                newHeights[x, z] = 0.01f - newHeights[x, z];
            }
            //Debug.Log(debugMessage);
        }



        // Ustaw zmienione wysoko�ci wierzcho�k�w na nowym terenie
        targetTerrainData.SetHeights(0, 0, newHeights);

    }
}
