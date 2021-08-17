using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum FlyState { Flying,Gliding};
    [SerializeField]private float rotateSpeed = 2f;
    [SerializeField] private float lerpSpeed = 2f;
    private Rigidbody rb;
    private FlyState flyState;
    private Animator animator;
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
        if (flyState == FlyState.Flying)
        {
            transform.Rotate(rotateSpeed, 0, 0);
        }

    }
    private FlyState ReverseState(FlyState state)
    {
        if (state == FlyState.Flying)
        {
            state = FlyState.Gliding;
            rb.useGravity = false;
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            LerpRotation();
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
    private void LerpRotation()
    {
        transform.rotation=Quaternion.Lerp(transform.rotation,Quaternion.identity,lerpSpeed);
    }
}
