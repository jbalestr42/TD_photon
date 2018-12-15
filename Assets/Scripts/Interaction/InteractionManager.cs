using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractionManager : Singleton<InteractionManager> {

    AInteraction _interaction = null;
    ISelectable _selectable = null;
    bool _isOver = false;
    Ray ray;
    RaycastHit hit;

    void Update() {
        if (!EventSystem.current.IsPointerOverGameObject()) {
            if (_interaction != null) 
            {
                UpdateInteraction();
            } 
            else 
            {
                if (Input.GetMouseButtonDown(0)) {
                    ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
                        var interactable = hit.collider.gameObject.GetComponentInParent<ISelectable>();

                        if (_selectable != interactable) {
                            if (_selectable != null) {
                                _selectable.UnSelect();
                            }
                            _selectable = interactable;
                            if (_selectable != null) {
                                _selectable.Select();
                            }
                        }
                    }
                }
                if (Input.GetKeyDown(KeyCode.Escape)) {
                    if (_selectable != null) {
                        _selectable.UnSelect();
                        _selectable = null;
                    }
                }
            }
        }
    }

    public void UpdateInteraction() {
        int layerMask = 1 << _interaction.GetLayer();

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)) {
            if (!_isOver) {
                _isOver = true;
                _interaction.OnMouseEnter(hit.point);
            } else {
                if (Input.GetMouseButtonDown(0)) {
                    _interaction.OnMouseClick(hit.point);
                } else {
                    _interaction.OnMouseOver(hit.point);
                }
            }
        } else if (_isOver) {
            _isOver = false;
            _interaction.OnMouseExit();
        }


        if (Input.GetKeyDown(KeyCode.Escape)) {
            CancelInteraction();
        }
    }

    public AInteraction GetInteraction() {
        return _interaction;
    }

    public void SetInteraction(AInteraction interaction) {
        CancelInteraction();
        _interaction = interaction;
    }

    public void CancelInteraction() {
        if (_interaction != null) {
            _interaction.Cancel();
            _interaction = null;
        }
    }

    public void EndInteraction() {
        if (_interaction != null) {
            _interaction.End();
            _interaction = null;
        }
    }
}