using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace Tito
{
    public class SceneLoadingManager : MonoBehaviour
    {
        public  static SceneLoadingManager InstanceScene {get ; private set;}
        private bool isLoading = false;
         [SerializeField]private Image imgLoad;
        [SerializeField]private GameObject refGameObPnl;


        private void Awake()
        {
            if (InstanceScene == null)
            {
                InstanceScene = this;
                DontDestroyOnLoad(this.gameObject);
                return;
            }
            Destroy(this.gameObject);
        }
        public void LoadScene(int idScene, Sprite sprite)
        {
            if(isLoading) return;
            StartCoroutine(LoadSceneAsync(idScene, sprite));
        }

        private IEnumerator LoadSceneAsync(int idScene, Sprite sprite)
        {
            isLoading = true;
            refGameObPnl.SetActive(true);
            imgLoad.sprite = sprite;
            AsyncOperation loading = SceneManager.LoadSceneAsync(idScene);

            while (!loading.isDone)
            {
                Debug.Log("Cargando..." + (loading.progress *100));
                yield return null;
            }
            Debug.Log("Escena completamente cargada");
            refGameObPnl.SetActive(false);
            isLoading = false;
        }
    }

}
