using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    [Header("Luz para encender/apagar")]
    public Light Linterna;

    private void Start()
    {
        Linterna.enabled = false;
    }

    void Update()
    {
        ManageLight();
    }
    void ManageLight()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (Linterna != null)
            {
                Linterna.enabled = !Linterna.enabled;
            }
        }
    }
}
