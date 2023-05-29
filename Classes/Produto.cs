using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Loja_Guinevere
{
    public class ProdutoRepositorio
    {
        private readonly string _connectionString = "sua string de conexão aqui";

        // Método Cadastrar
        public void Cadastrar(Produto produto)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                
                using (SqlCommand command = new SqlCommand("INSERT INTO produto (id_produto, id_secao, nome_produto, valor_produto, marca, resumo, detalhe, servico) VALUES (@Id, @SecaoId, @Nome, @Valor, @Marca, @Resumo, @Detalhe, @Servico)", connection))
                {
                    command.Parameters.AddWithValue("@Id", produto.Id);
                    command.Parameters.AddWithValue("@SecaoId", produto.SecaoId);
                    command.Parameters.AddWithValue("@Nome", produto.Name);
                    command.Parameters.AddWithValue("@Valor", produto.Valor);
                    command.Parameters.AddWithValue("@Marca", produto.Marca);
                    command.Parameters.AddWithValue("@Resumo", produto.Resumo);
                    command.Parameters.AddWithValue("@Detalhe", produto.Detalhe);
                    command.Parameters.AddWithValue("@Servico", produto.Servico ? 1 : 0);

                    command.ExecuteNonQuery();
                }
            }
        }

        // Método Ver
        public List<Produto> Ver()
        {
            List<Produto> produtos = new List<Produto>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT id_produto, id_secao, nome_produto, valor_produto, marca, resumo, detalhe, servico FROM produto", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Produto produto = new Produto
                            {
                                Id = reader.GetInt32(0),
                                SecaoId = reader.GetInt32(1),
                                Name = reader.GetString(2),
                                Valor = reader.GetFloat(3),
                                Marca = reader.GetString(4),
                                Resumo = reader.GetString(5),
                                Detalhe = reader.GetString(6),
                                Servico = reader.GetInt32(7) == 1
                            };
                            produtos.Add(produto);
                        }
                    }
                }
            }

            return produtos;
        }

        // Método Alterar
        public void Alterar(Produto produto)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("UPDATE produto SET id_secao = @SecaoId, nome_produto = @Nome, valor_produto = @Valor, marca = @Marca, resumo = @Resumo, detalhe = @Detalhe, servico = @Servico WHERE id_produto = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", produto.Id);
                    command.Parameters.AddWithValue("@SecaoId", produto.SecaoId);
                    command.Parameters.AddWithValue("@Nome", produto.Name);
                    command.Parameters.AddWithValue("@Valor", produto.Valor);
                    command.Parameters.AddWithValue("@Marca", produto.Marca);
                    command.Parameters.AddWithValue("@Resumo", produto.Resumo);
                    command.Parameters.AddWithValue("@Detalhe", produto.Detalhe);
                    command.Parameters.AddWithValue("@Servico", produto.Servico ? 1 : 0);
                            command.ExecuteNonQuery();
                }
            }
        }

        // Método Excluir
        public void Excluir(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
            connection.Open();
                using (SqlCommand command = new SqlCommand("DELETE FROM produto WHERE id_produto = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}

/*
E aqui está um exemplo de como usar `ProdutoRepositorio`:

```csharp
ProdutoRepositorio repositorio = new ProdutoRepositorio();

// Cadastrar um novo produto
Produto novoProduto = new Produto { Id = 1, SecaoId = 1, Name = "Produto 1", Valor = 100.0f, Marca = "Marca 1", Resumo = "Resumo 1", Detalhe = "Detalhe 1", Servico = false };
repositorio.Cadastrar(novoProduto);

// Ver todos os produtos
List<Produto> produtos = repositorio.Ver();

// Alterar um produto
Produto produtoAlterado = new Produto { Id = 1, SecaoId = 1, Name = "Produto Alterado", Valor = 150.0f, Marca = "Marca Alterada", Resumo = "Resumo Alterado", Detalhe = "Detalhe Alterado", Servico = true };
repositorio.Alterar(produtoAlterado);

// Excluir um produto
repositorio.Excluir(1);
*/

