using UnityEngine;

public class MiniMapFollow : MonoBehaviour
{
    public Transform player;

    void LateUpdate()
    {
        transform.position =
            new Vector3(
                player.position.x,
                40,
                player.position.z
            );
    }
}