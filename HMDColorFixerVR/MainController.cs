using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BOLL7708;
using Valve.VR;

namespace HMDColorFixerVR
{
    class MainController
    {
        public enum Color
        {
            R,
            G,
            B
        }

        EasyOpenVRSingleton _vr = EasyOpenVRSingleton.Instance;
        ConcurrentDictionary<Color, float> _values = new ConcurrentDictionary<Color, float>();
        volatile bool _changed = false;

        public MainController()
        {
            _vr.Init();
            InitWorkerThread();
        }

        public void SetColorValue(Color color, float value)
        {
            _values[color] = value;
            _changed = true;
        }

        private Thread _workerThread = null;
        public void InitWorkerThread()
        {
            _workerThread = new Thread(Worker);
            if (!_workerThread.IsAlive) _workerThread.Start();
        }

        private void Worker()
        {
            Thread.CurrentThread.IsBackground = true;
            while (true)
            {
                Thread.Sleep(100);
                if (_vr.IsInitialized() && _changed)
                {
                    _changed = false;
                    if (_values.ContainsKey(Color.R)) _vr.SetFloatSetting(OpenVR.k_pch_SteamVR_Section, OpenVR.k_pch_SteamVR_HmdDisplayColorGainR_Float, _values[Color.R]);
                    if (_values.ContainsKey(Color.G)) _vr.SetFloatSetting(OpenVR.k_pch_SteamVR_Section, OpenVR.k_pch_SteamVR_HmdDisplayColorGainG_Float, _values[Color.G]);
                    if (_values.ContainsKey(Color.B)) _vr.SetFloatSetting(OpenVR.k_pch_SteamVR_Section, OpenVR.k_pch_SteamVR_HmdDisplayColorGainB_Float, _values[Color.B]);
                }
            }
        }
    }
}
