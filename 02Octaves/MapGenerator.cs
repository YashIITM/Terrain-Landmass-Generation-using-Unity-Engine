using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    //Here we want values defining our map, such as mapWidth and mapHeight (later on : lacunarity and persistance)
    public int mapWidth;
    public int mapHeight;
    public float noiseScale;
    public int octaves;
    //Persistance should always be between 0 and 1, we may use OnValidate as we did for the others, but here we use a slider and solve the issue
    [Range(0,1)]
    public float persistance;
    public float lacunarity;
    public int seed;
    public Vector2 offset;

    public bool autoUpdate;

    //create a method to generate a map
    public void GenerateMap()
    {   //fetching the 2D float array from the Noise class
        float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight,seed, noiseScale, octaves, persistance, lacunarity,offset);

        //we want to call the mapDisplay, with our noiseMap
        //We need  reference to out MapDisplay ofcourse
        MapDisplay display = FindObjectOfType<MapDisplay>();
        display.DrawNoiseMap(noiseMap);
    }

    void OnValidate()
    {
        if (mapWidth < 1)
        {
            mapWidth = 1;
        }
        if (mapHeight < 1)
        {
            mapHeight = 1;
        }
        if (lacunarity < 1)
        {
            lacunarity = 1;
        }
        if (octaves < 0)
        {
            octaves = 0;
        }
    }
}
