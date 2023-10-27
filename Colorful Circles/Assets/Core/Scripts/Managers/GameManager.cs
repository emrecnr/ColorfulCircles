using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject _selectedObj;
    private GameObject _selectedStand;
    private CircleController _circleController;

    public bool isMove;
    public int numberOfTargetStand;
    [SerializeField] private int _numberOfStandCompleted;



    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100f))
            {
                if (hit.collider != null && hit.collider.CompareTag("Stand"))
                {
                    if (_selectedObj != null && _selectedStand != hit.collider.gameObject)
                    {
                        // Cember gonderme islemleri
                        StandController stand = hit.collider.GetComponent<StandController>();

                        if (stand.circles.Count != 4 && stand.circles.Count != 0)
                        {
                            if (_circleController.color == stand.circles[^1].GetComponent<CircleController>().color)
                            {
                                SetCircle(stand, hit.collider);
                               
                            }
                            else
                            {
                                TurnSocket();
                            }
                            
                        }
                        else if (stand.circles.Count == 0)
                        {
                            SetCircle(stand, hit.collider);
                        }
                        else
                        {
                            TurnSocket();
                        }



                    }
                    else if (_selectedStand == hit.collider.gameObject)
                    {
                        TurnSocket();
                    }
                    else
                    {
                        // Cember cikartma islemleri
                        StandController stand = hit.collider.GetComponent<StandController>();
                        GetCircle(stand);

                    }
                }
            }
        }
    }
    private void GetCircle(StandController stand)
    {
        _selectedObj = stand.GetCircle();
        _circleController = _selectedObj.GetComponent<CircleController>();
        isMove = true;

        if (_circleController.canMove)
        {
            _circleController.Move("Choise", null, null, _circleController.belongsToStand.GetComponent<StandController>().movePosition);
            _selectedStand = _circleController.belongsToStand;
        }
    }
    private void SetCircle(StandController stand,Collider hit)
    {
        _selectedStand.GetComponent<StandController>().ChangeSocket(_selectedObj);

        _circleController.Move("ChangePosition", hit.gameObject,
            stand.GetFreeCircle(), stand.movePosition);

        stand.emptySocketNumber++;
        stand.circles.Add(_selectedObj);
        stand.CheckCircles();
        _selectedObj = null;
        _selectedStand = null;
    }
    private void TurnSocket()
    {
        _circleController.Move("TurnSocket");
        _selectedStand = null;
        _selectedObj = null;
    }
    public void CompletedStand()
    {
        _numberOfStandCompleted++;
        if (_numberOfStandCompleted == numberOfTargetStand)
        {
            Debug.Log("WON!!");
            // TODO: Kazandin Paneli 
        }
    }

}
