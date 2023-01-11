using System.Diagnostics;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

namespace SinaC.PersistentLogs
{
    public class ProcessListener : MonoBehaviour
    {
        [SerializeField] private Text output;
        
        private int pid;
        private string logLocation;
        private string scriptPath;
        private string outputLocation;
        
        void Start() {
#if UNITY_STANDALONE_WIN
            scriptPath = Path.Combine(Application.streamingAssetsPath, "processListener.bat");
            outputLocation = Path.Combine(Directory.GetParent(Application.dataPath).ToString(), "Logs");
            logLocation = Application.persistentDataPath;
            pid = Process.GetCurrentProcess().Id;

            scriptPath = scriptPath.Replace("/", "\\");
            outputLocation = outputLocation.Replace("/", "\\");
            logLocation = logLocation.Replace("/", "\\");
            
            Debug.Log(logLocation);
            Debug.Log(scriptPath);
            Debug.Log(outputLocation);
            Debug.Log(pid);
#else
            Debug.LogWarning("Works only on Windows yet");
            Destroy(this);
            return;
#endif

            if (!File.Exists(scriptPath)) {
                Destroy(this);
                return;
            }
            
            output.text = string.Join("\n",new[] {
                pid.ToString(),
                logLocation,
                outputLocation
            });

            if (!Directory.Exists(outputLocation)) {
                Directory.CreateDirectory(outputLocation);
            }

            if (Application.isEditor)
                return;
            
#if UNITY_STANDALONE_WIN
            RunBatchScript();
#endif
        }
        
#if UNITY_STANDALONE_WIN
        private void RunBatchScript() {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = scriptPath;
            startInfo.Arguments = $"{pid} \"{logLocation}\" Player log \"{outputLocation}\"";
            Process.Start(startInfo);

            output.text = $"{output.text}\n{startInfo.Arguments}";
        }
#endif
    }
}
