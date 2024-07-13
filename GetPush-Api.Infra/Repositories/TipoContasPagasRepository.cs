﻿using Dapper;
using GetPush_Api.Domain.Commands.Results;
using GetPush_Api.Domain.Entities;
using GetPush_Api.Domain.Repositories;
using GetPush_Api.Shared;
using System.Data.SqlClient;

namespace GetPush_Api.Infra.Repositories
{
    public class TipoContasPagasRepository : ITipoContasPagasRepository
    {
        public async Task DeleteContasPagas(Guid contasPagasId)
        {
            using (var conn = new SqlConnection(Runtime.ConnectionString))
            {
                var query = @"delete 
                                from tipoContasPagas 
                               where id = @Id";

                var parameters = new { Id = contasPagasId };

                await conn.ExecuteAsync(query, parameters);
            }
        }

        public async Task<IEnumerable<TipoContaPagaResult>> GetTipoContaPagas()
        {
            var query = @"select tcp.id,
                                 tcp.code,
                                 tcp.descricao,
                                 tcp.data_cadastro,
                                 tcp.data_alterado,
                                 tcp.usuario_id_cadastro,
                                 u.nome
						    from tipoContasPagas tcp
                           inner join usuario u on u.id = tcp.usuario_id_cadastro";

            using (var conn = new SqlConnection(Runtime.ConnectionString))
            {
                await conn.OpenAsync();
                var result = await conn.QueryAsync(query);

                var tipoContasPagarList = new List<TipoContaPagaResult>();

                if (result != null)
                {
                    foreach (var item in result)
                    {
                        tipoContasPagarList.Add(new TipoContaPagaResult
                        {
                            id = item.id,
                            code = item.code,
                            descricao = item.descricao,
                            data_cadastro = item.data_cadastro,
                            data_alterado = item.data_alterado,
                            usuarioCadastro = new UsuarioResult { 
                                id = item.usuario_id_cadastro,
                                nome = item.nome,
                            }
                        });
                    }

                    return tipoContasPagarList;
                }

                return new List<TipoContaPagaResult>();
            }
        }

        public async Task InsertContasPagas(TipoContaPaga tipoContasPagas)
        {
            using (var conn = new SqlConnection(Runtime.ConnectionString))
            {
                var query = @"insert 
                                into contasPagas 
                                    (id,
                                     code,
                                     descricao,
                                     data_cadastro,
                                     data_alterado,
                                     usuario_id_cadastro)
                             values (newid(),
                                     @Code,
                                     @Descricao,
	                                 @Data_cadastro,
	                                 @Data_alterado,
	                                 @Usuario_id_cadastro)";

                var parameters = new
                {
                    Code = tipoContasPagas.code,
                    Descricao = tipoContasPagas.descricao,
                    Data_cadastro = tipoContasPagas.data_cadastro,
                    Data_alterado = tipoContasPagas.data_alterado,
                    Usuario_id_cadastro = tipoContasPagas.usuarioCadastro.id
                };

                await conn.ExecuteAsync(query, parameters);
            }
        }

        public async Task UpdateContasPagas(TipoContaPaga tipoContasPagas)
        {
            using (var conn = new SqlConnection(Runtime.ConnectionString))
            {
                var query = @"update tipoContasPagas 
                                 set code = @Code,
                                     descricao = @Descricao,
                                     data_alterado = @Data_alterado,
                                     usuario_id_cadastro = @Usuario_id_cadastro
                               where id = @Id";

                var parameters = new
                {
                    Id = tipoContasPagas.id,
                    Descricao = tipoContasPagas.descricao,
                    Data_alterado = tipoContasPagas.data_alterado,
                    Usuario_id_cadastro = tipoContasPagas.usuarioCadastro.id
                };

                await conn.ExecuteAsync(query, parameters);
            }
        }
    }
}
