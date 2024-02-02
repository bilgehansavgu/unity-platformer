using UnityEngine;
using UnityEngine.Tilemaps;

public class PerlinNoiseGenerator : MonoBehaviour
{
    public int width = 256;
    public int height = 256;
    public float scale = 20f;

    public Tilemap backgroundTilemap;
    public Tilemap floorTilemap;
    public TileBase backgroundTile;
    public TileBase floorTile;

    void Start()
    {
        GenerateNoise();
    }

    void GenerateNoise()
    {
        Vector3Int backgroundOrigin = backgroundTilemap.origin;
        Vector3Int floorOrigin = floorTilemap.origin;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float xCoord = (float)x / width * scale;
                float yCoord = (float)y / height * scale;

                float perlinValue = Mathf.PerlinNoise(xCoord, yCoord);

                Vector3Int backgroundTilePosition = new Vector3Int(backgroundOrigin.x + x, backgroundOrigin.y + y, backgroundOrigin.z);
                Vector3Int floorTilePosition = new Vector3Int(floorOrigin.x + x, floorOrigin.y + y, floorOrigin.z);

                // Adjust the thresholds based on your preference
                if (perlinValue > 0.6f)
                {
                    
                    floorTilemap.SetTile(floorTilePosition, floorTile);
                }
                else
                {
                    backgroundTilemap.SetTile(backgroundTilePosition, backgroundTile);
                }
            }
        }
    }
}