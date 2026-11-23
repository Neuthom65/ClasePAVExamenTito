using UnityEngine;
namespace Tito
{

    public interface IEnemyState
    {
        //Cuando el procerso entra, se actualiza y sale
        void Enter();
        void Update();
        void Exit();


    }
}
