using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Mirror;
using UnityEngine.UI;

public class Wire : NetworkBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler 
{
    
    public Color defaultwire;

    public bool bottomwire;

    public bool topwire;
    
    private Image _image;

    private LineRenderer _line;

    private Canvas _canvas;

    private bool _DragStart = false;

    private Matching _MatchingJob;

    public bool validPlace = false;

    public bool correct = false;

    private void Awake()
    {

        
        _image = GetComponent<Image>();

        _line = GetComponent<LineRenderer>();
        _canvas = GetComponentInParent<Canvas>();

        if (topwire)
        {
            this.SetColor(defaultwire);
        }

        _MatchingJob = GetComponentInParent<Matching>();
    }

    public void SetColor(Color color) {
     _image.color = color;
     _line.startColor = color;
     _line.endColor = color;
    }

    public void Clear() {
        _line.SetPosition(0, Vector3.zero);
        _line.SetPosition(1, Vector3.zero);
    }

    public Color GetColor() {
        return _image.color;
    }

    private void Update()
    {
        if (_DragStart)
        {
            Vector2 movePos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _canvas.transform as RectTransform,
                Input.mousePosition,
                _canvas.worldCamera,
                out movePos
                );

            _line.SetPosition(0, transform.position);
            _line.SetPosition(1, _canvas.transform.TransformPoint(movePos));
        }
        else {
            if (!validPlace) {
                _line.SetPosition(0, Vector3.zero);
                _line.SetPosition(1, Vector3.zero);
            }
                
        }

        bool isHovered = RectTransformUtility.RectangleContainsScreenPoint(transform as RectTransform, Input.mousePosition, _canvas.worldCamera);

        if (isHovered) {
            _MatchingJob.CurrentHovered = this;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (validPlace == true)
        {
            _line.SetPosition(0, Vector3.zero);
            _line.SetPosition(1, Vector3.zero);
            validPlace = false;
            if (correct) {
                _MatchingJob.DecrementCorrect();
            }
            return;
        }
        
        if (bottomwire) { 
            _DragStart = true;

            _MatchingJob.CurrentWire = this;
        }
        else {
            return;
        }
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_MatchingJob.CurrentHovered != null && ((_MatchingJob._P1BottomWires.Contains(_MatchingJob.CurrentWire) && _MatchingJob._P1TopWires.Contains(_MatchingJob.CurrentHovered)) ||(_MatchingJob._P2BottomWires.Contains(_MatchingJob.CurrentWire) && _MatchingJob._P2TopWires.Contains(_MatchingJob.CurrentHovered))))
        {
            correct = _MatchingJob.CompareCorrect();
            validPlace = true;
        }
        
        _DragStart = false;
        _MatchingJob.CurrentWire = null;
    }
}
