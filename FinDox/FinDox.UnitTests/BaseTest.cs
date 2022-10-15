namespace FinDox.UnitTests
{
    public abstract class BaseTest<T> where T : class
    {
        public BaseTest()
        {
            SetUp();
            InstanceUnderTest = CreateInstanceUnderTest();
        }

        public T InstanceUnderTest { get; }

        public abstract T CreateInstanceUnderTest();

        public abstract void SetUp();
    }
}