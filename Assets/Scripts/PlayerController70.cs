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
    public Light flashlight;
    public float stamina = 100f;

    private CharacterController cc;
    private float yaw, pitch;
    private float verticalVel;

    void Awake() { cc = GetComponent<CharacterController>(); Cursor.lockState = CursorLockMode.Locked; }

    void Update()
    {
        // InputSystem varsa onu kullan, yoksa eski Input fallback (Play çalışsın diye)
        var kb = Keyboard.current;
        var mouse = Mouse.current;
        bool hasNewInput = kb != null && mouse != null;
        if (hasNewInput)
        {
            var d = mouse.delta.ReadValue() * mouseSensitivity * 0.15f;
            yaw += d.x; pitch = Mathf.Clamp(pitch - d.y, -80f, 80f);
            transform.rotation = Quaternion.Euler(0, yaw, 0);
            if (cameraRoot) cameraRoot.localRotation = Quaternion.Euler(pitch, 0, 0);
        }
        else
        {
            // Fallback: eski Input
            float mx = Input.GetAxis("Mouse X") * mouseSensitivity;
            float my = Input.GetAxis("Mouse Y") * mouseSensitivity;
            yaw += mx; pitch = Mathf.Clamp(pitch - my, -80f, 80f);
            transform.rotation = Quaternion.Euler(0, yaw, 0);
            if (cameraRoot) cameraRoot.localRotation = Quaternion.Euler(pitch, 0, 0);
        }
        Vector2 inp = Vector2.zero;
        if (hasNewInput)
        {
            if (kb.wKey.isPressed) inp.y += 1;
            if (kb.sKey.isPressed) inp.y -= 1;
            if (kb.aKey.isPressed) inp.x -= 1;
            if (kb.dKey.isPressed) inp.x += 1;
            inp = Vector2.ClampMagnitude(inp, 1f);
        }
        else
        {
            if (Input.GetKey(KeyCode.W)) inp.y += 1;
            if (Input.GetKey(KeyCode.S)) inp.y -= 1;
            if (Input.GetKey(KeyCode.A)) inp.x -= 1;
            if (Input.GetKey(KeyCode.D)) inp.x += 1;
            inp = Vector2.ClampMagnitude(inp, 1f);
        }
        bool sprint = hasNewInput ? (kb.leftShiftKey.isPressed && stamina > 0) : (Input.GetKey(KeyCode.LeftShift) && stamina > 0);
        float speed = sprint ? sprintSpeed : walkSpeed;
        if (sprint) stamina = Mathf.Max(0, stamina - 28f * Time.deltaTime);
        else stamina = Mathf.Min(100, stamina + 18f * Time.deltaTime);

        bool grounded = cc.isGrounded;
        if (grounded && verticalVel < 0) verticalVel = -2f;
        bool doJump = hasNewInput ? kb.spaceKey.wasPressedThisFrame : Input.GetKeyDown(KeyCode.Space);
        if (doJump && grounded) verticalVel = jumpForce;
        verticalVel += gravity * Time.deltaTime;
        Vector3 move = (transform.right * inp.x + transform.forward * inp.y) * speed;
        move.y = verticalVel;
        cc.Move(move * Time.deltaTime);
        bool doEsc = hasNewInput ? kb.escapeKey.wasPressedThisFrame : Input.GetKeyDown(KeyCode.Escape);
        if (doEsc) Cursor.lockState = CursorLockMode.None;
        // İlk Play'de imleci kilitle
        if (Input.GetMouseButtonDown(0) && Cursor.lockState != CursorLockMode.Locked) Cursor.lockState = CursorLockMode.Locked;
    }
}
