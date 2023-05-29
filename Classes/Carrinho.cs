using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Loja_Guinevere
{
    public class Carrinho
    {
        private string _connectionString = "your_connection_string";

        public int Id { get; set; }
        public List<Produto> Produtos { get; set; } = new List<Produto>();
        public int QuantidadeProd { get; set; }

        public Carrinho(int id_carrinho, int quantidade_prod)
        {
            Id = id_carrinho;
            QuantidadeProd = quantidade_prod;
        }

        public void Cadastrar(Carrinho carrinho)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("INSERT INTO carrinho (id_carrinho, quantidade_prod) VALUES (@Id, @QuantidadeProd)", connection))
                {
                    command.Parameters.AddWithValue("@Id", carrinho.Id);
                    command.Parameters.AddWithValue("@QuantidadeProd", carrinho.QuantidadeProd);
                    command.ExecuteNonQuery();
                }
            }
        }

        public Carrinho Ver(int id_carrinho)
        {
            Carrinho carrinho = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM carrinho WHERE id_carrinho = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id_carrinho);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            carrinho = new Carrinho(reader.GetInt32(0), reader.GetInt32(1));
                        }
                    }
                }
            }

            return carrinho;
        }

        public void Alterar(Carrinho carrinho)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("UPDATE carrinho SET quantidade_prod = @QuantidadeProd WHERE id_carrinho = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", carrinho.Id);
                    command.Parameters.AddWithValue("@QuantidadeProd", carrinho.QuantidadeProd);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Excluir(int id_carrinho)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("DELETE FROM carrinho WHERE id_carrinho = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id_carrinho);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void IncluirProduto(Produto produto)
        {
            Produtos.Add(produto);
            QuantidadeProd++;
        }

        public void ExcluirProduto(Produto produto)
        {
            Produtos.Remove(produto);
            QuantidadeProd--;
        }

        public List<Produto> VerLista()
        {
            return Produtos;
        }

        public float ValorTotal()
        {
            float total = 0;

            foreach (Produto produto in Produtos)
            {
                total += produto.Valor;
            }

            return total;
        }
    }
}
