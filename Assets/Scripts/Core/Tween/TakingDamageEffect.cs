using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class TakingDamageEffect : MonoBehaviour
    {
        SpriteMask mask;
        SpriteRenderer spriteRenderer;
        ParticleSystem particles;

        [SerializeField] float BlinkDuration;

        private void Awake()
        {
            if (mask == null)
                mask = GetComponent<SpriteMask>();
            if (spriteRenderer == null)
                spriteRenderer = transform.parent.GetComponent<SpriteRenderer>();
            if (particles == null)
                particles = GetComponent<ParticleSystem>();

            mask.enabled = false;
            Debug.Log("Press Alpha0 to blink damage effect");
        }
        float blinkTimer;

        private void Update()
        {
            mask.sprite = spriteRenderer.sprite;

            if (Input.GetKeyDown(KeyCode.Alpha0))
                Blink();

            RunBlinkTimer();
        }

        private void RunBlinkTimer()
        {
            if (blinkTimer > 0)
            {
                blinkTimer -= Time.deltaTime;
                mask.enabled = true;
            }
            else
                mask.enabled = false;
        }

        public void Blink()
        {
            blinkTimer = BlinkDuration;
            particles.Play();
        }
    }
}
