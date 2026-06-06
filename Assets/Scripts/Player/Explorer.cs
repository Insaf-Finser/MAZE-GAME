using UnityEngine;

public class Explorer : MonoBehaviour
{
    public MazeGenerator maze;
    public float revealRadius = 3f;

    void Update()
    {
        if (maze != null)
        {
            maze.RevealAround(
                transform.position,
                revealRadius);
        }
    }
}