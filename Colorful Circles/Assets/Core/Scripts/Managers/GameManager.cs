using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _wonPanel;
    [SerializeField] private GameObject _retryButn;


    [Header("Audios")]
    [SerializeField] private AudioSource _circleSound;
    [SerializeField] private AudioSource _wonSound;


    private GameObject _selectedObj;
    private GameObject _selectedStand;

    private CircleController _circleController;

    public bool isMove;
    [SerializeField] private int numberOfTargetStand;
    private int _numberOfStandCompleted;


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
        _circleSound.Play();
        _selectedObj = stand.GetCircle();
        _circleController = _selectedObj.GetComponent<CircleController>();
        isMove = true;

        if (_circleController.canMove)
        {
            _circleController.Move("Choise", null, null, _circleController.belongsToStand.GetComponent<StandController>().movePosition);
            _selectedStand = _circleController.belongsToStand;
        }
    }
    private void SetCircle(StandController stand, Collider hit)
    {
        Sound("Circle");
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
        Sound("Circle");
        _circleController.Move("TurnSocket");
        _selectedStand = null;
        _selectedObj = null;
    }
    public void CompletedStand()
    {
        _numberOfStandCompleted++;
        Sound("Won");
        if (_numberOfStandCompleted == numberOfTargetStand)
        {
            Debug.Log("WON!!");
            
            _retryButn.SetActive(false);
            _wonPanel.SetActive(true);
        }
    }

    private void Sound(string soundValue)
    {
        switch (soundValue)
        {
            case "Won":
                Debug.Log("Calisti");
                _wonSound.Play();
                break;
            case "Circle":
                _circleSound.Play();
                break;
            
        }
    }

    // Buttons

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Quit()
    {

    }


}
