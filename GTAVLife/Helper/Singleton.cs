using System;

namespace GTAVLife.Helper
{
    public abstract class SimpleSingletonBase<T>
    {
        private static readonly Lazy<T> instance = new Lazy<T>(InstanceCreator);

        public static T Instance => instance.Value;

        private static T InstanceCreator()
        {
            return (T)Activator.CreateInstance(typeof(T), true);
        }
    }
}