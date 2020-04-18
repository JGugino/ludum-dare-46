using UnityEngine;

namespace GameManagement
{
    public class DDOL : MonoBehaviour
    {
        void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
