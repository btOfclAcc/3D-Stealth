using UnityEngine;

public class InfoComponent : MonoBehaviour
{
    [SerializeField] private GameObject infoText;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.P))
        {
            infoText.SetActive(true);
        }
        else
        {
            infoText.SetActive(false);
        }
    }
}
