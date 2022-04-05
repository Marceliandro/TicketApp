namespace TicketApp.Base.Interfaces
{
    public interface IServicoBase<TEntity, TSalvar, TEditar, TId>
    {
        TEntity GetById(TId id);
        TEntity Salvar(TSalvar dto);
        TEntity Editar(TEditar dto);
        void Excluir(TId id);
    }
}
