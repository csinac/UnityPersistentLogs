using UnityEngine;
using UnityEngine.UI;

namespace SinaC.PersistentLogs
{
    public class LogListenerBehaviour : MonoBehaviour
    {
        [SerializeField] private Text output;
        private float _t = 0;
        void Start()
        {
            LogListener.Start();
            output.text = string.Join("\n",new[] {
                LogListener.PID.ToString(),
                LogListener.LogLocation,
                LogListener.OutputLocation
            });
        }

        void Update()
        {
            _t += Time.deltaTime;
            if (_t > 1) {
                Debug.Log(Time.time);
                _t = 0;
            }
        }
    }
}
