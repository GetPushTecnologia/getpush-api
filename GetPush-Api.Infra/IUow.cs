namespace GetPush_Api.Infra
{
    public interface IUow
    {
        void Commit();
        void Rollback();
    }
}
