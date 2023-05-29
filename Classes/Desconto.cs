using System;
using System.Data.SqlClient;

namespace Loja_Guinevere.classes
{
    public class Desconto
    {
        private int id_desconto;
        private DateTime desc_inicio;
        private DateTime desc_fim;
        private float desc_valor;
        private string _connectionString = @"Sua string de conexão aqui";

        public Desconto(int id_desconto, DateTime desc_inicio, DateTime desc_fim, float desc_valor)
        {
            this.id_desconto = id_desconto;
            this.desc_inicio = desc_inicio;
            this.desc_fim = desc_fim;
            this.desc_valor = desc_valor;
        }

        public void Incluir(Desconto desconto)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("INSERT INTO desconto VALUES(@Id, @Inicio, @Fim, @Valor)", connection))
                {
                    command.Parameters.AddWithValue("@Id", desconto.id_desconto);
                    command.Parameters.AddWithValue("@Inicio", desconto.desc_inicio);
                    command.Parameters.AddWithValue("@Fim", desconto.desc_fim);
                    command.Parameters.AddWithValue("@Valor", desconto.desc_valor);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void Alterar(Desconto desconto)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("UPDATE desconto SET desc_inicio = @Inicio, desc_fim = @Fim, desc_valor = @Valor WHERE id_desconto = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", desconto.id_desconto);
                    command.Parameters.AddWithValue("@Inicio", desconto.desc_inicio);
                    command.Parameters.AddWithValue("@Fim", desconto.desc_fim);
                    command.Parameters.AddWithValue("@Valor", desconto.desc_valor);

                    command.ExecuteNonQuery();
                }
            }
        }

        public Desconto Ver(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM desconto WHERE id_desconto = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Desconto((int)reader["id_desconto"], (DateTime)reader["desc_inicio"], (DateTime)reader["desc_fim"], (float)reader["desc_valor"]);
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

                using (SqlCommand command = new SqlCommand("DELETE FROM desconto WHERE id_desconto = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    command.ExecuteNonQuery();
                }
            }
        }

        public bool Validade()
        {
            return desc_inicio <= DateTime.Now && desc_fim >= DateTime.Now;
        }
    }
}
