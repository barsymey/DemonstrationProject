using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainController : MonoBehaviour
{
    private Terrain _terrain;
    void Start()
    {
        _terrain = GetComponent<Terrain>();
    }

    public void AddTerrainHeight(Vector3 point)
    {
        Vector2 terrainCoordinates = GetTerrainCoordinate(point);
        int x = (int)terrainCoordinates.x;
        int y = (int)terrainCoordinates.y;
        float currentHeight = _terrain.terrainData.GetHeight(x, y);
        float[,] heights = {{currentHeight}};
        _terrain.terrainData.SetHeights(x, y, heights);
    }


    public Vector2 GetTerrainCoordinate(Vector3 worldPoint)
    {
        Vector2 terrainCoordinate = (worldPoint - _terrain.transform.position) / _terrain.terrainData.size.x;
        terrainCoordinate.x *= _terrain.terrainData.alphamapWidth;
        terrainCoordinate.y *= _terrain.terrainData.alphamapHeight;
        Debug.Log(terrainCoordinate);
        return terrainCoordinate;
    }


}
