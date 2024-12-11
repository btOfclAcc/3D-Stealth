using System.Collections;
using TMPro;
using UnityEngine;

public class Detection : MonoBehaviour
{
    public static Detection instance;

    private int detectionLevel = 0;

    [HideInInspector] public bool isDecaying = true;

    [SerializeField] private GameObject loseText;

    private Coroutine decayCoroutine;

    void Start()
    {
        instance = this;
        decayCoroutine = StartCoroutine(Decay());
    }

    void Update()
    {
        string detectionLevelString = "";
        for (int i = 0; i < detectionLevel; i++)
        {
            detectionLevelString += "|";
        }
        gameObject.GetComponent<TextMeshProUGUI>().text = detectionLevelString;

        if (detectionLevel >= 100)
        {
            StopCoroutine(decayCoroutine);
            loseText.SetActive(true);
        }
    }

    public void Caught()
    {
        detectionLevel = 100;
    }

    public void IncreaseDetectionLevel()
    {
        if (detectionLevel <= 100)
        {
            detectionLevel++;
        }
    }

    public void DecreaseDetectionLevel()
    {
        if (detectionLevel > 0)
        {
            detectionLevel--;
        }
    }

    IEnumerator Decay()
    {
        if (isDecaying)
        {
            DecreaseDetectionLevel();
        }
        yield return new WaitForSeconds(0.8f);
        decayCoroutine = StartCoroutine(Decay());
    }
}
