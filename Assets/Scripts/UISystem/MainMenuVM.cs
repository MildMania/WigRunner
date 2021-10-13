using MMFramework.MMUI;
using UnityEngine;
using UnityWeld.Binding;

[Binding]
public class MainMenuVM : VMBase
{
    [SerializeField] private ButtonWidget _bgButtonWidget = null;
    [SubWidget] public ButtonWidget BGButtonWidget => _bgButtonWidget;

    [SerializeField] private ButtonWidget _tapToStartWidget = null;
    [SubWidget] public ButtonWidget TapToStartWidget => _tapToStartWidget;

    private MainMenuPhase _mainMenuPhase;
    public MainMenuPhase MainMenuPhase { get => _mainMenuPhase; set => _mainMenuPhase = value; }

    #region Singleton

    private static MainMenuVM _instance;

    public static MainMenuVM Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<MainMenuVM>();

            return _instance;
        }
    }

    #endregion Singleton

    protected override void AwakeCustomActions()
    {
        if (PhaseTracker.Instance.CurrentPhase is MainMenuPhase mainMenuPhase)
        {
            _mainMenuPhase = mainMenuPhase;
        }

        RegisterToPhases();

        base.AwakeCustomActions();
    }

    protected override void OnDestroyCustomActions()
    {
        UnregisterFromPhases();

        base.OnDestroyCustomActions();
    }

    protected override void ActivatedCustomActions()
    {
        RegisterToButtonClicks();

        base.ActivatedCustomActions();
    }

    protected override void DeactivatedCustomActions()
    {
        UnregisterFromButtonClicks();

        base.DeactivatedCustomActions();
    }

    private void RegisterToPhases()
    {
        PhaseBaseNode.OnTraverseStarted_Static += OnPhaseStarted;
        PhaseBaseNode.OnTraverseFinished_Static += OnPhaseFinished;
    }

    private void UnregisterFromPhases()
    {
        PhaseBaseNode.OnTraverseStarted_Static -= OnPhaseStarted;
        PhaseBaseNode.OnTraverseFinished_Static -= OnPhaseFinished;
    }

    private void OnPhaseStarted(PhaseBaseNode phase)
    {
        if (phase is MainMenuPhase menuPhase)
        {
            _mainMenuPhase = menuPhase;

            TryActivate();
        }
    }

    private void OnPhaseFinished(PhaseBaseNode phase)
    {
        if (phase is MainMenuPhase)
        {
            TryDeactivate();
        }
    }

    private void RegisterToButtonClicks()
    {
        BGButtonWidget.Button.onClick.AddListener(OnTapToStartClicked);
        TapToStartWidget.Button.onClick.AddListener(OnTapToStartClicked);
    }

    private void UnregisterFromButtonClicks()
    {
        BGButtonWidget.Button.onClick.RemoveListener(OnTapToStartClicked);
        TapToStartWidget.Button.onClick.RemoveListener(OnTapToStartClicked);
    }

    private void OnTapToStartClicked()
    {
        _mainMenuPhase.CompletePhase();
    }
}