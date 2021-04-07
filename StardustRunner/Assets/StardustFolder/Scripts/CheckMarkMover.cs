using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckMarkMover : MonoBehaviour
{
    public void Move(GameObject obj)
    {
        if (SceneManager.GetActiveScene().name == "MainMenuScene")
        {
            transform.position = new Vector3(obj.transform.position.x + (obj.GetComponent<RectTransform>().sizeDelta.x / 300), obj.transform.position.y - (obj.GetComponent<RectTransform>().sizeDelta.y / 1200), transform.position.z);
            transform.SetParent(obj.transform);
        }
        else
        {
            transform.position = new Vector3(obj.transform.position.x + (obj.GetComponent<RectTransform>().sizeDelta.x / 150), obj.transform.position.y - (obj.GetComponent<RectTransform>().sizeDelta.y / 1000), transform.position.z);
            transform.SetParent(obj.transform);
        }
    }
}
