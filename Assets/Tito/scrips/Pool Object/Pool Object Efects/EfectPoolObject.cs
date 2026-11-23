using UnityEngine;
using UnityEngine.Pool;

namespace Tito
{
    public class EfectPoolObject : MonoBehaviour
    {
        [SerializeField] private EfectoImpacto efectPrefab;
        [SerializeField] private int capacity = 4;
        [SerializeField] private int size = 8;

        private ObjectPool<EfectoImpacto> efectPool;

        public static EfectPoolObject IntanceEfectPool { private set; get; }

        private void Awake()
        {
            if (IntanceEfectPool == null)
            {
                IntanceEfectPool = this;
                efectPool = CrearPool();
                return;
            }
            Destroy(gameObject);
        }

        private ObjectPool<EfectoImpacto> CrearPool()
        {
            return new ObjectPool<EfectoImpacto>(
                createFunc: () =>
                {
                    EfectoImpacto efecto = Instantiate(efectPrefab, transform);
                    efecto.SetPool(efectPool);
                    return efecto;
                },
                actionOnGet: efecto => efecto.gameObject.SetActive(true),
                actionOnRelease: efecto => efecto.gameObject.SetActive(false),
                actionOnDestroy: efecto => Destroy(efecto.gameObject),
                collectionCheck: false,
                defaultCapacity: capacity,
                maxSize: size
            );
        }

        public void Reproducir(Vector3 pos, Quaternion rot)
        {
            EfectoImpacto efecto = efectPool.Get();
            efecto.transform.SetPositionAndRotation(pos, rot);
        }
    }
}