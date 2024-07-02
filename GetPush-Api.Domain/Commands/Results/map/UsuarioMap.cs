namespace GetPush_Api.Domain.Commands.Results.map
{
    public class UsuarioMap
    {
        public UsuarioResult UsuarioMapResult(dynamic item)
        {
            return new UsuarioResult
            {
                id = item.u_id,
                nome = item.u_nome,
                cpf = item.u_cpf,
                email = item.u_email,
                nascimento = item.u_nascimento,
                sexo = item.u_sexo,
                ativo = item.u_ativo,
                data_cadastro = item.u_data_cadastro,
                data_alterado = item.u_data_alterado,
                usuarioCadastro = new UsuarioResult { id = item.u_usuario_id_cadastro }
            };
        }

        public UsuarioResult UsuarioCadastroMapResult(dynamic item)
        {
            return new UsuarioResult
            {
                id = item.u_usuario_id_cadastro
            };
        }
    }
}
