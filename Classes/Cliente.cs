using System;
using System.Data.SqlClient;

namespace Loja_Guinevere.classes
{
    public class Cliente
    {
        private int Id_cliente;
        private string Nome;
        private string Sobrenome;
        private string Email;
        private string Endereco;
        private string Fone;
        private string UserName;
        private string Password;
        private string _connectionString = @"Sua string de conexão aqui";

        public Cliente(int id_cliente, string nome, string sobrenome, string email, string endereco, string fone, string username, string password)
        {
            this.Id_cliente = id_cliente;
            this.Nome = nome;
            this.Sobrenome = sobrenome;
            this.Email = email;
            this.Endereco = endereco;
            this.Fone = fone;
            this.UserName = username;
            this.Password = password;
        }

        public void Cadastrar(Cliente cliente)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("INSERT INTO cliente VALUES(@Id, @Nome, @Sobrenome, @Email, @Endereco, @Fone, @UserName, @Password)", connection))
                {
                    command.Parameters.AddWithValue("@Id", cliente.Id_cliente);
                    command.Parameters.AddWithValue("@Nome", cliente.Nome);
                    command.Parameters.AddWithValue("@Sobrenome", cliente.Sobrenome);
                    command.Parameters.AddWithValue("@Email", cliente.Email);
                    command.Parameters.AddWithValue("@Endereco", cliente.Endereco);
                    command.Parameters.AddWithValue("@Fone", cliente.Fone);
                    command.Parameters.AddWithValue("@UserName", cliente.UserName);
                    command.Parameters.AddWithValue("@Password", cliente.Password);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void Alterar(Cliente cliente)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("UPDATE cliente SET Nome = @Nome, Sobrenome = @Sobrenome, Email = @Email, Endereco = @Endereco, Fone = @Fone, UserName = @UserName, Password = @Password WHERE Id_cliente = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", cliente.Id_cliente);
                    command.Parameters.AddWithValue("@Nome", cliente.Nome);
                    command.Parameters.AddWithValue("@Sobrenome", cliente.Sobrenome);
                    command.Parameters.AddWithValue("@Email", cliente.Email);
                    command.Parameters.AddWithValue("@Endereco", cliente.Endereco);
                    command.Parameters.AddWithValue("@Fone", cliente.Fone);
                    command.Parameters.AddWithValue("@UserName", cliente.UserName);
                    command.Parameters.AddWithValue("@Password", cliente.Password);

                    command.ExecuteNonQuery();
                }
            }
        }

        public Cliente Ver(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM cliente WHERE Id_cliente = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Cliente(
                                (int)reader["Id_cliente"],
                                (string)reader["Nome"],
                                (string)reader["Sobrenome"],
                                (string)reader["Email"],
                                (string)reader["Endereco"],
                                (string)reader["Fone"],
                                (string)reader["UserName"],
                                (string)reader["Password"]);
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        public void Excluir(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("DELETE FROM cliente WHERE Id_cliente = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
