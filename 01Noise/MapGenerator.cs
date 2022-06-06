using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    //Here we want values defining our map, such as mapWidth and mapHeight (later on : lacunarity and persistance)
    public int mapWidth;
    public int mapHeight;
    public float noiseScale;

    public bool autoUpdate;

    //create a method to generate a map
    public void GenerateMap()
    {   //fetching the 2D float array from the Noise class
        float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, noiseScale);

        //we want to call the mapDisplay, with our noiseMap
        //We need  reference to out MapDisplay ofcourse
        MapDisplay display = FindObjectOfType<MapDisplay>();
        display.DrawNoiseMap(noiseMap);
    }
}
