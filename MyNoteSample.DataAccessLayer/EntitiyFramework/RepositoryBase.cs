

namespace MyNoteSample.DataAccessLayer.EntityFramework
{
    public class RepositoryBase
    {
        private protected static DatabaseContext db;
        private static object _lockSycn = new object();
        public RepositoryBase()
        {
            CreateContext();
        }
        //Single Pattern
        private static void CreateContext()
        {
            if (db == null)
            {
                lock (_lockSycn)
                {
                    if (db == null)
                    {
                        db = new DatabaseContext();

                    }
                }
            }
        }
    }
}
