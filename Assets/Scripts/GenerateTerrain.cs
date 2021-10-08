using UnityEngine;

public class GenerateTerrain : MonoBehaviour
{
    // Prefabs

    float currentSample;

    Vector3 defaultRotation = new Vector3(-90f, 0f, 0f);

    // Larger = more detail
    float scale = 2;

    // ^2 is amount of tiles
    float size = 100f;

    float randomPosition01;
    float randomPosition02;
    float randomPosition03;

    float randomBiomePosition01;
    float randomBiomePosition02;
    float randomBiomePosition03;

    int _x;
    int _y;

    // ----Biomes----

    // 1. Forest
    // 2. Desert
    // 3. Mountains
    // 4. Ocean
    // 5. Snowy

    // --------------

    public Biome[] biomes;
    float[] randomNums;

    float biomeNoiseScale = 0.0511f;

    void Start()
    {
        randomPosition01 = Random.Range(-1000, 1000);
        randomPosition02 = Random.Range(-1000, 1000);
        randomPosition03 = Random.Range(-1000, 1000);

        randomBiomePosition01 = Random.Range(-1000, 1000);
        randomBiomePosition02 = Random.Range(-1000, 1000);
        randomBiomePosition03 = Random.Range(-1000, 1000);

        randomNums = new float[biomes.Length];

        CreateTerrain();
    }

    void CreateTerrain()
    {
        for (int i = 0; i < biomes.Length; i++)
        {
            // Out of range
            randomNums[i] = Random.Range(-1000f, 1000f);
        }

        for (int x = 0; x < size; x++)
        {
            _x = x;
            for (int y = 0; y < size; y++)
            {
                _y = y;
                // Each tile

                float value = 0f;
                int valueIndex = 0;

                for (int i = 0; i < biomes.Length; i++)
                {
                    float currentValue = Mathf.PerlinNoise((x + randomNums[i]) * biomeNoiseScale, y * biomeNoiseScale);

                    if (value < currentValue)
                    {
                        value = currentValue;
                        valueIndex = i;
                    }
                }

                // We know the coords of the tile, as well as which biome it is

                CreateBiomeTile(x, y, valueIndex);
            }
        }
    }

    void CreateBiomeTile(int x, int y, int biome)
    {
        // Not sure if this works
        for (int i = 0; i < biomes[biome].biomeElements.Length; i++)
        {
            if (biomes[biome].biomeElements[i].isGroundElement)
            {
                if (Mathf.PerlinNoise((x + biomes[biome].biomeElements[i].noisePosition) * biomes[biome].biomeElements[i].noiseScale, y * biomes[biome].biomeElements[i].noiseScale) >= biomes[biome].biomeElements[i].noiseCutoff)
                {
                    if (Random.Range(0f, 1f) >= biomes[biome].biomeElements[i].randomCutoff)
                    {
                        Instantiate(biomes[biome].biomeElements[i].prefab, new Vector3((_x - (size / 2)) * scale, 0f, (_y - (size / 2)) * scale), Quaternion.Euler(defaultRotation));
                        break;
                    }
                }
            }
        }

        for (int i = 0; i < biomes[biome].biomeElements.Length; i++)
        {
            if (!biomes[biome].biomeElements[i].isGroundElement)
            {
                if (Mathf.PerlinNoise((x + biomes[biome].biomeElements[i].noisePosition) * biomes[biome].biomeElements[i].noiseScale, y * biomes[biome].biomeElements[i].noiseScale) >= biomes[biome].biomeElements[i].noiseCutoff)
                {
                    if (Random.Range(0f, 1f) >= biomes[biome].biomeElements[i].randomCutoff)
                    {
                        Instantiate(biomes[biome].biomeElements[i].prefab, new Vector3((_x - (size / 2)) * scale, 0f, (_y - (size / 2)) * scale), Quaternion.Euler(defaultRotation));
                        break;
                    }
                }
            }
        }
    }
}

// Environment element class
[System.Serializable]
public class Biome
{
    public string biomeName;

    public BiomeElement[] biomeElements;
}

// Environment element class
[System.Serializable]
public class BiomeElement
{
    public GameObject prefab;

    public bool isGroundElement;

    // Higher = less
    public float noiseScale;

    public float noisePosition;

    public float noiseCutoff;

    public float randomCutoff;
}