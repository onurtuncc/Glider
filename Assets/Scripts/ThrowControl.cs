using UnityEngine;

public class ThrowControl : MonoBehaviour
{
    private GameObject rocketMan;
    private Animator stickAnimator;
    float  xStartPosition;
    float force = 0f;
    private float appliedForce = 0f;
    [SerializeField] private float forceMultiplier=20f;
    [SerializeField] private float upForce = 10f;
    [SerializeField] float forceModifier = 400f;
    [SerializeField]private float lerpSpeed=0.05f;
    private bool isClicked = false;
    private bool isReleased = false;
    
    void Start()
    {
        rocketMan = GameObject.FindGameObjectWithTag("Player");
        stickAnimator = GetComponent<Animator>();
    }

    
    void Update()
    {
        
        if (Input.GetMouseButton(0))
        {
            if (!isClicked)
            {
                xStartPosition = Input.mousePosition.x;
                isClicked = true;
            }
            if (Input.mousePosition.x < xStartPosition)
            {
                force = xStartPosition - Input.mousePosition.x;
                force = force / forceModifier;
                
            }
            
            stickAnimator.SetFloat("bendForce", force);

        }
        if (Input.GetMouseButtonUp(0))
        {
            //Mouse released
            if (force > 1)
            {
                force = 1;
            }
            appliedForce = force;
            isReleased = true;
            
        }
        if (isReleased)
        {
            force = Mathf.Lerp(force,-0.1f, lerpSpeed);
            stickAnimator.SetFloat("bendForce", force);
            if (force <= 0f)
            {
                stickAnimator.SetTrigger("releaseTrigger");
                rocketMan.transform.parent = null;
                rocketMan.GetComponent<Rigidbody>().isKinematic = false;
                rocketMan.GetComponent<Rigidbody>().AddForce(new Vector3(0, upForce, appliedForce * forceMultiplier));
                isReleased = false;
                Destroy(this);
                
            }
            
        }
    }
}
