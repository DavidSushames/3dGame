using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public CharacterController charCon;
    public float moveSpeed;
    public InputActionReference moveAction;
    public InputActionReference lookAction;
    public float lookSpeed;
    public float gravityModifier = 4f;
    public InputActionReference jumpAction;
    public float jumpSpeed;
    public float runSpeed;
    public InputActionReference sprintAction;

    private Vector3 currentMovement;
    private Vector2 rotStore;

    Animator anim1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim1 = this.GetComponent<Animator>();
        anim1.SetBool("Run", false);
        anim1.SetBool("Jump", false);
        anim1.SetBool("Back", false);
        anim1.SetBool("Right", false);
        anim1.SetBool("Left", false);
    }

    // Update is called once per frame
    void Update()
    {
        float yStore = currentMovement.y;

        Vector2 moveInput = moveAction.action.ReadValue<Vector2>();
        currentMovement = new Vector3(moveInput.x * moveSpeed, 0f, moveInput.y * moveSpeed);

        if (currentMovement == Vector3.zero)
        {
            anim1.SetBool("Run", false);
            anim1.SetBool("Back", false);
            anim1.SetBool("Right", false);
            anim1.SetBool("Left", false);
            anim1.SetBool("Jump", false);
        }
        else if (moveInput.y > 0f)
        {
            anim1.SetBool("Run", true);
            anim1.SetBool("Back", false);
            anim1.SetBool("Right", false);
            anim1.SetBool("Left", false);
            anim1.SetBool("Jump", false);
        }
        else if (moveInput.y < 0f) 
        {
            anim1.SetBool("Run", false);
            anim1.SetBool("Back", true);
            anim1.SetBool("Right", false);
            anim1.SetBool("Left", false);
            anim1.SetBool("Jump", false);
        }
        else if (moveInput.x > 0f) 
        {
            anim1.SetBool("Run", false);
            anim1.SetBool("Back", false);
            anim1.SetBool("Right", true);
            anim1.SetBool("Left", false);
            anim1.SetBool("Jump", false);
        }
        else if (moveInput.x < 0f)
        {
            anim1.SetBool("Run", false);
            anim1.SetBool("Back", false);
            anim1.SetBool("Right", false);
            anim1.SetBool("Left", true);
            anim1.SetBool("Jump", false);
        }


        Vector3 moveForward = transform.forward * moveInput.y;
        Vector3 moveSideways = transform.right * moveInput.x;
        if (sprintAction.action.IsPressed())
        {
            currentMovement = (moveForward + moveSideways) * runSpeed;

        }
        else
        {
            currentMovement = (moveForward + moveSideways) * moveSpeed;
        }

            //add gravity to keep the character controller on the ground

            if (charCon.isGrounded)
        {
            yStore = 0f;

        }

        currentMovement.y = yStore + (Physics.gravity.y * Time.deltaTime * gravityModifier);

        //handle jump

        if (jumpAction.action.WasPressedThisFrame())
        {

            //Debug.Log("Jump invoked");

            currentMovement.y = jumpSpeed;

            anim1.SetBool("Run", false);
            anim1.SetBool("Back", false);
            anim1.SetBool("Right", false);
            anim1.SetBool("Left", false);
            anim1.SetBool("Jump", true);

        }
        

            charCon.Move(currentMovement * Time.deltaTime);
        

        //Debug.Log(moveInput);
        //Debug.Log(moveForward);

        //handle turning

        Vector2 LookInput = lookAction.action.ReadValue<Vector2>();
        rotStore = rotStore + (LookInput * lookSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0f, rotStore.x, 0f);
    }
}
