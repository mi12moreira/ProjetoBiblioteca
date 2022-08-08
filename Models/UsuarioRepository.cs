using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;
using MySqlConnector;

namespace Biblioteca.Models
{
    public class UsuarioRepository
    {
        private const string DadosConexao = "Database=biblioteca; Data Source=localhost; User Id=root;";
        public Usuarios ValidarLogin (Usuarios u)
        {
            MySqlConnection Conexao = new MySqlConnection(DadosConexao);
            Conexao.Open();

            string Query = "SELECT * FROM usuarios WHERE login = @login AND senha = MD5(@senha);";
            MySqlCommand Comando = new MySqlCommand(Query, Conexao);

            Comando.Parameters.AddWithValue("@login", u.login);
            Comando.Parameters.AddWithValue("@senha", u.senha);
            Comando.Parameters.AddWithValue("@id", u.id);

            MySqlDataReader Reader = Comando.ExecuteReader();

            Usuarios user = new Usuarios();
             user.id = 0;

            while (Reader.Read())
            {
                user.id = Reader.GetInt32("id");

                if (!Reader.IsDBNull(Reader.GetOrdinal("login")))
                {
                    user.login = Reader.GetString("login");
                }
                if (!Reader.IsDBNull(Reader.GetOrdinal("senha")))
                {
                    user.senha = Reader.GetString("senha");
                }
                if (!Reader.IsDBNull(Reader.GetOrdinal("privilegio")))
                {
                    user.privilegio = Reader.GetString("privilegio");
                }
            }
            Conexao.Close();

            return user;
        }

        public void Inserir(Usuarios usr)
         {
            MySqlConnection Conexao = new MySqlConnection(DadosConexao);
            Conexao.Open();

            string Query = "INSERT INTO usuarios(login, senha, privilegio) VALUES (@login, (MD5(@senha)), @privilegio)";
            MySqlCommand Comando = new MySqlCommand(Query, Conexao);

            Comando.Parameters.AddWithValue("@login", usr.login);
            Comando.Parameters.AddWithValue("@senha", usr.senha);
            Comando.Parameters.AddWithValue("@privilegio", usr.privilegio);

            Comando.ExecuteNonQuery();

            Conexao.Close();
        }

            public List<Usuarios> Listar(){
            List<Usuarios> lista = new List<Usuarios>();

            MySqlConnection Conexao = new MySqlConnection(DadosConexao);
            Conexao.Open();

            string Query = "SELECT * FROM usuarios;";
            MySqlCommand Comando = new MySqlCommand(Query, Conexao);

            MySqlDataReader Reader = Comando.ExecuteReader();

            while(Reader.Read()) {
                Usuarios usr = new Usuarios();

                usr.id = Reader.GetInt32("id");

                if (!Reader.IsDBNull(Reader.GetOrdinal("login"))) {
                    usr.login = Reader.GetString("login");
                }
                if (!Reader.IsDBNull(Reader.GetOrdinal("privilegio"))) {
                    usr.privilegio = Reader.GetString("privilegio");
                }

                lista.Add(usr);
            }

            Conexao.Close();

            return lista;
        }

        public void Deletar(int id){
            MySqlConnection Conexao = new MySqlConnection(DadosConexao);
            Conexao.Open();

            string Query = "DELETE FROM usuarios WHERE id=@id";

            MySqlCommand Comando = new MySqlCommand(Query, Conexao);

            Comando.Parameters.AddWithValue("@id",id);

            Comando.ExecuteNonQuery();
            Conexao.Close();
        }

        public Usuarios BuscarPorId(int id){
            MySqlConnection Conexao = new MySqlConnection(DadosConexao);
            Conexao.Open();

            string Query = "SELECT * FROM usuarios WHERE id=@id;";
            MySqlCommand Comando = new MySqlCommand(Query, Conexao);

            Comando.Parameters.AddWithValue("@id",id);

            MySqlDataReader Reader = Comando.ExecuteReader();

            Usuarios user = new Usuarios();

            while(Reader.Read()) {
                user.id = Reader.GetInt32("id");

                if (!Reader.IsDBNull(Reader.GetOrdinal("login"))) {
                    user.login = Reader.GetString("login");
                }
                if (!Reader.IsDBNull(Reader.GetOrdinal("privilegio"))) {
                    user.privilegio = Reader.GetString("privilegio");
                }
            }

            Conexao.Close();
            return user;
        }

        public void Editar(Usuarios usr){
            MySqlConnection Conexao = new MySqlConnection(DadosConexao);
            Conexao.Open();

            string Query = "UPDATE usuarios SET login=@login, privilegio=@privilegio WHERE id=@id";

            MySqlCommand Comando = new MySqlCommand(Query, Conexao);

            Comando.Parameters.AddWithValue("@login", usr.login);
            Comando.Parameters.AddWithValue("@privilegio", usr.privilegio);
            Comando.Parameters.AddWithValue("@id", usr.id);

            Comando.ExecuteNonQuery();
            Conexao.Close();
        }
    }
} 