using UnityEngine;

public class GenerateTerrain : MonoBehaviour
{
    [SerializeField]
    WorldObject world;

    Vector3 defaultRotation = new Vector3(-90f, 0f, 0f);

    float scale = 2;

    // ^2 is amount of tiles
    public int size;
    
    // Higher = more detail (default: 5f)
    public float biomeNoiseScale;

    int _x;
    int _y;

    public Biome[] biomes;
    float[] randomNums;

    void Start()
    {
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
                    float currentValue = Mathf.PerlinNoise((x + randomNums[i]) * biomeNoiseScale, y * biomeNoiseScale) + biomes[i].noisePriority;

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

        InstantiateWorld();
    }

    void CreateBiomeTile(int x, int y, int biome)
    {
        // Ground elements
        for (int i = 0; i < biomes[biome].biomeElements.Length; i++)
        {
            if (biomes[biome].biomeElements[i].isGroundElement)
            {
                if (Mathf.PerlinNoise((x + biomes[biome].biomeElements[i].noisePosition) * biomes[biome].biomeElements[i].noiseScale, y * biomes[biome].biomeElements[i].noiseScale) >= biomes[biome].biomeElements[i].noiseCutoff)
                {
                    Random.seed = (biomes[biome].biomeElements[i].randomSeed + 1) * (_x + 100) * (_y + 100);
                    if (Random.Range(0f, 1f) >= biomes[biome].biomeElements[i].randomCutoff)
                    {
                        if (biomes[biome].biomeElements[i].hasCustomRotation)
                            world.Add(new Vector2(_x, _y), biomes[biome].biomeElements[i].objectObject);
                            //Instantiate(biomes[biome].biomeElements[i].prefab, new Vector3((_x - (size / 2)) * scale, 0f, (_y - (size / 2)) * scale), Quaternion.Euler(biomes[biome].biomeElements[i].customRotation));
                        else
                            world.Add(new Vector2(_x, _y), biomes[biome].biomeElements[i].objectObject);
                            //Instantiate(biomes[biome].biomeElements[i].prefab, new Vector3((_x - (size / 2)) * scale, 0f, (_y - (size / 2)) * scale), Quaternion.Euler(defaultRotation));
                        break;
                    }
                }
            }
        }

        // Objects
        for (int i = 0; i < biomes[biome].biomeElements.Length; i++)
        {
            if (!biomes[biome].biomeElements[i].isGroundElement)
            {
                if (Mathf.PerlinNoise((x + biomes[biome].biomeElements[i].noisePosition) * biomes[biome].biomeElements[i].noiseScale, y * biomes[biome].biomeElements[i].noiseScale) >= biomes[biome].biomeElements[i].noiseCutoff)
                {
                    Random.seed = (biomes[biome].biomeElements[i].randomSeed + 1) * (_x + 100) * (_y + 100);
                    if (Random.Range(0f, 1f) >= biomes[biome].biomeElements[i].randomCutoff)
                    {
                        if (biomes[biome].biomeElements[i].hasCustomRotation)
                            world.Add(new Vector2(_x, _y), biomes[biome].biomeElements[i].objectObject);
                            //Instantiate(biomes[biome].biomeElements[i].prefab, new Vector3((_x - (size / 2)) * scale, 0f, (_y - (size / 2)) * scale), Quaternion.Euler(biomes[biome].biomeElements[i].customRotation));
                        else
                            world.Add(new Vector2(_x, _y), biomes[biome].biomeElements[i].objectObject);
                            //Instantiate(biomes[biome].biomeElements[i].prefab, new Vector3((_x - (size / 2)) * scale, 0f, (_y - (size / 2)) * scale), Quaternion.Euler(defaultRotation));
                        break;
                    }
                }
            }
        }
    }

    void InstantiateWorld()
    {
        for (int x = 0; x < world.GetDictionaryLength(); x++)
        {
            for (int y = 0; y < world.GetDictionaryLength(); y++)
            {
                // TEMPORARY Instantiate(world.GetObject(x, y).prefab, new Vector3((_x - (size / 2)) * scale, 0f, (_y - (size / 2)) * scale), Quaternion.Euler(defaultRotation));
            }
        }
    }
}

// Environment element class
[System.Serializable]
public class Biome
{
    public string biomeName;

    public float noisePriority;

    public BiomeElement[] biomeElements;
}

// Environment element class
[System.Serializable]
public class BiomeElement
{
    public ObjectObject objectObject;

    // Need to get rid of
    public GameObject prefab;

    public bool hasCustomRotation;

    public Vector3 customRotation;

    public bool isGroundElement;

    // Higher = less
    public float noiseScale;

    public float noisePosition;

    public float noiseCutoff;

    public int randomSeed;

    public float randomCutoff;
}