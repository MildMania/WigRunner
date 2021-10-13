namespace ProgressionSystem
{
    public abstract class LevelFailureBase : ProgressionBase
    {
        public override EProgressionResult GetProgressionType()
        {
            return EProgressionResult.Failure;
        }
    }
}