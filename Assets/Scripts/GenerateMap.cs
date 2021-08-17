using UnityEngine;
using UnityEngine.SceneManagement;
public class GenerateMap : MonoBehaviour
{
    //Cylinder and rect prism prefabs
    public GameObject[] prefabs;
    
    [SerializeField]private float distanceBetweenTiles=20f;
    [SerializeField] private float verticalObjectNumber = 100f;
    private float objectNumber;
    //The point where we start to put cylinders and rectange prism
    private Vector3 startPoint;
    //Out of bound for first iteration
    private int prevIndex=6;
    void Start()
    {
        startPoint = new Vector3(-250f, 0f, 30f);
        CreateMap();
    }

    
    void CreateMap()
    {
        objectNumber = Mathf.Abs(startPoint.x) * 2 / distanceBetweenTiles;
        for(int j=0; j < 100; j++)
        {
            for (int i = 0; i < objectNumber; i++)
            {
                int randIndex = Random.Range(0, prefabs.Length);
                while (prevIndex == randIndex)
                {
                    randIndex = Random.Range(0, prefabs.Length);
                    prevIndex = randIndex;
                }
                Instantiate(prefabs[randIndex], startPoint + Vector3.right * i*distanceBetweenTiles, Quaternion.identity, this.transform);

            }
            startPoint += Vector3.forward * distanceBetweenTiles;
        }
        
    }
    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
