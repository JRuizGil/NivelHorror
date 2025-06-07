using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject Canvas;
    public GameObject Dooropen;
    public float angulo = 4f;
    public bool isinside;
    public bool open;
    public bool Haskey;
    private void Start()
    {
        Haskey = false;
        open = false;
        isinside = false;
        Canvas.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isinside = true;            
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
    private void Update()
    {
        if (isinside && !open)
        {
            Canvas.SetActive(true);
            if (Input.GetKey(KeyCode.E))
            {
                Debug.Log("abierta");
                Dooropen.transform.rotation = Quaternion.Euler(0f, angulo, 0f);
                open = true;
            }
        }
    }

}
