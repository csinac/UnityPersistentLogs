using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace SinaC.PersistentLogs
{
    public class ProcessListener : MonoBehaviour
    {
        [SerializeField] private Text output;
        
        private int pid;
        private string processName;
        
        void Start() {
            pid = Process.GetCurrentProcess().Id;
            processName = Process.GetCurrentProcess().ProcessName;
            
            output.text = string.Join("\n",new[] {
                pid.ToString(),
                processName
            });
        }
    }
}
