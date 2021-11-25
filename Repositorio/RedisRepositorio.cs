using Dominio.Entidade.Configuracoes;
using Dominio.Interface.Repositorios;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace Repositorio
{
    public class RedisRepositorio : IRedisRepositorio
    {
        private readonly RedisConfig config;
        public RedisRepositorio(IOptions<RedisConfig> configuration)
        {
            this.config = configuration.Value;
        }

        public async Task GravarChave(string chave, string valor, int segundos = 600)
        {
            ConnectionMultiplexer connectionRedis = ConnectionMultiplexer.Connect(config.ConnectionString);

            IDatabase clientRedis = connectionRedis.GetDatabase();

            await clientRedis.StringSetAsync(chave, valor, TimeSpan.FromSeconds(segundos));
        }

        public async Task<string> RecuperarChave(string chave)
        {
            ConnectionMultiplexer connectionRedis = ConnectionMultiplexer.Connect(config.ConnectionString);

            IDatabase clientRedis = connectionRedis.GetDatabase();

            return await clientRedis.StringGetAsync(chave);
        }
    }
}
