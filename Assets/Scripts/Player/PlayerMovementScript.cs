using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;

    private Vector3 startingScale;

    private void Awake()
    {
        startingScale = transform.localScale;
    }

    private void Update()
    {
        Keyboard keyboard = Keyboard.current;
        if (keyboard == null) return;

        float direction = 0f;

        if (keyboard.leftArrowKey.isPressed) direction -= 1f;
        if (keyboard.rightArrowKey.isPressed) direction += 1f;

        // Move horizontally. Delta time keeps speed consistent across frame rates.
        transform.position += Vector3.right
            * direction * movementSpeed * Time.deltaTime;

        // Keep facing the last direction we moved.
        if (direction != 0f)
        {
            transform.localScale = new Vector3(
                Mathf.Abs(startingScale.x) * direction,
                startingScale.y,
                startingScale.z
            );
        }
    }
}