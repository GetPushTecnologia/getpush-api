using Dapper;
using GetPush_Api.Domain.Commands.Results;
using GetPush_Api.Domain.Entities;
using GetPush_Api.Domain.Repositories;
using GetPush_Api.Shared;
using System.Data.SqlClient;

namespace GetPush_Api.Infra.Repositories
{
    public class ValorRecebidoRepository : IValorRecebidoRepository
    {
        public async Task DeleteValorRecebido(Guid valorRecebidoId)
        {
            using (var conn = new SqlConnection(Runtime.ConnectionString))
            {
                var query = @"delete 
                                from ValorRecebido 
                               where id = @Id";

                var parameters = new { Id = valorRecebidoId };

                await conn.ExecuteAsync(query, parameters);
            }
        }

        public async Task<IEnumerable<ValorRecebidoResult>> GetValorRecebido(Usuario usuario, IEnumerable<TipoValorRecebidoResult> tipoValorRecebidoList)
        {
            var query = @"select vr.id,
							     vr.descricao,
							     vr.tipoValorRecebido_code,
							     vr.data_recebimento,
							     vr.valor_recebido,
							     vr.usuario_id,
                                 u.nome,
							     vr.data_cadastro,
							     vr.data_alterado,
							     vr.usuario_id_cadastro,
                                 uc.nome as nomeCadastro
						    from ValorRecebido vr
                      inner join Usuario u on u.id = vr.usuario_id
                      inner join Usuario uc on uc.id = vr.usuario_id_cadastro
                           where usuario_id = @Usuario_id";

            using (var conn = new SqlConnection(Runtime.ConnectionString))
            {
                await conn.OpenAsync();
                var result = await conn.QueryAsync(query, new { Usuario_id = usuario.id });

                var contasPagarList = new List<ValorRecebidoResult>();

                if (result != null)
                {
                    foreach (var item in result)
                    {
                        var tipoValorRecebido = tipoValorRecebidoList.Where(x => x.code == item.tipoValorRecebido_code).FirstOrDefault();

                        contasPagarList.Add(new ValorRecebidoResult
                        {
                            id = item.id,
                            descricao = item.descricao,
                            tipoValorRecebido = tipoValorRecebido != null ? tipoValorRecebido : new TipoValorRecebidoResult(),
                            data_recebimento = item.data_recebimento,
                            valor_recebido = item.valor_recebido,
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

                return new List<ValorRecebidoResult>();
            }
        }

        public async Task InsertValorRecebido(ValorRecebido valorRecebido)
        {
            using (var conn = new SqlConnection(Runtime.ConnectionString))
            {
                var query = @"insert 
                                into ValorRecebido 
                                    (id,
                                     descricao,
                                     tipoValorRecebido_code,
                                     data_recebimento,
                                     valor,
                                     usuario_id,
                                     data_cadastro,
                                     data_alterado,
                                     usuario_id_cadastro)
                             values (newid(),
                                     @Descricao,
	                                 @TipoValorRecebido_code,
	                                 @Data_recebimento,
	                                 @Valor,
	                                 @Usuario_id,
	                                 @Data_cadastro,
	                                 @Data_alterado,
	                                 @Usuario_id_cadastro)";

                var parameters = new
                {
                    Descricao = valorRecebido.descricao,
                    TipoValorRecebido_code = valorRecebido.tipoValorRecebido.code,
                    Data_recebimento = valorRecebido.data_recebimento,
                    Valor = valorRecebido.valor,
                    Usuario_id = valorRecebido.usuario.id,
                    Data_cadastro = valorRecebido.data_cadastro,
                    Data_alterado = valorRecebido.data_alterado,
                    Usuario_id_cadastro = valorRecebido.usuarioCadastro.id
                };

                await conn.ExecuteAsync(query, parameters);
            }
        }

        public async Task UpdateValorRecebido(ValorRecebido valorRecebido)
        {
            using (var conn = new SqlConnection(Runtime.ConnectionString))
            {
                var query = @"update ValorRecebido 
                                 set descricao = @Descricao,
	                                 tipoValorRecebido_code = @TipoValorRecebido_code,
	                                 data_recebimento = @Data_recebimento,
	                                 valor = @Valor,
	                                 data_alterado = @Data_alterado,
	                                 usuario_id_cadastro = @Usuario_id_cadastro
                               where id = @Id";

                var parameters = new
                {
                    Id = valorRecebido.id,
                    Descricao = valorRecebido.descricao,
                    TipoValorRecebido = valorRecebido.tipoValorRecebido.code,
                    Data_pagamento = valorRecebido.data_recebimento,
                    Valor = valorRecebido.valor,
                    Data_alterado = valorRecebido.data_alterado,
                    Usuario_id_cadastro = valorRecebido.usuarioCadastro.id
                };

                await conn.ExecuteAsync(query, parameters);
            }
        }
    }
}
