using System;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace VRStandardAssets.Utils
{
    // This class ensures that the UI (such as the reticle and selection bar)
    // are set up correctly.
    public class VRCameraUI : NetworkBehaviour
    {
        [SerializeField] private Canvas m_Canvas;       // Reference to the canvas containing the UI.
        [SerializeField] private Image m_reticleBck;
        [SerializeField] private Color m_color;
        [SerializeField] public LineRenderer m_lr;
        [SerializeField] private Text m_scoreText;
        [SerializeField] private Text m_pointsText;
        private void Awake()
        {
            // Make sure the canvas is on.
            m_Canvas.enabled = true;

            // Set its sorting order to the front.
            m_Canvas.sortingOrder = Int16.MaxValue;

            // Force the canvas to redraw so that it is correct before the first render.
            Canvas.ForceUpdateCanvases();            
        }

        private void Update()
        {
            if (!isLocalPlayer)
            {
                m_reticleBck.color = m_color;
                m_lr.startColor = m_color;
                m_lr.endColor = m_color;
                m_scoreText.color = m_color;
                m_pointsText.color = m_color;
            }

        }
    }
}