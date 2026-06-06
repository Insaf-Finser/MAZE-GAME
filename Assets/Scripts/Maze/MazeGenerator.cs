using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public int width = 21;
    public int height = 21;
    
    private GameObject[,] floorTiles;
    private GameObject[,] FogTiles;
    public GameObject fogPrefab;
    public GameObject wallPrefab;
    public GameObject floorPrefab;

    private int[,] maze;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GenerateMaze();
        DrawMaze();

        GameObject player = GameObject.Find("Player");

        if (player != null)
        {
            player.transform.position =
    new Vector3(
        1 - width / 2f,
        1,
        1 - height / 2f
    );
        }

        RevealAround(new Vector3(1 - width / 2f,1,1 - height / 2f), 3f);
    }

    void GenerateMaze()
    {
        maze = new int[width,height];

        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                maze[i,j] = 1;
            }
        }

        Carve(1, 1);
    }

    public void RevealAround(Vector3 worldPos, float radius)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (FogTiles[x, y] == null)
                    continue;

                float distance =
                    Vector3.Distance(
                        worldPos,
                        FogTiles[x, y].transform.position);

                if (distance < radius)
                {
                    FogTiles[x, y].SetActive(false);
                }
            }
        }
    }

    void Carve(int x, int y)
    {
        maze[x,y] = 0;

        int[] dirs = { 0, 1, 2, 3 };

        for (int i = 0; i < dirs.Length; i++)
        {
            int r = Random.Range(i, dirs.Length);
            (dirs[i], dirs[r]) = (dirs[r], dirs[i]);
        }

        foreach (int dir in dirs)
        {
            int dx = 0;
            int dy = 0;

            switch (dir)
            {
                case 0: dy = 2; break;
                case 1: dy = -2; break;
                case 2: dx = 2; break;
                case 3: dx = -2; break;
            }

            int nx = x + dx;
            int ny = y + dy;

            if (nx > 0 && nx < width - 1 &&
                ny > 0 && ny < height - 1 &&
                maze[nx, ny] == 1)
            {
                maze[x + dx / 2, y + dy / 2] = 0;
                Carve(nx, ny);
            }


        }
    }

    void DrawMaze()
    {
        floorTiles = new GameObject[width, height];
        FogTiles = new GameObject[width, height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector3 pos = new Vector3(i - width / 2f,0,j - height / 2f);
                
                
                

            

                

                if (maze[i, j] == 1)
                {
                    Instantiate(
                        wallPrefab,
                        pos + Vector3.up,
                        Quaternion.identity);
                }
                else
                {
                    GameObject floor = Instantiate(
                    floorPrefab,
                    pos,
                    Quaternion.identity);

                    floorTiles[i, j] = floor;
                    GameObject fog = Instantiate(
                                          fogPrefab,
                                          pos + new Vector3(0, 1, 0),
                                          Quaternion.identity);


                    FogTiles[i,j] = fog;
                }
            }
        }
    }
}
