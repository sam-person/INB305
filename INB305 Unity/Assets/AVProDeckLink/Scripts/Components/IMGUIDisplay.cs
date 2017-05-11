using UnityEngine;
using System.Collections;

//-----------------------------------------------------------------------------
// Copyright 2014-2016 RenderHeads Ltd.  All rights reserved.
//-----------------------------------------------------------------------------

namespace RenderHeads.Media.AVProDeckLink
{
    [AddComponentMenu("AVPro DeckLink/IMGUI Display")]
    public class IMGUIDisplay : MonoBehaviour
    {
        public DeckLinkInput _inputDecklink;

        public ScaleMode _scaleMode = ScaleMode.ScaleToFit;
        public Color _color = Color.white;
        public int _depth = 0;

        public bool _fullScreen = true;
        public float _x = 0.0f;
        public float _y = 0.0f;
        public float _width = 1.0f;
        public float _height = 1.0f;

        public Texture2D _defaultTexture = null;

        //-------------------------------------------------------------------------

        public void OnGUI()
        {
            if (_inputDecklink == null)
                return;

            _x = Mathf.Clamp01(_x);
            _y = Mathf.Clamp01(_y);
            _width = Mathf.Clamp01(_width);
            _height = Mathf.Clamp01(_height);

            Texture texture = _inputDecklink.OutputTexture == null ? _defaultTexture : _inputDecklink.OutputTexture;

            if (texture != null)
            {
                GUI.depth = _depth;

                Rect rect;
                if (_fullScreen)
                    rect = new Rect(0.0f, 0.0f, Screen.width, Screen.height);
                else
                    rect = new Rect(_x * (Screen.width - 1), _y * (Screen.height - 1), _width * Screen.width, _height * Screen.height);

                GUI.color = _color;
                GUI.DrawTexture(rect, texture, _scaleMode, true);
            }
        }
    }
}