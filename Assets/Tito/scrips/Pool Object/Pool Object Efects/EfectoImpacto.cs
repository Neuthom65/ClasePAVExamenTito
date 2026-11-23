using UnityEngine;
using UnityEngine.Pool;

namespace Tito
{
    public class EfectoImpacto : MonoBehaviour
    {
        [SerializeField] private float duracion = 1f;

        private IObjectPool<EfectoImpacto> pool;

        public void SetPool(IObjectPool<EfectoImpacto> pool) => this.pool = pool;

        private void OnEnable()
        {
            if (TryGetComponent(out ParticleSystem ps)) ps.Play();
            Invoke(nameof(Devolver), duracion);
        }

        private void Devolver() => pool.Release(this);

        private void OnDisable() => CancelInvoke();
    }
}