using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleController : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    

    public GameObject belongsToStand;
    [SerializeField] private GameObject _belongsToSocet;
    public bool canMove;
    public string color;


    private GameObject _movePosition;
    private GameObject _targetStand;

    private bool _select, _changePos, _enterSocket, _turnSocket;


    public void Move(string process, GameObject stand = null, GameObject socket = null, GameObject targetObj = null)
    {
        switch (process)
        {
            case "Choise":
                _movePosition = targetObj;
                _select = true;
                break;
            case "ChangePosition":
                _targetStand = stand;
                _belongsToSocet = socket;
                _movePosition = targetObj;
                _changePos = true;
                break;
            case "TurnSocket":
                _turnSocket = true;
                break;
        }
    }
    private void Update()
    {
        Select();
    }
    private void Select()
    {
        if (_select)
        {
            transform.position = Vector3.Lerp(transform.position, _movePosition.transform.position, .3f);
            if (Vector3.Distance(transform.position, _movePosition.transform.position) < .1f)
            {
                _select = false;
            }
        }
        if (_changePos)
        {
            transform.position = Vector3.Lerp(transform.position, _movePosition.transform.position, .3f);
            if (Vector3.Distance(transform.position, _movePosition.transform.position) < .1f)
            {
                _changePos = false;
                _enterSocket = true;
            }
        }
        if (_enterSocket)
        {
            transform.position = Vector3.Lerp(transform.position, _belongsToSocet.transform.position, .3f);
            if (Vector3.Distance(transform.position, _belongsToSocet.transform.position) < .1f)
            {
                transform.position = _belongsToSocet.transform.position;
                _enterSocket = false;

                belongsToStand = _targetStand;
                if (belongsToStand.GetComponent<StandController>().circles.Count > 1)
                {
                    belongsToStand.GetComponent<StandController>().circles[^2].GetComponent<CircleController>().canMove = false;
                }
                _gameManager.isMove = false;
            }
        }
        if (_turnSocket)
        {
            transform.position = Vector3.Lerp(transform.position, _belongsToSocet.transform.position, .3f);
            if (Vector3.Distance(transform.position, _belongsToSocet.transform.position) < .1f)
            {
                transform.position = _belongsToSocet.transform.position;
                _turnSocket = false;
                _gameManager.isMove = false;
            }
        }
    }
   
}
