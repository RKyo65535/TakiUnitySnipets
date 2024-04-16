using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class AnimationText : MonoBehaviour
    {
        private Transform TF;
        private TextMeshProUGUI t;
        [SerializeField] private float xRatio = 1;
        [SerializeField] private AnimationCurve x;
        [SerializeField] private float yRatio = 1;
        [SerializeField] private AnimationCurve y;
        [SerializeField] private AnimationCurve alpha;
        [SerializeField] private float time;
        private float passedTime;

        private Color initialColor;
        private Vector3 initialPos;
        
        private void Awake()
        {
            t = GetComponent<TextMeshProUGUI>();
            TF = transform;
            initialColor = t.color;
            initialPos = TF.position;
        }

        private void Update()
        {
            passedTime += Time.deltaTime;
            float normalized = passedTime / time;
            if(normalized > 1) Destroy(gameObject);

            TF.position = initialPos + new Vector3(x.Evaluate(normalized) * xRatio, y.Evaluate(normalized)*yRatio, 0);
            t.color = new Color(
                initialColor.r,
                initialColor.g,
                initialColor.b,
                initialColor.a * alpha.Evaluate(normalized)
            );
        }
    }
}