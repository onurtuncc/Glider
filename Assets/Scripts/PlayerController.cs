using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables
    //Mobile controls
    private Touch touch;
    private float deadZone = 5f;
    private float speedModifier = 0.003f;
    //
    public enum FlyState { Flying,Gliding};
    private FlyState flyState;

    [SerializeField]private float rotateSpeed = 2f;
    [SerializeField] private float lerpSpeed = 2f;
    [SerializeField] private float horizontalSpeed = 0.2f;
    [SerializeField] private float upForce = 50f;
   
    private Rigidbody rb;
    private Animator animator;

    private float lerpZ;
    private float lerpZLimit = 20f;

    public GameObject endPanel;
    #endregion

    #region Start and Update Methods
    void Start()
    {
        
        flyState = FlyState.Flying;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            flyState=ReverseState(flyState);
            
        }
        if (Input.GetMouseButtonUp(0)) 
        {
            flyState = ReverseState(flyState);
        }

    }
    private void FixedUpdate()
    {
        
        if (flyState==FlyState.Gliding)
        {
            //Changing rotation to the when moving horizontal
            lerpZ = transform.position.x - Input.mousePosition.x;
            //Limiting our rotation
            if (lerpZ > lerpZLimit)
            {
                lerpZ = lerpZLimit;
            }
            else if(lerpZ < -lerpZLimit)
            {
                lerpZ = -lerpZLimit;
            }
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.x, transform.rotation.y, lerpZ), 0.1f);
            transform.position = new Vector3(transform.position.x + Input.mousePosition.normalized.x * horizontalSpeed, transform.position.y,
                transform.position.z);
            
        }
        else
        {
            transform.Rotate(rotateSpeed, 0, 0);
        }
    }
    #endregion
    private FlyState ReverseState(FlyState state)
    {
        if (state == FlyState.Flying)
        {
            state = FlyState.Gliding;
            rb.useGravity = false;
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, lerpSpeed);
            animator.SetBool("wingIsOpen", true);
        }
        else
        {
            state = FlyState.Flying;
            rb.useGravity = true;
            animator.SetBool("wingIsOpen", false);
        }
        return state;
    }
   
    #region Trigger and Collision Controls
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Rectangle")
        {
            
            rb.AddForce(Vector3.up * upForce);
        }
        else if(other.tag == "Cylinder")
        {
            rb.AddForce(Vector3.up * upForce*2);
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            EndGame();
        }
    }
    #endregion

    private void EndGame()
    {
        
        endPanel.SetActive(true);
    }
}
