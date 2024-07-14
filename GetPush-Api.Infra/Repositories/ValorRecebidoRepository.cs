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
                                from valorRecebido 
                               where id = @Id";

                var parameters = new { Id = valorRecebidoId };

                await conn.ExecuteAsync(query, parameters);
            }
        }

        public async Task<IEnumerable<ValorRecebidoResult>> GetValorRecebido(Usuario usuario)
        {
            var query = @"select vr.id,
							     vr.descricao,
							     vr.tipoValorRecebico_code,
							     vr.data_recebimento,
							     vr.valor,
							     vr.usuario_id,
                                 u.nome,
							     vr.data_cadastro,
							     vr.data_alterado,
							     vr.usuario_id_cadastro
                                 uc.nome as nomeCadastro
						    from valorRecebido vr
                      inner join usuario u on u.id = vr.usuario_id
                      inner join usuario uc on u.id = vr.usuario_id_cadastro
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
                        contasPagarList.Add(new ValorRecebidoResult
                        {
                            id = item.id,
                            descricao = item.descricao,
                            tipoValorRecebico = new TipoValorRecebidoResult { code = item.tipoValorRecebico_code },
                            data_recebimento = item.data_pagamento,
                            valor = item.valor,
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
                                into valorRecebido 
                                    (id,
                                     descricao,
                                     tipoValorRecebico_code,
                                     data_recebimento,
                                     valor,
                                     usuario_id,
                                     data_cadastro,
                                     data_alterado,
                                     usuario_id_cadastro)
                             values (newid(),
                                     @Descricao,
	                                 @TipoValorRecebico_code,
	                                 @Data_recebimento,
	                                 @Valor,
	                                 @Usuario_id,
	                                 @Data_cadastro,
	                                 @Data_alterado,
	                                 @Usuario_id_cadastro)";

                var parameters = new
                {
                    Descricao = valorRecebido.descricao,
                    TipoContasPagar_code = valorRecebido.tipoValorRecebico.code,
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
                var query = @"update valorRecebido 
                                 set descricao = @Descricao,
	                                 tipoValorRecebico_code = @TipoValorRecebico_code,
	                                 data_recebimento = @Data_recebimento,
	                                 valor = @Valor,
	                                 data_alterado = @Data_alterado,
	                                 usuario_id_cadastro = @Usuario_id_cadastro
                               where id = @Id";

                var parameters = new
                {
                    Id = valorRecebido.id,
                    Descricao = valorRecebido.descricao,
                    TipoContasPagar_code = valorRecebido.tipoValorRecebico.code,
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
