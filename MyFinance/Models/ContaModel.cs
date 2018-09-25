﻿using Microsoft.AspNetCore.Http;
using MyFinance.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Models
{
    public class ContaModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o Nome da Conta")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe o Saldo da Conta")]
        public double Saldo { get; set; }

        public int Usuario_id { get; set;}
        public IHttpContextAccessor HttpContextAccessor { get; set; }

        public ContaModel()
        {

        }

        //Recebe o contexto para acesso ás variáveis de sessão.
        public ContaModel(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }


        public List<ContaModel> ListaConta()
        {
            List<ContaModel> lista = new List<ContaModel>();
            ContaModel item;

            string id_usuario_logado = HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
            string sql = $"SELECT ID, NOME, SALDO, USUARIO_ID FROM CONTA WHERE USUARIO_ID  = {id_usuario_logado}";
            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTable(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new ContaModel();
                item.Id = int.Parse(dt.Rows[i]["ID"].ToString());
                item.Nome = dt.Rows[i]["NOME"].ToString();
                item.Saldo = double.Parse(dt.Rows[i]["SALDO"].ToString());
                item.Usuario_id = int.Parse(dt.Rows[i]["USUARIO_ID"].ToString());

                lista.Add(item);
            }
            return lista;

        }

        public void Insert()
        {
            string id_usuario_logado = HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
            string sql =$"INSERT INTO CONTA (NOME, SALDO, USUARIO_ID) VALUES('{Nome}', '{Saldo}', '{id_usuario_logado}')";
            DAL objDAL = new DAL();
            objDAL.ExecutarComandoSQL(sql);
        }

        public void Excluir(int id_conta)
        {
            new DAL().ExecutarComandoSQL("DELETE FROM CONTA WHERE ID = " + id_conta);
        }
    }
}
