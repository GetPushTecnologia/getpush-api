using Dapper;
using GetPush_Api.Domain.Commands.Results;
using GetPush_Api.Domain.Entities;
using GetPush_Api.Domain.Repositories;
using GetPush_Api.Shared;
using System.Data.SqlClient;

namespace GetPush_Api.Infra.Repositories
{
    public class GraficoRepository : IGraficoRepository
    {
        public async Task<DadosGraficoResult> GetDadosGraficoResumido(Usuario usuario)
        {
            var query = @"select u.id as usuario_id, 
	                             sum(cp.valor_pago) as totalValorPago,
	                             sum(vr.valor_recebido) as totalValorRecebido
                            from usuario u
                            left join ContaPaga cp on cp.usuario_id = u.id
                            left join ValorRecebido vr on vr.usuario_id = u.id
                           where u.id = @Id
                        group by u.id";

            using (var conn = new SqlConnection(Runtime.ConnectionString))
            {
                await conn.OpenAsync();
                var result = await conn.QueryFirstOrDefaultAsync(query, new { Id = usuario.id });

                if (result != null)
                {
                    return new DadosGraficoResult
                    {
                        usuario = new UsuarioResult { id = result.usuario_id },
                        totalContaPaga = result.totalValorPago,
                        totalValorRecebido = result.totalValorRecebido
                    };
                }
            }

            return new DadosGraficoResult();
        }
    }
}
