
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Unity.Mathematics;

namespace Tito
{
    public class ProyectilPool : MonoBehaviour
    {
        [Header("Pool")]
        [SerializeField] private GameObject proyectilPrefab;
        [SerializeField] private int defaultCapacity = 5;
        [SerializeField] private int maxSize = 8;

        [Header("Disparo")]
        [SerializeField] private AudioClip sonidoDisparo;
        [SerializeField] private float retrasoDisparo = 2f;

        private AudioSource audioSource;
        private float siguienteDisparo = 0f;

        private ObjectPool<Proyectil> pool;

        public static ProyectilPool IntanceProyectilPool { private set; get; }

        private void Awake()
        {
            if (IntanceProyectilPool == null)
            {
                IntanceProyectilPool = this;

                audioSource = GetComponent<AudioSource>();

                if (audioSource == null)
                {
                    audioSource = gameObject.AddComponent<AudioSource>();
                }

                pool = CreatePool();

                List<Proyectil> proyectilesIniciales = new List<Proyectil>();

                for (int i = 0; i < defaultCapacity; i++)
                {
                    Proyectil proyectil = pool.Get();
                    proyectilesIniciales.Add(proyectil);
                }

                foreach (Proyectil proyectil in proyectilesIniciales)
                {
                    pool.Release(proyectil);
                }

                return;
            }

            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            if (IntanceProyectilPool == this)
            {
                IntanceProyectilPool = null;
            }
        }

        private ObjectPool<Proyectil> CreatePool()
        {
            return new ObjectPool<Proyectil>
            (
                createFunc: () =>
                {
                    GameObject proyectilGO = Instantiate(proyectilPrefab, transform);

                    if (!proyectilGO.TryGetComponent(out Proyectil proyectil))
                    {
                        proyectil = proyectilGO.AddComponent<Proyectil>();
                    }

                    return proyectil;
                },

                actionOnGet: (proyectil) =>
                {
                    proyectil.gameObject.SetActive(true);
                },

                actionOnRelease: (proyectil) =>
                {
                    proyectil.gameObject.SetActive(false);
                },

                actionOnDestroy: (proyectil) =>
                {
                    if (proyectil != null)
                    {
                        Destroy(proyectil.gameObject);
                    }
                },

                collectionCheck: true,
                defaultCapacity: defaultCapacity,
                maxSize: maxSize
            );
        }

        public Proyectil GetProyectil(Vector3 pos, quaternion rot)
        {
            if (Time.time < siguienteDisparo)
            {
                return null;
            }

            if (pool.CountActive >= maxSize)
            {
                Debug.Log("Se alcanzo el limite de proyectiles");
                return null;
            }

            siguienteDisparo = Time.time + retrasoDisparo;

            Proyectil proyectil = pool.Get();

            proyectil.transform.SetPositionAndRotation(pos, rot);

            if (sonidoDisparo != null)
            {
                audioSource.PlayOneShot(sonidoDisparo);
            }

            return proyectil;
        }

        public void ReturnProyectil(Proyectil proyectil)
        {
            if (proyectil != null)
            {
                pool.Release(proyectil);
            }
        }
    }
}
