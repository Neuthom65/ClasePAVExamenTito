
using UnityEngine;
using System.Collections;

namespace Tito
{
    public class Enemy : MonoBehaviour
    {
        [Header("Sonido")]
        [SerializeField] private AudioClip sonidoHit;

        private AudioSource audioSource;

        private EnemyType enemyType;
        private Enemypool pool;

        private bool returning;

        public EnemyType EnemyType => enemyType;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();

            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }

            audioSource.playOnAwake = false;
        }

        public void Initialize(
            EnemyType type,
            Enemypool pool,
            Vector3 position,
            Quaternion rotation)
        {
            this.enemyType = type;
            this.pool = pool;

            returning = false;

            transform.SetPositionAndRotation(position, rotation);

            gameObject.SetActive(true);
        }

        private void OnEnable()
        {
            returning = false;
        }

        public void RecibirImpacto()
        {
            if (returning) return;

            returning = true;

            Debug.Log("Enemigo eliminado: " + gameObject.name);

            if (EnemyGamingManager.Instance != null)
            {
                EnemyGamingManager.Instance.DeadEnemy();
            }

            StartCoroutine(Morir());
        }

        private IEnumerator Morir()
        {
            if (sonidoHit != null)
            {
                audioSource.PlayOneShot(sonidoHit);

                yield return new WaitForSeconds(sonidoHit.length);
            }

            if (pool != null)
            {
                pool.ReturnEnemy(this);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}
