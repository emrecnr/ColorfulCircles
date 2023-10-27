using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject _selectedObj;
    private GameObject _selectedStand;
    private CircleController _circleController;

    public bool isMove;
    [SerializeField] private int _targetStandNumber;
    [SerializeField] private int _completedStandNumber;



    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100f)) ;
            {
                if (hit.collider != null && hit.collider.CompareTag("Stand"))
                {
                    if (_selectedObj != null && _selectedStand != hit.collider.gameObject)
                    {
                        // Cember gonderme islemleri
                        StandController stand = hit.collider.GetComponent<StandController>();
                        _selectedStand.GetComponent<StandController>().ChangeSocket(_selectedObj);

                        _circleController.Move("ChangePosition",hit.collider.gameObject,
                            stand.GetFreeCircle(),stand.movePosition);

                        stand.emptySocketNumber++;
                        stand.circles.Add(_selectedObj);

                        _selectedObj = null;
                        _selectedStand = null;
                    }
                    else
                    {
                        // Cember cikartma islemleri
                        StandController stand = hit.collider.GetComponent<StandController>();
                        _selectedObj = stand.GetCircle();
                        _circleController = _selectedObj.GetComponent<CircleController>();
                        isMove = true;

                        if (_circleController.canMove)
                        {
                            _circleController.Move("Choise",null,null,_circleController.belongsToStand.GetComponent<StandController>().movePosition);
                            _selectedStand = _circleController.belongsToStand;
                        }

                    }
                }
            }
        }
    }
}
