using MMFramework.MMUI;
using UnityWeld.Binding;

[Binding]
public class TextWidget : WidgetBase
{
    private string _targetText;
    [Binding]
    public string TargetText
    {
        get => _targetText;
        set
        {
            _targetText = value;
            OnPropertyChanged(nameof(TargetText));
        }
    }
}
