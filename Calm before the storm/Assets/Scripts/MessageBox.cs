using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessageBox : MonoBehaviour
{
    public static MessageBox instance;

    [SerializeField] private GameObject messagePrefab;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void SpawnMessage(string text)
    {
        GameObject obj = Instantiate(messagePrefab, transform);
        obj.GetComponentInChildren<TextMeshProUGUI>().text = text;

        Destroy(obj, 1.5f);
    }
}
