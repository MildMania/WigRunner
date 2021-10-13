namespace ProgressionSystem
{
    public abstract class LevelSuccessBase : ProgressionBase
    {
        public override EProgressionResult GetProgressionType()
        {
            return EProgressionResult.Success;
        }
    }
}