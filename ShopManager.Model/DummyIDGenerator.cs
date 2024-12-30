namespace ShopManager.Controller
{
    /// <summary>
    /// Gets a dummy ID which will be overwritten by the database and is only here to help us when caching operations.
    /// </summary>
    internal static class DummyIDGenerator
    {
        private static long _lastID = long.MaxValue;

        /// <summary>
        /// Returns a dummy long that is very close to long.MaxValue, but decrements at each call of this function.
        /// </summary>
        /// <returns></returns>
        internal static long GetDummyID()
        {
            long id = _lastID;
            _lastID--;
            return id;
        }
    }
}
