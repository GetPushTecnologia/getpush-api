using Dapper;
using GetPush_Api.Domain.Commands.Results;
using GetPush_Api.Domain.Entities;
using GetPush_Api.Domain.Repositories;
using GetPush_Api.Shared;
using System.Data.SqlClient;

namespace GetPush_Api.Infra.Repositories
{
    public class ContaPagaRepository : IContaPagaRepository
    {
        public async Task DeleteContaPaga(Guid contaPagaId)
        {
            using (var conn = new SqlConnection(Runtime.ConnectionString))
            {
                var query = @"delete 
                                from ContaPaga
                               where id = @Id";

                var parameters = new { Id = contaPagaId };

                await conn.ExecuteAsync(query, parameters);
            }
        }

        public async Task<IEnumerable<ContaPagaResult>> GetContaPaga(Usuario usuario, IEnumerable<TipoContaPagaResult> tipoContaPagaList)
        {
            var query = @"select cp.id,
							     cp.descricao,
							     cp.tipoContaPaga_code,
							     cp.data_pagamento,
							     cp.valor_pago,
							     cp.usuario_id,
                                 u.nome,
							     cp.data_cadastro,
							     cp.data_alterado,
							     cp.usuario_id_cadastro,
                                 uc.nome as nomeCadastro
						    from ContaPaga cp
                      inner join Usuario u on u.id = cp.usuario_id
                      inner join Usuario uc on uc.id = cp.usuario_id_cadastro
                           where usuario_id = @Usuario_id";

            using (var conn = new SqlConnection(Runtime.ConnectionString))
            {
                await conn.OpenAsync();
                var result = await conn.QueryAsync(query, new { Usuario_id = usuario.id });

                var contasPagarList = new List<ContaPagaResult>();

                if (result != null)
                {
                    foreach (var item in result)
                    {
                        var tipoContaPaga = tipoContaPagaList.Where(x => x.code == item.tipoContaPaga_code).FirstOrDefault();

                        contasPagarList.Add(new ContaPagaResult
                        {
                            id = item.id,
                            descricao = item.descricao,
                            tipoContaPaga = tipoContaPaga != null ? tipoContaPaga : new TipoContaPagaResult(),
                            data_pagamento = item.data_pagamento,
                            valor_pago = item.valor_pago,
                            usuario = new UsuarioResult
                            {
                                id = item.usuario_id,
                                nome = item.nome
                            },
                            data_cadastro = item.data_cadastro,
                            data_alterado = item.data_alterado,
                            usuarioCadastro = new UsuarioResult
                            {
                                id = item.usuario_id_cadastro,
                                nome = item.nomeCadastro
                            }
                        });
                    }

                    return contasPagarList;
                }

                return new List<ContaPagaResult>();
            }
        }

        public async Task InsertContaPaga(ContaPaga contasPagas)
        {
            using (var conn = new SqlConnection(Runtime.ConnectionString))
            {
                var query = @"insert 
                                into ContaPaga
                                    (id,
	                                 descricao,
	                                 tipoContaPaga_code,
	                                 data_pagamento,
	                                 valor,
	                                 usuario_id,
	                                 data_cadastro,
	                                 data_alterado,
	                                 usuario_id_cadastro)
                             values (newid(),
                                     @Descricao,
	                                 @TipoContaPaga_code,
	                                 @Data_pagamento,
	                                 @Valor,
	                                 @Usuario_id,
	                                 @Data_cadastro,
	                                 @Data_alterado,
	                                 @Usuario_id_cadastro)";

                var parameters = new
                {
                    Descricao = contasPagas.descricao,
                    tipoContaPaga_code = contasPagas.tipoContaPaga.code,
                    Data_pagamento = contasPagas.data_pagamento,
                    Valor = contasPagas.valor,
                    Usuario_id = contasPagas.usuario.id,
                    Data_cadastro = contasPagas.data_cadastro,
                    Data_alterado = contasPagas.data_alterado,
                    Usuario_id_cadastro = contasPagas.usuarioCadastro.id
                };

                await conn.ExecuteAsync(query, parameters);
            }
        }

        public async Task UpdateContaPaga(ContaPaga contasPagas)
        {
            using (var conn = new SqlConnection(Runtime.ConnectionString))
            {
                var query = @"update ContaPaga
                                 set descricao = @Descricao,
	                                 tipoContaPaga_code = @TipoContaPaga_code,
	                                 data_pagamento = @Data_pagamento,
	                                 valor = @Valor,
	                                 data_alterado = @Data_alterado,
	                                 usuario_id_cadastro = @Usuario_id_cadastro
                               where id = @Id";

                var parameters = new
                {
                    Id = contasPagas.id,
                    Descricao = contasPagas.descricao,
                    tipoContaPaga_code = contasPagas.tipoContaPaga.code,
                    Data_pagamento = contasPagas.data_pagamento,
                    Valor = contasPagas.valor,
                    Data_alterado = contasPagas.data_alterado,
                    Usuario_id_cadastro = contasPagas.usuarioCadastro.id
                };

                await conn.ExecuteAsync(query, parameters);
            }
        }

        public async Task<IEnumerable<TotalContaPagaDiaResult>> GetContaPagaTotalDia(Usuario usuario)
        {
            var query = @"select convert(varchar(10), data_pagamento, 103) as dataPagamento,
	                             sum(valor_pago) as totalContaPaga
                            from ContaPaga
                           where usuario_id = @Usuario_id
                           group by convert(varchar(10), data_pagamento, 103)
                           order by convert(varchar(10), data_pagamento, 103)";

            using(var conn = new SqlConnection(Runtime.ConnectionString))
            {
                await conn.OpenAsync();

                return await conn.QueryAsync<TotalContaPagaDiaResult>(query, new { Usuario_id = usuario.id });
            }
        }
    }
}
