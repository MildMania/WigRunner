public class NpcAnimationController : BaseAnimationController<NpcAnimationController.EAnimation>
{
    public enum EAnimation
    {
        None = 0,
        Idle = 1,
        LowJoy = 2,
        MiddleJoy = 3,
        HighJoy = 4
    }
}
