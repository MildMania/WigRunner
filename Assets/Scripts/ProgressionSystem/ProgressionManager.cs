using MMFramework.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ProgressionSystem
{
    public class ProgressionManager : Singleton<ProgressionManager>
    {
        public ProgressionBase ActiveProgression { get; private set; }

        public Action<EProgressionResult> OnProgressionUpdated { get; set; }

        private List<ProgressionBase> _progressionCheckers;

        private List<ProgressionBase> _ProgressionCheckers
        {
            get
            {
                if (_progressionCheckers == null)
                    _progressionCheckers = GetComponents<ProgressionBase>().ToList();

                return _progressionCheckers;
            }
        }

        public void CheckProgression()
        {
            if (ActiveProgression != null)
                return;

            ActiveProgression = _ProgressionCheckers
                .FirstOrDefault(val => val.CheckProgression());

            if (ActiveProgression == null)
                return;

            if (ActiveProgression.GetProgressionType() != EProgressionResult.None)
            {
                OnProgressionUpdated?.Invoke(ActiveProgression.GetProgressionType());
            }
        }

        private void Update()
        {
            CheckProgression();
        }
    }
}