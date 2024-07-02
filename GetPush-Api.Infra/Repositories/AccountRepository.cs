using GetPush_Api.Domain.Commands.Results;
using GetPush_Api.Domain.Repositories;
using GetPush_Api.Shared;
using System.Data.SqlClient;
using Dapper;
using GetPush_Api.Domain.Commands.Results.map;

namespace GetPush_Api.Infra.Repositories
{
    public class AccountRepository : IAccountRepository
    {
		private readonly UsuarioMap _usuarioMap;
        public AccountRepository(UsuarioMap usuarioMap)
        {
			_usuarioMap = usuarioMap;
        }

        public async Task<UsuarioLoginResult> GetUsuarioLogin(string email)
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
                var result = await conn.QueryFirstOrDefaultAsync(query, new { email });

                if (result != null)
                {
                    var usuarioLogin = new UsuarioLoginResult
                    {
                        id = result.ul_id,
                        login = result.ul_login,
                        password = result.ul_password,
                        data_cadastro = result.ul_data_cadastro,
                        data_alterado = result.ul_data_alterado,
                        usuarioCadastro = new UsuarioResult { id = result.u_usuario_id_cadastro },
                        usuario = _usuarioMap.UsuarioMapResult(result)

                    };

                    return usuarioLogin;
                }

                return new UsuarioLoginResult();
            }
        }
    }
}
