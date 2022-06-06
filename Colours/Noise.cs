using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//We do not apply this to any object in our scene.So no reason to inherit from 'Monobehaviour'.
//We do not want to make multiple instances of the script, so we make it a 'public static class'.
public static class Noise
{
    //We want this to have a method to generate a noise map.
    //We want that to return essentially a grid of values between 0 and 1.
    //So we create a 'public static' method returning a 2D array of float values called 'GenerateNoiseMap'.

    //We further add additional arguments octaves, persistance, lacunarity

    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, int seed, float scale, int octaves, float persistance, float lacunarity, Vector2 offset)
    {
        //create a 2D float array called 'noiseMap' and set that equal to a new 2D float array of size : mapWidth x mapHeight
        float[,] noiseMap = new float[mapWidth, mapHeight];

        System.Random prng = new System.Random(seed);
        Vector2[] octaveOffsets = new Vector2[octaves];

        for(int i = 0; i < octaves; i++)
        {
            float offsetX = prng.Next(-100000, 100000) + offset.x;
            float offsetY = prng.Next(-100000, 100000) + offset.y;
            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        }
        //to prevent a division by zero error, we handle the case by the if condition:
        if (scale <= 0)
        {
            scale = 0.0001f;
        }

        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        float halfWidth = mapWidth / 2f;
        float halfHeight = mapHeight / 2f;
        //we want to loop through the noiseMap:
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                //now we want to figure out at which points, we want to sample our height values from
                //We do not want to use the same integral values as they would provide the same value everytime, so we scale it 
                //We want to loop through all of our octaves:
                float amplitude = 1;
                float frequency = 1;
                //We also want to keep track of our currrent height value
                float noiseHeight = 0;
                for (int i = 0; i < octaves; i++)
                {
                    float sampleX = (x-halfWidth) / scale * frequency + octaveOffsets[i].x;
                    float sampleY = (y-halfHeight) / scale * frequency + octaveOffsets[i].y;
                    //higher the frequency, further apart the sample points will be, that is height values will change more rapidly
                    //now we have our sample coordinates so we call the Perlin Noise
                    //By default Mathf.PerlinNoise gives a value between 0 and 1, but we would like to have a value between -1 and 1
                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                    //instead of setting the noiseMap directly equal to the perlinValue, we will increase the noiseHeight by the perlinValue of each Octave

                    noiseHeight += perlinValue * amplitude;

                    amplitude *= persistance;
                    frequency *= lacunarity;
                }
                if (noiseHeight > maxNoiseHeight)
                {
                    maxNoiseHeight = noiseHeight;
                }
                else if (noiseHeight < minNoiseHeight)
                {
                    minNoiseHeight = noiseHeight;
                }
                noiseMap[x, y] = noiseHeight;
            }
        }

        //We want to loop through the new range of noise height values:
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {

                noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);
            }
        }
     return noiseMap;
    }

    }
