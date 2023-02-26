using System.Diagnostics;
using System.IO;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace SinaC.PersistentLogs
{
    public static class LogListener
    {
        private static int _pid;
        private static string _logLocation;
        private static string _scriptPath;
        private static string _outputLocation;

        public static int PID => _pid;
        public static string LogLocation => _logLocation;
        public static string ScriptPath => _scriptPath;
        public static string OutputLocation => _outputLocation;
        
        public static void Start() {
#if UNITY_STANDALONE_WIN
            InitialiseWin();
#else
            UnityEngine.Debug.LogWarning("Works only on Windows yet");
            return;
#endif

            if (!File.Exists(_scriptPath)) {
                return;
            }
            
            if (!Directory.Exists(_outputLocation)) {
                Directory.CreateDirectory(_outputLocation);
            }

            if (Application.isEditor)
                return;
            
#if UNITY_STANDALONE_WIN
            RunBatchScript();
#endif
        }
        
#if UNITY_STANDALONE_WIN
        private static void InitialiseWin() {
            _scriptPath = Path.Combine(Application.streamingAssetsPath, "processListener.bat");
            _outputLocation = Path.Combine(Directory.GetParent(Application.dataPath).ToString(), "Logs");
            _logLocation = Application.persistentDataPath;
            _pid = Process.GetCurrentProcess().Id;

            _scriptPath = _scriptPath.Replace("/", "\\");
            _outputLocation = _outputLocation.Replace("/", "\\");
            _logLocation = _logLocation.Replace("/", "\\");
            
            Debug.Log(_logLocation);
            Debug.Log(_scriptPath);
            Debug.Log(_outputLocation);
            Debug.Log(_pid);
        }
        
        private static void RunBatchScript() {
            string arguments = $"{_pid} \"{_logLocation}\" Player log \"{_outputLocation}\"";
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = _scriptPath;
            startInfo.Arguments = arguments;
            Process.Start(startInfo);
            
            Debug.Log($"Arguments: {arguments}");
        }
#endif
    }
}
