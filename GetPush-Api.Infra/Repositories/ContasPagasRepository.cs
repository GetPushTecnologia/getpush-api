using Dapper;
using GetPush_Api.Domain.Commands.Results;
using GetPush_Api.Domain.Repositories;
using GetPush_Api.Shared;
using System.Data.SqlClient;

namespace GetPush_Api.Infra.Repositories
{
    public class ContasPagasRepository : IContasPagasRepository
    {
        public async Task<IEnumerable<ContasPagarResult>> GetContasPagas()
        {
            var query = @"select ul.id as ul_id,
								 ul.login as ul_login,
								 ul.password ul_password,
								 ul.data_cadastro as ul_data_cadastro,
								 ul.data_alterado as ul_data_alterado,
								 ul.usuario_id_cadastro as ul_usuario_id_cadastro,
								 u.id as u_id,
								 u.nome as u_nome,
								 u.cpf as u_cpf,
								 u.email as u_email,
								 u.nascimento as u_nascimento,
								 u.sexo as u_sexo,
								 u.ativo as u_ativo,
								 u.data_cadastro as u_data_cadastro,
								 u.data_alterado u_data_alterado,
								 u.usuario_id_cadastro as u_usuario_id_cadastro
						    from usuario u with(nolock)
						   inner join usuarioLogin ul with(nolock) on ul.usuario_id = u.id
                           where email = @email";

			using (var conn = new SqlConnection(Runtime.ConnectionString))
			{
				await conn.OpenAsync();
				var result = await conn.QueryAsync(query);

				var contasPagarList = new List<ContasPagarResult>();

				if (result != null)
				{
					contasPagarList.Add(new ContasPagarResult
					{

					});
                }

				return new List<ContasPagarResult>();

            }
        }
    }
}
