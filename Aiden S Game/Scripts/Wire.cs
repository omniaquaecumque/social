﻿using System.Collections;
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

    public bool attatched = false;

    public Wire attatchedTo = null;


    private void Awake()
    {    
        _image = GetComponent<Image>();

        _line = GetComponent<LineRenderer>();
        _canvas = GetComponentInParent<Canvas>();

        //All top wires are yellow 
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
        this.endDrag();
        this.validPlace = false;
        this.attatchedTo = null;
        correct = false;
    }

    public Color GetColor() {
        return _image.color;
    }

    private void Update()
    {
        //click and drag
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
            //if invalid location, clear line
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

    public void endDrag()
    {
        _DragStart = false;
        _MatchingJob.CurrentWire = null;
    }
    
    //needed for lineRenderer don't delete
    public void OnDrag(PointerEventData eventData)
    {

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //reset wire if already placed
        if (validPlace == true)
        {
            _line.SetPosition(0, Vector3.zero);
            _line.SetPosition(1, Vector3.zero);
            validPlace = false;
            this.attatchedTo.attatched = false;
            this.attatchedTo = null;
            if (correct) {
                _MatchingJob.DecrementCorrect();
            }
            return;
        }
        
        //start drag if valid drag wire
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
        //if endpoint is valid (connection is within the same set of wires or the currently hovered wire isn't already connected to another wire) attach the wire
        if (_MatchingJob.CurrentHovered != null && ((_MatchingJob._P1BottomWires.Contains(_MatchingJob.CurrentWire) && _MatchingJob._P1TopWires.Contains(_MatchingJob.CurrentHovered)) ||(_MatchingJob._P2BottomWires.Contains(_MatchingJob.CurrentWire) && _MatchingJob._P2TopWires.Contains(_MatchingJob.CurrentHovered))) && _MatchingJob.CurrentHovered.attatched == false)
        {
            correct = _MatchingJob.CompareCorrect();
            validPlace = true;
            _MatchingJob.CurrentHovered.attatched = true;
            this.attatchedTo = _MatchingJob.CurrentHovered;
        }
        
        _DragStart = false;
        _MatchingJob.CurrentWire = null;
    }
}
