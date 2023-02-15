using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise
{
	public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, int seed, float scale, int octaves, float persistance, float lacunarity, Vector2 offset)
	{
		//Create noise map using the width/height that we brought in.
		float[,] noiseMap = new float[mapWidth, mapHeight];

		System.Random prng = new System.Random(seed);
		Vector2[] octaveOffsets = new Vector2[octaves];

		for (int i = 0; i < octaves; i++)
		{
			float offsetX = prng.Next(-100000, 100000) + offset.x;
			float offsetY = prng.Next(-100000, 100000) - offset.y;
			octaveOffsets[i] = new Vector2(offsetX, offsetY);
		}

		//We don't want to divive by 0 or a negative number
		if (scale <= 0)
		{
			scale = 0.0001f;
		}

		float maxValue = float.MinValue;
		float minValue = float.MaxValue;

		float halfWidth = mapWidth / 2f;
		float halfHeight = mapHeight / 2f;

		//Loop through each point of the noise map and give it a value.
		for (int y = 0; y < mapHeight; y++)
		{
			for (int x = 0; x < mapWidth; x++)
			{
				float amplitude = 1;
				float frequency = 1;
				float noiseHeight = 0;
				for (int i = 0; i < octaves; i++)
				{
					float sampleX = (x - halfWidth) / scale * frequency + octaveOffsets[i].x * frequency;
					float sampleY = (y - halfHeight) / scale * frequency - octaveOffsets[i].y * frequency;

					float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
					noiseHeight += perlinValue * amplitude;

					amplitude *= persistance;
					frequency *= lacunarity;
				}
				if (noiseHeight < minValue)
				{
					minValue = noiseHeight;
				}
				if (noiseHeight > maxValue)
				{
					maxValue = noiseHeight;
				}
				noiseMap[x, y] = noiseHeight;
			}
		}
		//Normalize Values
		for (int y = 0; y < mapHeight; y++)
		{
			for (int x = 0; x < mapWidth; x++)
			{
				noiseMap[x, y] = Mathf.InverseLerp(minValue, maxValue, noiseMap[x, y]);
			}
		}
		return noiseMap;
	}
}