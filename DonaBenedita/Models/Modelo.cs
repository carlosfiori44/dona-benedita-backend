using MySql.Data.MySqlClient;
using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DonaBenedita.Models
{
    public class Modelo
    {        
        private string email, senha, nome, genero, id, conex = "Server=localhost; Database=loja_Benedita; Uid=root ;Password=root";
        private byte[] imagem;

        public string Email { get => email; set => email = value; }
        public string Senha { get => senha; set => senha = value; }
        public string Nome { get => nome; set => nome = value; }
        public string Genero { get => genero; set => genero = value; }
        public string Id { get => id; set => id = value; } 
        public byte[] Imagem { get => imagem; set => imagem = value; }
        
        public string enviarPersonalizado()
        {
            try
            {
                MySqlConnection con = new MySqlConnection(conex);
                con.Open();
                MySqlCommand qry = new MySqlCommand("INSERT INTO categoria(personalizado) VALUES(@img)", con);
                qry.Parameters.AddWithValue("@img", imagem);
                qry.ExecuteNonQuery();
                con.Close();
                return "Enviado com sucesso!";
            }
            catch (Exception e)
            {
                return "Erro: " + e.Message;
            }
        }

        public string CadastrarLogin()
        {
            try
            {
                MySqlConnection con = new MySqlConnection(conex); //Abrir conexão
                con.Open();
                MySqlCommand query = new MySqlCommand("SELECT * FROM cliente WHERE email = @email", con);
                query.Parameters.AddWithValue("@email", email);
                MySqlDataReader leitor = query.ExecuteReader();

                if (leitor.Read())
                {
                    con.Close();
                    return "Conta já existente!";
                }
                else
                {
                    leitor.Close();
                    query = new MySqlCommand("INSERT INTO cliente(email, senha, nome, sexo) VALUES(@email, @senha, @nome, @genero)", con);
                    query.Parameters.AddWithValue("@email", email);
                    query.Parameters.AddWithValue("@senha", senha);
                    query.Parameters.AddWithValue("@nome", nome);
                    query.Parameters.AddWithValue("@genero", genero);
                    query.ExecuteNonQuery();
                    con.Close();
                    return "Cadastrado!";
                }
            }
            catch (Exception e)
            {
                return "Erro: " + e.Message;
            }
        }

        public static List<Modelo> Listar() //Lista os clientes. Somente para administradores.
        {
            List<Modelo> lista = new List<Modelo>();
            try
            {
                MySqlConnection con = new MySqlConnection("Server=localhost; Database=loja_Benedita; Uid=root ;Password=2040364c@d4S"); 
                con.Open();
                MySqlCommand query = new MySqlCommand("SELECT * FROM cliente", con);
                MySqlDataReader leitor = query.ExecuteReader();

                if (leitor.HasRows)
                {
                    while (leitor.Read())
                    {
                        //Criar um item da lista
                        Modelo item = new Modelo();
                        item.Email = leitor["email"].ToString();
                        item.Nome = leitor["nome"].ToString();
                        item.Genero = leitor["sexo"].ToString();
                        lista.Add(item);
                    }
                }

                con.Close();
            }
            catch (Exception o)
            {
                lista = null;
                Modelo item = new Modelo();
                item.Nome = "não" + o.Message;
                lista.Add(item);
                return lista;
            }

            return lista;
        }      

        public string VerificarLogin()
        {
            try
            {
                MySqlConnection con = new MySqlConnection("Server=localhost; Database=loja_Benedita; Uid=root ;Password="); con.Open();
                MySqlCommand query = new MySqlCommand("SELECT * FROM cliente WHERE email = @email", con);
                query.Parameters.AddWithValue("email", email);
                MySqlDataReader leitor = query.ExecuteReader();
                if (leitor.Read())
                {
                    if (leitor["senha"].Equals(senha))
                    {
                        id = leitor["nome"].ToString();
                        return "Você entrou na sua conta!";
                    }
                    else
                    {
                        return "Senha incorreta!";
                    }
                }
                else
                {
                    return "Essa conta não existe!";
                }
            }
            catch (Exception e)
            {
                return "Erro: " + e.Message;
            }
        }
    }
}