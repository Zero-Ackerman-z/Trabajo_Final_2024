using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public GameObject panel;
    public bool isPanelActive = false;

    void Update()
    {
        if (isPanelActive)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            panel.transform.Translate(new Vector3(horizontal, vertical, 0) * Time.deltaTime * 5f);
        }
    }

    public void ActivatePanel()
    {
        panel.SetActive(true);
        isPanelActive = true;
    }

    public void DeactivatePanel()
    {
        panel.SetActive(false);
        isPanelActive = false;
    }
}
