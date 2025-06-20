using WebApiProj1.ActingDB;

namespace WebApiProj1.Repositories.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DumDb _dbContext;
        public IUserRepository UserRepository { get; private set; }

        public UnitOfWork(DumDb dbContext)
        {
            _dbContext = dbContext;
            UserRepository = new UserRepository(dbContext);
        }

        public void SaveChanges()
        {
            // eta chai if real db use gareko xa vaye sabai ko changes ekaichoti save garna ko lagi logice lekhne ho
        }
    }
}
