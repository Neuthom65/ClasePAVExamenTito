using UnityEngine;
namespace Tito
{

    public abstract class Weapon : MonoBehaviour
    {
        //Permite que cada arma se use a su modo
        public abstract void Use();
    }
}
