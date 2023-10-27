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

}
