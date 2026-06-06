using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public FixedJoystick joystick;
    public float speed = 5f;

    CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector3 move =
            new Vector3(
                joystick.Horizontal,
                0,
                joystick.Vertical);

        controller.Move(move * speed * Time.deltaTime);
    }

}
