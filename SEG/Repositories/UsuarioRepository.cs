using Microsoft.AspNetCore.Http.HttpResults;
using SEG.Context;
using SEG.Models;
using System.Security.Cryptography;
using System.Text;

namespace SEG.Repositories
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<bool> ValidarUsuarioLogin(string login, string senha)
        {
            senha = GerarHashMD5(senha);
            return await GetUnicoBoolAsync(u => u.Login == login && u.Senha == senha);
        }
        private string GerarHashMD5(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }


}
