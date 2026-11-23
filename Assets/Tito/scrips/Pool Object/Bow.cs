using UnityEngine;
namespace Tito
{

    public class Bow : Weapon
    {
        //[SerializeField] private ProyectilPool proyectilPool;
        [SerializeField]private Transform canon;

        public override void Use()
        {
            if (ProyectilPool.IntanceProyectilPool != null)
            {
                
            ProyectilPool.IntanceProyectilPool.GetProyectil(canon.position,canon.rotation);
            }
        }
    }
}
