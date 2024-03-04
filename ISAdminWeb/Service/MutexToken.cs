namespace ISAdminWeb.Service
{
    public static class MutexToken
    {
        public static Mutex Mutex { get; } = new Mutex();
    }
}
