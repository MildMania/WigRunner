using UnityEngine;

public class JointSettingsManager : MonoBehaviour
{
    static JointSettingsManager _instance;
    public static JointSettingsManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<JointSettingsManager>();

            return _instance;
        }
    }

    public StickableJointSettings JointSettings;
}
