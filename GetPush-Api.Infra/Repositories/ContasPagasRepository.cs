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
        public async Task DeleteContasPagas(Guid contaPagaId)
        {
            using (var conn = new SqlConnection(Runtime.ConnectionString))
            {
                var query = @"delete 
                                from contasPagas 
                               where id = @Id";

                var parameters = new { Id = contaPagaId };

                await conn.ExecuteAsync(query, parameters);
            }
        }

        public async Task<IEnumerable<ContasPagasResult>> GetContasPagas(Usuario usuario, IEnumerable<TipoContaPagaResult> tipoContaPagaList)
        {
            var query = @"select cp.id,
							     cp.descricao,
							     cp.tipoContasPagar_code,
							     cp.data_pagamento,
							     cp.valor,
							     cp.usuario_id,
                                 u.nome,
							     cp.data_cadastro,
							     cp.data_alterado,
							     cp.usuario_id_cadastro,
                                 uc.nome as nomeCadastro
						    from contasPagas cp
                      inner join usuario u on u.id = cp.usuario_id
                      inner join usuario uc on uc.id = cp.usuario_id_cadastro
                           where usuario_id = @Usuario_id";

            using (var conn = new SqlConnection(Runtime.ConnectionString))
            {
                await conn.OpenAsync();
                var result = await conn.QueryAsync(query, new { Usuario_id = usuario.id });

                var contasPagarList = new List<ContasPagasResult>();

                if (result != null)
                {
                    foreach (var item in result)
                    {
                        var tipoContaPaga = tipoContaPagaList.Where(x => x.code == item.tipoContasPagar_code).FirstOrDefault();

                        contasPagarList.Add(new ContasPagasResult
                        {
                            id = item.id,
                            descricao = item.descricao,
                            tipoContaPaga = tipoContaPaga != null ? tipoContaPaga : new TipoContaPagaResult(),                            
                            data_pagamento = item.data_pagamento,
                            valor = item.valor,
                            usuario = new UsuarioResult { 
                                id = item.usuario_id,
                                nome = item.nome
                            },
                            data_cadastro = item.data_cadastro,
                            data_alterado = item.data_alterado,
                            usuarioCadastro = new UsuarioResult { 
                                id = item.usuario_id_cadastro,
                                nome = item.nomeCadastro
                            }
                        });
                    }

                    return contasPagarList;
                }

                return new List<ContasPagasResult>();
            }
        }

        public async Task InsertContasPagas(ContasPagas contasPagas)
        {
            using (var conn = new SqlConnection(Runtime.ConnectionString))
            {
                var query = @"insert 
                                into contasPagas 
                                    (id,
	                                 descricao,
	                                 tipoContasPagar_code,
	                                 data_pagamento,
	                                 valor,
	                                 usuario_id,
	                                 data_cadastro,
	                                 data_alterado,
	                                 usuario_id_cadastro)
                             values (newid(),
                                     @Descricao,
	                                 @TipoContasPagar_code,
	                                 @Data_pagamento,
	                                 @Valor,
	                                 @Usuario_id,
	                                 @Data_cadastro,
	                                 @Data_alterado,
	                                 @Usuario_id_cadastro)";

                var parameters = new
                {
                    Descricao = contasPagas.descricao,
                    TipoContasPagar_code = contasPagas.tipoContaPaga.code,
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

        public async Task UpdateContasPagas(ContasPagas contasPagas)
        {
            using (var conn = new SqlConnection(Runtime.ConnectionString))
            {
                var query = @"update contasPagas 
                                 set descricao = @Descricao,
	                                 tipoContasPagar_code = @TipoContasPagar_code,
	                                 data_pagamento = @Data_pagamento,
	                                 valor = @Valor,
	                                 data_alterado = @Data_alterado,
	                                 usuario_id_cadastro = @Usuario_id_cadastro
                               where id = @Id";

                var parameters = new
                {
                    Id = contasPagas.id,
                    Descricao = contasPagas.descricao,
                    TipoContasPagar_code = contasPagas.tipoContaPaga.code,
                    Data_pagamento = contasPagas.data_pagamento,
                    Valor = contasPagas.valor,
                    Data_alterado = contasPagas.data_alterado,
                    Usuario_id_cadastro = contasPagas.usuarioCadastro.id
                };

                await conn.ExecuteAsync(query, parameters);
            }
        }
    }
}
