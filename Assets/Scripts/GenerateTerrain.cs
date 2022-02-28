using UnityEngine;

public class GenerateTerrain : MonoBehaviour
{
    [SerializeField]
    WorldObject world;

    public Vector3 defaultRotation = new Vector3(-90f, 0f, 0f);

    public float scale = 2;

    // ^2 is amount of tiles
    public int size;
    
    // Higher = more detail (default: 5f)
    public float biomeNoiseScale;

    public GameObject groundTemplate;
    public GameObject objectTemplate;

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
                    Random.seed = (biomes[biome].biomeElements[i].randomSeed + 1) * (x + 100) * (y + 100);
                    if (Random.Range(0f, 1f) >= biomes[biome].biomeElements[i].randomCutoff)
                    {
                        world.AddGround(new Vector2(x, y), biomes[biome].biomeElements[i].objectObject);
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
                        world.AddObject(new Vector2(_x, _y), biomes[biome].biomeElements[i].objectObject);
                        break;
                    }
                }
            }
        }
    }

    void InstantiateWorld()
    {
        for (int x = 0; x < world.GetGroundDictionaryLength(); x++)
        {
            for (int y = 0; y < world.GetGroundDictionaryLength(); y++)
            {
                if (world.ValueAtKeyGround(new Vector2(x, y)))
                {
                    InstantiateGroundTile(new Vector2(x, y));
                }

                if (world.ValueAtKeyObject(new Vector2(x, y)))
                {
                    InstantiateObjectTile(new Vector2(x, y));
                }
            }
        }
    }

    public void InstantiateObjectTile(Vector2 coordinates)
    {
        GameObject temp1;
        GameObject temp2;

        int x = (int)coordinates.x;
        int y = (int)coordinates.y;

        temp1 = Instantiate(world.GetObject(new Vector2(x, y)).states[0], new Vector3((x - (size / 2)) * scale, 0f, (y - (size / 2)) * scale), Quaternion.Euler(defaultRotation));
        temp2 = Instantiate(objectTemplate, new Vector3((x - (size / 2)) * scale, 0f, (y - (size / 2)) * scale), Quaternion.Euler(Vector3.zero));
        if (!world.GetObject(new Vector2(x, y)).objectType.collider)
            temp2.GetComponent<BoxCollider>().enabled = false;
        temp1.transform.parent = temp2.transform;
        temp2.GetComponent<Object>().objectObject = world.GetObject(new Vector2(x, y));
        temp2.GetComponent<Object>().coordinates = new Vector2(x, y);
        temp2.name = temp1.name;
    }

    public void InstantiateGroundTile(Vector2 coordinates)
    {
        GameObject temp1;
        GameObject temp2;

        int x = (int)coordinates.x;
        int y = (int)coordinates.y;

        temp1 = Instantiate(world.GetGround(new Vector2(x, y)).states[0], new Vector3((x - (size / 2)) * scale, 0f, (y - (size / 2)) * scale), Quaternion.Euler(defaultRotation));
        temp2 = Instantiate(groundTemplate, new Vector3((x - (size / 2)) * scale, 0f, (y - (size / 2)) * scale), Quaternion.Euler(Vector3.zero));
        temp1.transform.parent = temp2.transform;
        temp2.GetComponent<Object>().objectObject = world.GetGround(new Vector2(x, y));
        temp2.GetComponent<Object>().coordinates = new Vector2(x, y);
        temp2.name = temp1.name;
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

    public bool isGroundElement;

    // Higher = less
    public float noiseScale;

    public float noisePosition;

    public float noiseCutoff;

    public int randomSeed;

    public float randomCutoff;
}