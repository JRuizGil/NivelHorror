using UnityEngine;

public class Key : MonoBehaviour
{
    public bool isinside;
    public GameObject Canvas;
    public Door[] Door;
    
    void Start()
    {
        Canvas.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isinside = true;
            Canvas.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isinside = false;
            Canvas.SetActive(false);
        }
    }    
    void Update()
    {
        if (isinside)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                foreach (Door o in Door)
                {
                    o.Haskey = true;
                }
                gameObject.SetActive(false);
            }
        }
    }
}
