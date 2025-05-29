namespace Ap.Nodes
{
    public class LineBuilderProvider
    {
        /// <summary>
        /// with start node
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public LineBuilder Create(string state)
        {
            return new LineBuilder(state);
        }
    }
}
