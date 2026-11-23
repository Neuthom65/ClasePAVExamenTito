
using UnityEngine;
using System.Collections;

namespace Tito
{
    public class Proyectil : MonoBehaviour
    {
        [SerializeField] private float time = 1f;
        [SerializeField] private float velocity = 10f;

        private bool impact = false;

        private void OnEnable()
        {
            impact = false;
            StopAllCoroutines();
            StartCoroutine(WaitTime());
        }

        private void Update()
        {
            if (impact) return;

            transform.position += transform.forward * velocity * Time.deltaTime;
        }

        private IEnumerator WaitTime()
        {
            yield return new WaitForSeconds(time);

            DevolverProyectil();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (impact) return;

            // Buscar enemigo en el objeto impactado o sus padres
            Enemy enemigo = other.GetComponentInParent<Enemy>();

            if (enemigo != null)
            {
                impact = true;
                StopAllCoroutines();

                if (EfectPoolObject.IntanceEfectPool != null)
                {
                    EfectPoolObject.IntanceEfectPool.Reproducir(
                        transform.position,
                        Quaternion.identity
                    );
                }

                enemigo.RecibirImpacto();

                DevolverProyectil();
                return;
            }

            // Impacto contra el jugador
            if (other.CompareTag("Player"))
            {
                impact = true;
                StopAllCoroutines();

                if (EfectPoolSangre.IntanceSangrePool != null)
                {
                    EfectPoolSangre.IntanceSangrePool.Reproducir(
                        transform.position,
                        Quaternion.identity
                    );
                }

                DevolverProyectil();
            }
        }

        private void DevolverProyectil()
        {
            if (impact == false)
                impact = true;

            StopAllCoroutines();

            if (ProyectilPool.IntanceProyectilPool != null)
            {
                ProyectilPool.IntanceProyectilPool.ReturnProyectil(this);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        public virtual void OnTakenFromPool()
        {
            impact = false;
        }

        public virtual void Initialize()
        {
            impact = false;
        }

        public virtual void OnReturnedToPool()
        {
            impact = true;
            StopAllCoroutines();
        }
    }
}
