using TMPro;
using UnityEngine;
namespace Tito
{
public class EnemyGamingManager : MonoBehaviour
    {
        private int EnemigoModiros;

        public static EnemyGamingManager Instance { get; private set; }

        public TextMeshProUGUI textoEnemy;


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


        public void DeadEnemy()
        {
            EnemigoModiros++;
            ActualizarUI();
        }
       

        private void ActualizarUI()
        {
            if (textoEnemy != null)
                textoEnemy.text = EnemigoModiros.ToString();

        }
    }
}