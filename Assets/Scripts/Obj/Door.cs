using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using System.Collections;

public class Door : MonoBehaviour
{
    public GameObject Canvas;
    public GameObject Doorclosed;
    public GameObject Dooropen;
    public GameObject Opntxt;
    public GameObject Clostxt;
    public GameObject Keytxt;

    public bool isinside;
    public bool open;
    public bool Haskey;

    public float cooldownTime = 0.5f; 
    public float nextActionTime = 0f; 

    private void Start()
    {
        Haskey = false;
        open = false;
        isinside = false;
        Canvas.SetActive(false);
        Dooropen.SetActive(false);
        Doorclosed.SetActive(true);
        Opntxt.SetActive(true);
        Clostxt.SetActive(false);
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
            if (Haskey)
            {
                if (Input.GetKeyDown(KeyCode.E) && Time.time >= nextActionTime)
                {
                    if (!open)
                    {
                        Debug.Log("abierta");
                        Doorclosed.SetActive(false);
                        Dooropen.SetActive(true);
                        Opntxt.SetActive(false);
                        Clostxt.SetActive(true);
                        open = true;
                    }
                    else
                    {
                        Debug.Log("cerrada");
                        Doorclosed.SetActive(true);
                        Dooropen.SetActive(false);
                        Opntxt.SetActive(true);
                        Clostxt.SetActive(false);
                        open = false;
                    }
                    nextActionTime = Time.time + cooldownTime;
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.E) && Time.time >= nextActionTime)
                {
                    StartCoroutine(Keymsg());
                    nextActionTime = Time.time + cooldownTime;
                }

            }
            
        }
    }
    private IEnumerator Keymsg()
    {
        Opntxt.SetActive(false);
        Clostxt.SetActive(false);
        Keytxt.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Keytxt.SetActive(false);
        if (open) { Clostxt.SetActive(true); }
        else { Opntxt.SetActive(true); }
        yield break;
    }
}
