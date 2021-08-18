using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform player;
    [SerializeField] private float transitionSpeed=1f;
    [SerializeField] private float cameraYDistance = 5f;
    [SerializeField] private float cameraZDistance = 10f;
    private Vector3 distanceVector;
    private bool isFlying;
    public bool IsFlying { get { return isFlying; } set { isFlying = value; } }
    void Start()
    {
        distanceVector = new Vector3(0, cameraYDistance, -cameraZDistance);
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    
    void LateUpdate()
    {
        if (!isFlying) return;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, Time.deltaTime * transitionSpeed);
        transform.position = Vector3.Lerp(transform.position, player.position + distanceVector,
            Time.deltaTime * transitionSpeed);
    }
}
