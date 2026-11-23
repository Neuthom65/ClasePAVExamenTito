using UnityEngine;
using UnityEngine.Pool;

namespace Tito
{
    public class EfectPoolSangre : MonoBehaviour
    {
        [SerializeField] private EfectoImpacto sangrePrefab;
        [SerializeField] private int capacity = 4;
        [SerializeField] private int size = 8;

        private ObjectPool<EfectoImpacto> sangrePool;

        public static EfectPoolSangre IntanceSangrePool { private set; get; }

        private void Awake()
        {
            if (IntanceSangrePool == null)
            {
                IntanceSangrePool = this;
                sangrePool = CrearPool();
                return;
            }
            Destroy(gameObject);
        }

        private ObjectPool<EfectoImpacto> CrearPool()
        {
            return new ObjectPool<EfectoImpacto>(
                createFunc: () =>
                {
                    EfectoImpacto efecto = Instantiate(sangrePrefab, transform);
                    efecto.SetPool(sangrePool);
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
            EfectoImpacto efecto = sangrePool.Get();
            efecto.transform.SetPositionAndRotation(pos, rot);
        }
    }
}