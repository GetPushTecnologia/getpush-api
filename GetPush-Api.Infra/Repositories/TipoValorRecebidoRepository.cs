using Dapper;
using GetPush_Api.Domain.Commands.Results;
using GetPush_Api.Domain.Entities;
using GetPush_Api.Domain.Repositories;
using GetPush_Api.Shared;
using System.Data.SqlClient;

namespace GetPush_Api.Infra.Repositories
{
    public class TipoValorRecebidoRepository : ITipoValorRecebidoRepository
    {
        public async Task DeleteTipoValorRecebido(Guid tipoContipoValorRecebidoIdtaPagaId)
        {
            using (var conn = new SqlConnection(Runtime.ConnectionString))
            {
                var query = @"delete 
                                from TipoValorRecebido 
                               where id = @Id";

                var parameters = new { Id = tipoContipoValorRecebidoIdtaPagaId };

                await conn.ExecuteAsync(query, parameters);
            }
        }

        public async Task<IEnumerable<TipoValorRecebidoResult>> GetTipoValorRecebido()
        {
            var query = @"select tvr.id,
                                 tvr.code,
							     tvr.descricao,                                
							     tvr.data_cadastro,
							     tvr.data_alterado,
							     tvr.usuario_id_cadastro,
                                 u.nome as nomeCadastro
						    from TipoValorRecebido tvr
                      inner join Usuario u on u.id = tvr.usuario_id_cadastro";

            using (var conn = new SqlConnection(Runtime.ConnectionString))
            {
                await conn.OpenAsync();
                var result = await conn.QueryAsync(query);

                var contasPagarList = new List<TipoValorRecebidoResult>();

                if (result != null)
                {
                    foreach (var item in result)
                    {
                        contasPagarList.Add(new TipoValorRecebidoResult
                        {
                            id = item.id,
                            code = item.code,
                            descricao = item.descricao,
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

                return new List<TipoValorRecebidoResult>();
            }
        }

        public async Task InsertTipoValorRecebido(TipoValorRecebido tipoValorRecebido)
        {
            using (var conn = new SqlConnection(Runtime.ConnectionString))
            {
                var query = @"insert 
                                into TipoValorRecebido 
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
                    Code = tipoValorRecebido.code,
                    Descricao = tipoValorRecebido.descricao,
                    Data_cadastro = tipoValorRecebido.data_cadastro,
                    Data_alterado = tipoValorRecebido.data_alterado,
                    Usuario_id_cadastro = tipoValorRecebido.usuarioCadastro.id
                };

                await conn.ExecuteAsync(query, parameters);
            }
        }

        public async Task UpdateTipoContaPaga(TipoValorRecebido tipoValorRecebido)
        {
            using (var conn = new SqlConnection(Runtime.ConnectionString))
            {
                var query = @"update TipoValorRecebido 
                                 set code = @Code,
                                     descricao = @Descricao,
                                     data_alterado = @Data_alterado,
                                     usuario_id_cadastro = @Usuario_id_cadastro
                               where id = @Id";

                var parameters = new
                {
                    Id = tipoValorRecebido.id,
                    Descricao = tipoValorRecebido.descricao,
                    Data_alterado = tipoValorRecebido.data_alterado,
                    Usuario_id_cadastro = tipoValorRecebido.usuarioCadastro.id
                };

                await conn.ExecuteAsync(query, parameters);
            }
        }
    }
}
