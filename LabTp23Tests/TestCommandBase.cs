using LabTp23.DAL;

namespace LabTp23Tests;

public abstract class TestCommandBase : IDisposable
    {
        protected readonly ShopDbContext Context;

        public TestCommandBase()
        {
            Context = ShopDbContextFactory.Create();
        }

        public void Dispose()
        {
            ShopDbContextFactory.Destroy(Context);
        }
    }