using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
#if UNITY_EDITOR
    private Vector3 mousePos_;
#else
    private Vector3 touchPos_;
#endif

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
        {
            mousePos_ = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
#else
        if (Input.touchCount > 0)
        {
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
        }

#endif
    }

#if UNITY_EDITOR
    Vector3 GetMousePosition()
    {
        return mousePos_;
    }
#else
    Vector3 GetTouchPosition()
    {
        return touchPos_;
    }
#endif
}
