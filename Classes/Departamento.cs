using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Loja_Guinevere
{
    public class DepartamentoRepositorio
    {
        private readonly string _connectionString = @"Data Source = WINDOWS10;Initial Catalog = GUINEVERE;User ID = ;Password = ";

       
        public void Cadastrar(Departamento departamento)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                
                using (SqlCommand command = new SqlCommand("INSERT INTO departamento (id_departamento, nome_dep) VALUES (@Id, @Nome)", connection))
                {
                    command.Parameters.AddWithValue("@Id", departamento.Id);
                    command.Parameters.AddWithValue("@Nome", departamento.Name);
                    command.Parameters.AddWithValue("@Numero", departamento.Numero);
                    command.ExecuteNonQuery();
                }
            }
        }

        
        public List<Departamento> Ver()
        {
            List<Departamento> departamentos = new List<Departamento>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT id_departamento, nome_dep FROM departamento", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Departamento departamento = new Departamento
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Numero = reader.GetString(2)
                            };
                            departamentos.Add(departamento);
                        }
                    }
                }
            }

            return departamentos;
        }

        
        public void Alterar(Departamento departamento)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("UPDATE departamento SET nome_dep = @Nome, numero_dep = @Numero WHERE id_departamento = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", departamento.Id);
                    command.Parameters.AddWithValue("@Nome", departamento.Name);
                    command.Parameters.AddWithValue("@Numero", departamento.Numero);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Excluir(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("DELETE FROM departamento WHERE id_departamento = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    command.ExecuteNonQuery();
                }
            }
        }
    }    
} 
/*
DepartamentoRepositorio repositorio = new DepartamentoRepositorio();

// Cadastrar um novo departamento
Departamento novoDepartamento = new Departamento { Id = 1, Name = "Vendas" };
repositorio.Cadastrar(novoDepartamento);

// Ver todos os departamentos
List<Departamento> departamentos = repositorio.Ver();

// Alterar um departamento
Departamento departamentoAlterado = new Departamento { Id = 1, Name = "Marketing" };
repositorio.Alterar(departamentoAlterado);

// Excluir um departamento
repositorio.Excluir(1);
*/