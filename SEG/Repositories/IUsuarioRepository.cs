using SEG.Models;

namespace SEG.Repositories;

public interface IUsuarioRepository : IRepository<Usuario>
{
    Task<bool> ValidarUsuarioLogin(string login, string senha);
}
