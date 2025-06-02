namespace Ap.Core.Builders
{
    public class CompleteBuilder(StateSetBuilder setBuilder)
    {
        private readonly StateSetBuilder _setBuilder = setBuilder;
    }
}
