using System;
using System.Data.SqlClient;

namespace Loja_Guinevere.classes
{
    public class Venda
    {
        private int Id_venda;
        private DateTime Data;
        private float Total;
        private string Tipo_pag;
        private int Id_cliente;
        private int Id_desconto;
        private string _connectionString = @"Sua string de conexão aqui";

        public Venda(int id_venda, DateTime data, float total, string tipo_pag, int id_cliente, int id_desconto)
        {
            this.Id_venda = id_venda;
            this.Data = data;
            this.Total = total;
            this.Tipo_pag = tipo_pag;
            this.Id_cliente = id_cliente;
            this.Id_desconto = id_desconto;
        }

        public void GerarVenda()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("INSERT INTO venda (Id_venda, Data, Total, Tipo_pag, Id_cliente, Id_desconto) VALUES (@Id_venda, @Data, @Total, @Tipo_pag, @Id_cliente, @Id_desconto)", connection))
                {
                    command.Parameters.AddWithValue("@Id_venda", this.Id_venda);
                    command.Parameters.AddWithValue("@Data", this.Data);
                    command.Parameters.AddWithValue("@Total", this.Total);
                    command.Parameters.AddWithValue("@Tipo_pag", this.Tipo_pag);
                    command.Parameters.AddWithValue("@Id_cliente", this.Id_cliente);
                    command.Parameters.AddWithValue("@Id_desconto", this.Id_desconto);

                    command.ExecuteNonQuery();
                }
            }
        }


        private void CancelarVenda(int idVenda)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("DELETE FROM venda WHERE id_venda = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", idVenda);
                    command.ExecuteNonQuery();
                }
            }
        }


        public Venda Ver(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM venda WHERE Id_venda = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Venda(
                                (int)reader["Id_venda"],
                                (DateTime)reader["Data"],
                                (float)reader["Total"],
                                (string)reader["Tipo_pag"],
                                (int)reader["Id_cliente"],
                                (int)reader["Id_desconto"]);
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }
    }
}
