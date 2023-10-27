using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class StandController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameManager _gameManager;
    public GameObject movePosition;
    [SerializeField] private GameObject[] _sockets;

    public int emptySocketNumber;
    public List<GameObject> circles = new();

    private int completedCircle;
    public GameObject GetCircle()
    {
        return circles[^1]; // Cemberlerin en sonuncu elamýna eris = _circles.Count -1 
    }
    public GameObject GetFreeCircle()
    {
        return _sockets[emptySocketNumber]; 
    }
    public void ChangeSocket(GameObject objToBeDeleted)
    {
        circles.Remove(objToBeDeleted);
          
        if (circles.Count != 0)
        {
            emptySocketNumber--;
            circles[^1].GetComponent<CircleController>().canMove = true;
        }
        else
        {
            emptySocketNumber = 0;
        }
    }
    public void CheckCircles()
    {
        if (circles.Count == 4)
        {
            string color = circles[0].GetComponent<CircleController>().color;
            foreach (GameObject circle in circles) 
            {
                if (color == circle.GetComponent<CircleController>().color) completedCircle++;  
                               
            }
            if (completedCircle == 4)
            {
                _gameManager.CompletedStand();
                CompletedStandProcess();
                Debug.Log("Completed!");
            }            
        }
    }
    private void CompletedStandProcess()
    {
        foreach (var circle in circles)
        {
            circle.GetComponent<CircleController>().canMove = false;
            Color32 color = circle.GetComponent<MeshRenderer>().material.GetColor("_Color");
            color.a = 150;
            circle.GetComponent<MeshRenderer>().material.SetColor("_Color",color);
            gameObject.tag = "CompletedStand";
        }
    }
}
