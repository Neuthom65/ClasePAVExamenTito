using UnityEngine;
namespace Tito
{
    public class Collectable : MonoBehaviour
    {
        public enum ItemType { Oro, Plata, Trofeo }

        public ItemType tipoDeObjeto;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                switch (tipoDeObjeto)
                {
                    case ItemType.Oro:
                        CoinGameManagaer.Instance.CollectGoldCoin();
                        break;
                    case ItemType.Plata:
                        CoinGameManagaer.Instance.CollectSilverCoin();
                        break;
                    case ItemType.Trofeo:
                        CoinGameManagaer.Instance.CollectTrophy();
                        break;
                }
                Destroy(gameObject);
            }
        }
    }
}