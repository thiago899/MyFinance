using System;
using Xunit;
using MyFinance.Models;

namespace ProjetoTestes
{
    public class UnitTest1Models
    {
        [Fact]
        public void TestLoginUsuario()
        {
            UsuarioModel usuarioModel = new UsuarioModel();
            usuarioModel.Email = "thiago@email.com";
            usuarioModel.Senha = "123456";
            bool result = usuarioModel.ValidarLogin();
            Assert.True(result);
        }

        [Fact]
        public void TesteRegistrarUsuario()
        {
            UsuarioModel usuarioModel = new UsuarioModel();
            usuarioModel.Nome = "Teste";
            usuarioModel.Data_nascimento = "1987/05/22";
            usuarioModel.Email = "usuario@email.com";
            usuarioModel.Senha = "123";
            usuarioModel.RegistrarUsuario();
            bool result = usuarioModel.ValidarLogin();
            Assert.True(result);
        }
    }
}
