using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.UI;
namespace Tito
{

    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private int idScene;
        [SerializeField]private Sprite spriteRefScene;

       
        
        public void StartGame()
        {
            SceneLoadingManager.InstanceScene.LoadScene(idScene, spriteRefScene);
            
        }
        public void ExitGame()
        {
            
        }
    }
}
