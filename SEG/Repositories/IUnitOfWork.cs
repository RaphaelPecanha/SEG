namespace SEG.Repositories;

public interface IUnitOfWork
{
    IUsuarioRepository UsuarioRepository { get; }
    Task CommitAsync();
}
