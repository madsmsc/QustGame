using UnityEngine;

public class Move : MonoBehaviour
{
    public float moveSpeed;
    public float mouseSpeed;
    public GameObject looking;
    public GameObject head;

    private Vector2 mouseDir;
    private CharacterController characterController;
    private Animator animator;

    private static float MAX_MOUSE_DIR = 50;
    private static float MAX_HEAD_ROTATION = 20;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        mouseDir = new Vector2();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        mouseLook();
        keyboardMove();
    }

    private void mouseLook()
    {
        mouseDir += new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        float lookingRotation = clampMouse(MAX_MOUSE_DIR);
        looking.transform.localRotation = Quaternion.AngleAxis(lookingRotation, Vector3.right);

        float headRotation = clampMouse(MAX_HEAD_ROTATION);
        head.transform.localRotation = Quaternion.AngleAxis(headRotation, Vector3.right);
        transform.localRotation = Quaternion.AngleAxis(mouseDir.x * mouseSpeed, Vector3.up);
    }

    private void keyboardMove()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");
        if(vertical != 0 || horizontal != 0)
        {
            Vector3 move = transform.forward * vertical + transform.right * horizontal;
            characterController.SimpleMove(move.normalized * moveSpeed);
        }
    }

    private float clampMouse(float max)
    {
        if (-mouseDir.y * mouseSpeed > max)
            return max;
        if (-mouseDir.y * mouseSpeed < -max)
            return -max;
        return -mouseDir.y * mouseSpeed;
    }
}