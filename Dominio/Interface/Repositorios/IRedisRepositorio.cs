using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interface.Repositorios
{
    public interface IRedisRepositorio
    {
        Task GravarChave(string chave, string valor, int segundos = 600);
        Task<string> RecuperarChave(string chave);
    }
}
