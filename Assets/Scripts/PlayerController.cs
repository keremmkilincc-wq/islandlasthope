using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// FPS controller - SON IŞIK'taki PointerLockControls mantığının Unity karşılığı
/// WASD + Mouse + SHIFT koş + F fener
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Move")]
    public float walkSpeed = 4f;
    public float sprintSpeed = 6.5f;
    public float jumpForce = 5f;
    public float gravity = -12f;

    [Header("Look")]
    public float mouseSensitivity = 1.5f;
    public Transform cameraRoot;

    [Header("Stamina & Flashlight")]
    public float stamina = 100f;
    public float staminaDrain = 30f;
    public float staminaRecover = 20f;
    public float battery = 100f;
    public float batteryDrain = 0.4f;
    public Light flashlight;

    private CharacterController cc;
    private float verticalVel;
    private float yaw, pitch;
    private bool isSprinting;
    private bool flashOn = true;

    void Awake()
    {
        cc = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        yaw = transform.eulerAngles.y;
    }

    void Update()
    {
        HandleLook();
        HandleMove();
        HandleStamina();
        HandleBattery();
        if (Keyboard.current.fKey.wasPressedThisFrame) ToggleFlash();
        if (Keyboard.current.escapeKey.wasPressedThisFrame) Cursor.lockState = CursorLockMode.None;
    }

    void HandleLook()
    {
        if (Mouse.current == null) return;
        var delta = Mouse.current.delta.ReadValue() * mouseSensitivity * 0.15f;
        yaw += delta.x;
        pitch = Mathf.Clamp(pitch - delta.y, -85f, 85f);
        transform.rotation = Quaternion.Euler(0, yaw, 0);
        if (cameraRoot) cameraRoot.localRotation = Quaternion.Euler(pitch, 0, 0);
    }

    void HandleMove()
    {
        var kb = Keyboard.current;
        Vector2 inp = Vector2.zero;
        if (kb.wKey.isPressed) inp.y += 1;
        if (kb.sKey.isPressed) inp.y -= 1;
        if (kb.aKey.isPressed) inp.x -= 1;
        if (kb.dKey.isPressed) inp.x += 1;
        inp = Vector2.ClampMagnitude(inp, 1f);

        isSprinting = kb.leftShiftKey.isPressed && stamina > 0 && inp.magnitude > 0.1f;
        float speed = isSprinting ? sprintSpeed : walkSpeed;

        bool grounded = cc.isGrounded;
        if (grounded && verticalVel < 0) verticalVel = -2f;
        if (kb.spaceKey.wasPressedThisFrame && grounded) verticalVel = jumpForce;
        verticalVel += gravity * Time.deltaTime;

        Vector3 move = (transform.right * inp.x + transform.forward * inp.y) * speed;
        move.y = verticalVel;
        cc.Move(move * Time.deltaTime);
    }

    void HandleStamina()
    {
        if (isSprinting) stamina = Mathf.Max(0, stamina - staminaDrain * Time.deltaTime);
        else stamina = Mathf.Min(100, stamina + staminaRecover * Time.deltaTime);
    }

    void HandleBattery()
    {
        if (flashOn && flashlight && flashlight.enabled)
        {
            battery = Mathf.Max(0, battery - batteryDrain * Time.deltaTime);
            if (battery <= 0) { flashlight.enabled = false; flashOn = false; }
        }
    }

    void ToggleFlash()
    {
        if (battery <= 0) return;
        flashOn = !flashOn;
        if (flashlight) flashlight.enabled = flashOn;
    }

    public bool IsFlashOn => flashOn && flashlight && flashlight.enabled;
}
