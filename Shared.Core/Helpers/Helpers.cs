namespace Shared.Core.Helpers
{
    public class Helpers
    {
        public static string[] SplitStateFilter(string stateFilter)
        {
            var delimiter = "-";
            return stateFilter.Split(delimiter);
        }
    }
}
