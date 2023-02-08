using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainCreator : MonoBehaviour
{
    Terrain _terrain;
    [SerializeField] float noiseScale = 3;
    [SerializeField] float noiseOffset = 3;

    // Start is called before the first frame update
    void Start()
    {
        _terrain = GetComponent<Terrain>();
        GenerateTerrain();
        
    }

    [ContextMenu("Do Something")]
    void GenerateTerrain()
    {
        int width, height;
        width = _terrain.terrainData.heightmapResolution;
        height = _terrain.terrainData.heightmapResolution;
        
        float [,] heightSamples = new float [height, width];

        int x = 0;
        int counter = 0;
        while (x < width)
        {
            int y = 0;
            while(y < height)
            {
                float perlinX = (float)x/width * noiseScale + noiseOffset;
                float perlinY = (float)y/height * noiseScale + noiseOffset;
                heightSamples[x, y] = Mathf.PerlinNoise(perlinX, perlinY);
                y++;
                counter++;
            }  
            x++;
        }
        Debug.Log(counter);
        _terrain.terrainData.SetHeights(0, 0, heightSamples);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
