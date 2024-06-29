using GetPush_Api.Infra.Contexts;

namespace GetPush_Api.Infra.Transactions
{
    public class Uow : IUow
    {
        private readonly ProjetoAPICRMDataContext _context;

        public Uow(ProjetoAPICRMDataContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Rollback()
        {
            throw new NotImplementedException();
        }
    }
}
