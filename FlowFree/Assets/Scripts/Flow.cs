using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlowFree
{
    enum Direction {Up,Down,Right,Left };
    public class Flow : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("The flow sprite")]
        private Sprite _image;
        [SerializeField]
        [Tooltip("The flow color")]
        private Color _color;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetColor(Color c)
        {
            _color = c;
        }

        public Color GetColor()
        {
            return _color;
        }
    }
}