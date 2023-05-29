using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Loja_Guinevere
{
    public class SecaoRepositorio
    {
        private readonly string _connectionString = "sua string de conexão aqui";

        // Método Cadastrar
        public void Cadastrar(Secao secao)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                
                using (SqlCommand command = new SqlCommand("INSERT INTO secao (id_secao, id_setor, nome_secao) VALUES (@Id, @SetorId, @Nome)", connection))
                {
                    command.Parameters.AddWithValue("@Id", secao.Id);
                    command.Parameters.AddWithValue("@SetorId", secao.SetorId);
                    command.Parameters.AddWithValue("@Nome", secao.Name);

                    command.ExecuteNonQuery();
                }
            }
        }

        // Método Ver
        public List<Secao> Ver()
        {
            List<Secao> secoes = new List<Secao>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT id_secao, id_setor, nome_secao FROM secao", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Secao secao = new Secao
                            {
                                Id = reader.GetInt32(0),
                                SetorId = reader.GetInt32(1),
                                Name = reader.GetString(2)
                            };
                            secoes.Add(secao);
                        }
                    }
                }
            }

            return secoes;
        }

        // Método Alterar
        public void Alterar(Secao secao)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("UPDATE secao SET id_setor = @SetorId, nome_secao = @Nome WHERE id_secao = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", secao.Id);
                    command.Parameters.AddWithValue("@SetorId", secao.SetorId);
                    command.Parameters.AddWithValue("@Nome", secao.Name);

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

                using (SqlCommand command = new SqlCommand("DELETE FROM secao WHERE id_secao = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
/*
SecaoRepositorio repositorio = new SecaoRepositorio();

// Cadastrar uma nova secao
Secao novaSecao = new Secao { Id = 1, SetorId = 1, Name = "Vendas Online" };
repositorio.Cadastrar(novaSecao);

// Ver todas as secoes
List<Secao> secoes = repositorio.Ver
();

// Alterar uma secao
Secao secaoAlterada = new Secao { Id = 1, SetorId = 1, Name = "Vendas Internas" };
repositorio.Alterar(secaoAlterada);

// Excluir uma secao
repositorio.Excluir(1);
*/