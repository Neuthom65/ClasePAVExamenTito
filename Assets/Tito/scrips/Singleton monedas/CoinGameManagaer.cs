using TMPro;
using Unity.VisualScripting;
using UnityEngine;
namespace Tito
{

    public class CoinGameManagaer : MonoBehaviour
    {
        private int monedasOro;
        private int monedasPlata;
        private int trofeos;

        public static CoinGameManagaer Instance { get; private set; }

        public TextMeshProUGUI textoOro;
        public TextMeshProUGUI textoPlata;
        public TextMeshProUGUI textoTrofeos;


        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
                return;
            }
            Destroy(this.gameObject);
        }

        private void Start()
        {
            ActualizarUI();
        }


        public void CollectGoldCoin()
        {
            monedasOro++;
            ActualizarUI();
        }
        public void CollectSilverCoin()
        {
            monedasPlata++;
            ActualizarUI();

            if (monedasPlata >= 3)
            {
                monedasOro++;
                monedasPlata = 0;
                ActualizarUI();

            }
        }
        public void CollectTrophy()
        {
            trofeos++;
            ActualizarUI();

        }

        private void ActualizarUI()
        {
            if (textoOro != null)
                textoOro.text = monedasOro.ToString();

            if (textoPlata != null)
                textoPlata.text = monedasPlata.ToString();

            if (textoTrofeos != null)
                textoTrofeos.text = trofeos.ToString();
        }
    }

}