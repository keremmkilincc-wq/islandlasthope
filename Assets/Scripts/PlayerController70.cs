using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController70 : MonoBehaviour
{
    public float walkSpeed = 4f;
    public float sprintSpeed = 6.2f;
    public float gravity = -12f;
    public float jumpForce = 5f;
    public float mouseSensitivity = 1.4f;
    public Transform cameraRoot;
    public float stamina = 100f;

    private CharacterController cc;
    private float yaw, pitch;
    private float verticalVel;

    void Awake() { cc = GetComponent<CharacterController>(); Cursor.lockState = CursorLockMode.Locked; }

    void Update()
    {
        var kb = Keyboard.current;
        var mouse = Mouse.current;
        if (mouse != null)
        {
            var d = mouse.delta.ReadValue() * mouseSensitivity * 0.15f;
            yaw += d.x; pitch = Mathf.Clamp(pitch - d.y, -80f, 80f);
            transform.rotation = Quaternion.Euler(0, yaw, 0);
            if (cameraRoot) cameraRoot.localRotation = Quaternion.Euler(pitch, 0, 0);
        }
        Vector2 inp = Vector2.zero;
        if (kb.wKey.isPressed) inp.y += 1;
        if (kb.sKey.isPressed) inp.y -= 1;
        if (kb.aKey.isPressed) inp.x -= 1;
        if (kb.dKey.isPressed) inp.x += 1;
        inp = Vector2.ClampMagnitude(inp, 1f);
        bool sprint = kb.leftShiftKey.isPressed && stamina > 0;
        float speed = sprint ? sprintSpeed : walkSpeed;
        if (sprint) stamina = Mathf.Max(0, stamina - 28f * Time.deltaTime);
        else stamina = Mathf.Min(100, stamina + 18f * Time.deltaTime);

        bool grounded = cc.isGrounded;
        if (grounded && verticalVel < 0) verticalVel = -2f;
        if (kb.spaceKey.wasPressedThisFrame && grounded) verticalVel = jumpForce;
        verticalVel += gravity * Time.deltaTime;
        Vector3 move = (transform.right * inp.x + transform.forward * inp.y) * speed;
        move.y = verticalVel;
        cc.Move(move * Time.deltaTime);
        if (kb.escapeKey.wasPressedThisFrame) Cursor.lockState = CursorLockMode.None;
    }
}
