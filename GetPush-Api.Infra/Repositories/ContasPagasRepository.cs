using Dapper;
using GetPush_Api.Domain.Commands.Results;
using GetPush_Api.Domain.Entities;
using GetPush_Api.Domain.Repositories;
using GetPush_Api.Shared;
using System.Data.SqlClient;

namespace GetPush_Api.Infra.Repositories
{
    public class ContasPagasRepository : IContasPagasRepository
    {
        public Task DeleteContasPagas(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ContasPagarResult>> GetContasPagas(Usuario usuario)
        {
            var query = @"select id,
							     descricao,
							     tipoContasPagar_code,
							     data_pagamento,
							     valor,
							     usuario_id,
							     data_cadastro,
							     data_alterado,
							     usuario_id_cadastro
						    from contasPagas
                           where usuario_id = @Usuario_id";

            using (var conn = new SqlConnection(Runtime.ConnectionString))
            {
                await conn.OpenAsync();
                var result = await conn.QueryAsync(query, new { Usuario_id = usuario.id });

                var contasPagarList = new List<ContasPagarResult>();

                if (result != null)
                {
                    foreach (var item in result)
                    {
                        contasPagarList.Add(new ContasPagarResult
                        {
                            id = item.id,
                            descricao = item.descricao,
                            tipoContasPagar = new TipoContasPagar { code = item.tipoContasPagar_code },
                            data_pagamento = item.data_pagamento,
                            valor = item.valor,
                            usuario = new UsuarioResult { id = item.usuario_id },
                            data_cadastro = item.data_cadastro,
                            data_alterado = item.data_alterado,
                            usuarioCadastro = new UsuarioResult { id = item.usuario_id_cadastro }
                        });
                    }

                    return contasPagarList;
                }

                return new List<ContasPagarResult>();

            }
        }

        public Task InsertContasPagas(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public Task UpdateContasPagas(Usuario usuario)
        {
            throw new NotImplementedException();
        }
    }
}
