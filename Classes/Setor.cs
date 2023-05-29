using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Loja_Guinevere
{
    public class SetorRepositorio
    {
        private readonly string _connectionString = "sua string de conexão aqui";

        // Método Cadastrar
        public void Cadastrar(Setor setor)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                
                using (SqlCommand command = new SqlCommand("INSERT INTO setor (id_setor, id_departamento, nome_setor) VALUES (@Id, @DepartamentoId, @Nome)", connection))
                {
                    command.Parameters.AddWithValue("@Id", setor.Id);
                    command.Parameters.AddWithValue("@DepartamentoId", setor.DepartamentoId);
                    command.Parameters.AddWithValue("@Nome", setor.Name);

                    command.ExecuteNonQuery();
                }
            }
        }

        // Método Ver
        public List<Setor> Ver()
        {
            List<Setor> setores = new List<Setor>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT id_setor, id_departamento, nome_setor FROM setor", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Setor setor = new Setor
                            {
                                Id = reader.GetInt32(0),
                                DepartamentoId = reader.GetInt32(1),
                                Name = reader.GetString(2)
                            };
                            setores.Add(setor);
                        }
                    }
                }
            }

            return setores;
        }

        // Método Alterar
        public void Alterar(Setor setor)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("UPDATE setor SET id_departamento = @DepartamentoId, nome_setor = @Nome WHERE id_setor = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", setor.Id);
                    command.Parameters.AddWithValue("@DepartamentoId", setor.DepartamentoId);
                    command.Parameters.AddWithValue("@Nome", setor.Name);

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

                using (SqlCommand command = new SqlCommand("DELETE FROM setor WHERE id_setor = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    command.ExecuteNonQuery();
                }
            }
        }
    }   
}  

/*
SetorRepositorio repositorio = new SetorRepositorio();

// Cadastrar um novo setor
Setor novoSetor = new Setor { Id = 1, DepartamentoId = 1, Name = "Vendas Internas" };
repositorio.Cadastrar(novoSetor);

// Ver todos os setores
List<Setor> setores = repositorio.Ver();
*/

