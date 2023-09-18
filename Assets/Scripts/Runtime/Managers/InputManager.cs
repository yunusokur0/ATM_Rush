using Assets.Scripts.Runtime.Data.UnityObject;
using Assets.Scripts.Runtime.Data.ValueObject;
using Assets.Scripts.Runtime.Keys;
using Assets.Scripts.Runtime.Signals;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    private InputData _data;
    private bool isReadyForTouch, isFirstTimeTouchTaken;
    private bool _isTouching;
    private float _currentVelocity;
    private Vector2? _mousePosition;
    private float3 _moveVector;

    private void Awake()
    {
        _data = GetInputData();
    }

    private InputData GetInputData() => Resources.Load<CD_Input>("Data/CD_Input").Data;

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        CoreGameSignals.Instance.onPlay += OnPlay;
        CoreGameSignals.Instance.onReset += OnReset;
    }

    private void OnPlay()
    {
        isReadyForTouch = true;
    }

    private void OnReset()
    {
        _isTouching = false;
        isReadyForTouch = false;
        isFirstTimeTouchTaken = false;
    }

    private void Update()
    {
        if (!isReadyForTouch) return;

        if (Input.GetMouseButtonDown(0) && !IsPointerOverUIElement())
        {
            //ilk basmada true yapiyor
            _isTouching = true;
            InputSignals.Instance.onInputTaken?.Invoke();
            if (!isFirstTimeTouchTaken)
            {
                isFirstTimeTouchTaken = true;
            }
            //fare pozunu at�yor
            _mousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0) && !IsPointerOverUIElement())
        {
            if (_isTouching)
            {
                if (_mousePosition != null)
                {
                    //                      mevcut fare pozundan onceki fare pozunu aras�ndaki fark
                    //fareyi s�r�kledi�inizde veya hareket ettirdi�inizde olu�an fare pozisyonu de�i�ikliklerini takip etmek i�in kullan�l�r
                    Vector2 mouseDeltaPos = (Vector2)Input.mousePosition - _mousePosition.Value;


                    if (mouseDeltaPos.x > _data.HorizontalInputSpeed)
                        _moveVector.x = _data.HorizontalInputSpeed / 10f * mouseDeltaPos.x;
                    else if (mouseDeltaPos.x < -_data.HorizontalInputSpeed)
                        _moveVector.x = -_data.HorizontalInputSpeed / 10f * -mouseDeltaPos.x;
                    else
                        //Xeksendeki hiz,Xeksendeki Hiz,gidecegi hiz,  , nekadar Hizda yavaslayacak
                        _moveVector.x = Mathf.SmoothDamp(_moveVector.x, 0f, ref _currentVelocity,
                            _data.ClampSpeed);

                    _mousePosition = Input.mousePosition;

                    InputSignals.Instance.onInputDragged?.Invoke(new HorizontalInputParams()
                    {
                        HorizontalValue = _moveVector.x,
                        ClampValues = _data.ClampSides
                    });
                }
            }
        }

        if (Input.GetMouseButtonUp(0) && !IsPointerOverUIElement())
        {
            _isTouching = false;
            InputSignals.Instance.onInputReleased?.Invoke();
        }
    }

    private bool IsPointerOverUIElement()
    {
        //                UI olaylar�n� yakalamak ve i�lemek i�in kullan�lan bir s�n�ft�r, mevcut sahnede tan�mlanan olaylar� dinlemek ve i�lemek i�in kullan�lacak.
        var eventData = new PointerEventData(EventSystem.current);
        // bu kod, fare pozisyonunu eventData i�indeki olay verilerine yazar.
        eventData.position = Input.mousePosition;
        // Bu liste, fare veya dokunmatik giri�in UI ��eleri ile etkile�imde bulunup bulunmad���n� kontrol etmek i�in kullan�lacak.
        var results = new List<RaycastResult>();
        //t�m fare veya dokunmatik giri�lerinin UI ��eleri �zerinde bir ���n (ray) ��kart�larak neye �arpt���n� kontrol eder. eventData ve results verileri bu i�lev i�in kullan�l�r ve sonu�lar results listesine eklenir.
        EventSystem.current.RaycastAll(eventData, results);
        //e�er results listesinde en az bir sonu� varsa (yani fare veya dokunmatik giri� UI ��eleri �zerindeyse), true de�eri d�ner
        return results.Count > 0;
    }
    private void UnsubscribeEvents()
    {
        CoreGameSignals.Instance.onPlay -= OnPlay;
        CoreGameSignals.Instance.onReset -= OnReset;
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }
}
