public class PromoDep : IPromocao
{
    private readonly string _connectionString = @"Data Source=.\sqlexpress;Initial Catalog=Loja_Guinevere;Integrated Security=True";

    public int Id { get; set; }
    public int Grau { get; set; }
    public DateTime Inicio { get; set; }
    public DateTime Fim { get; set; }
    public int Porcentagem { get; set; }
    public float Valor { get; set; }

    // Relacionar a promoção com o departamento correspondente
    public Departamento Departamento { get; set; }
    public Setor Setor { get; set; }
    public Secao Secao { get; set; }
    public Produto Produto { get; set; }

    public void Cadastrar()
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            using (SqlCommand command = new SqlCommand(
                "INSERT INTO PromoDep (grau_promo, inicio_promo, fim_promo, promo_porc, promo_valor) " +
                "VALUES (@Grau, @Inicio, @Fim, @Porcentagem, @Valor)", connection))
            {
                command.Parameters.AddWithValue("@Grau", this.Grau);
                command.Parameters.AddWithValue("@Inicio", this.Inicio);
                command.Parameters.AddWithValue("@Fim", this.Fim);
                command.Parameters.AddWithValue("@Porcentagem", this.Porcentagem);
                command.Parameters.AddWithValue("@Valor", this.Valor);

                command.ExecuteNonQuery();
            }
        }
    }

    public void Ver()
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            using (SqlCommand command = new SqlCommand("SELECT * FROM PromoDep WHERE id_promo = @Id", connection))
            {
                command.Parameters.AddWithValue("@Id", this.Id);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        this.Grau = reader.GetInt32(1);
                        this.Inicio = reader.GetDateTime(2);
                        this.Fim = reader.GetDateTime(3);
                        this.Porcentagem = reader.GetInt32(4);
                        this.Valor = reader.GetFloat(5);
                    }
                }
            }
        }
    }

    public void Alterar()
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            using (SqlCommand command = new SqlCommand(
                "UPDATE PromoDep SET grau_promo = @Grau, dt_inicio = @Inicio, dt_Fim = @Fim, promo_porc = @Porcentagem, promo_valor = @Valor " +
                "WHERE id_promo = @Id", connection))
            {
                command.Parameters.AddWithValue("@Id", this.Id);
                command.Parameters.AddWithValue("@Grau", this.Grau);
                command.Parameters.AddWithValue("@Inicio", this.Inicio);
                command.Parameters.AddWithValue("@Fim", this.Fim);
                command.Parameters.AddWithValue("@Porcentagem", this.Porcentagem);
                command.Parameters.AddWithValue("@Valor", this.Valor);

                command.ExecuteNonQuery();
            }
        }
    }

    public void Excluir()
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            using (SqlCommand command = new SqlCommand("DELETE FROM PromoDep WHERE id_promo = @Id", connection))
            {
                command.Parameters.AddWithValue("@Id", this.Id);
                command.ExecuteNonQuery();
            }
        }
    }

    public bool PromoValida()
    {
        return Inicio < Fim && DateTime.Now >= Inicio && DateTime.Now <= Fim;
    }

    public void AplicarPromocao()
    {
        // Inicializando a conexão com o banco de dados
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            // Verificando qual entidade está associada à promoção e aplicando o desconto
            if (Departamento != null)
            {
                foreach (var setor in Departamento.Setores)
                {
                    foreach (var secao in setor.Secoes)
                    {
                        foreach (var produto in secao.Produtos)
                        {
                            // Armazenando o preço original e aplicando o desconto
                            produto.PrecoOriginal = produto.Preco;
                            produto.Preco = produto.Preco * (1 - PromoValor / 100);

                            // Atualizando o produto no banco de dados
                            using (SqlCommand command = new SqlCommand("UPDATE produto SET preco = @Preco, preco_original = @PrecoOriginal WHERE id_produto = @Id", connection))
                            {
                                command.Parameters.AddWithValue("@Id", produto.Id);
                                command.Parameters.AddWithValue("@Preco", produto.Preco);
                                command.Parameters.AddWithValue("@PrecoOriginal", produto.PrecoOriginal);
                                command.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
            else if (Setor != null)
            {
                foreach (var secao in Setor.Secoes)
                {
                    foreach (var produto in secao.Produtos)
                    {
                        produto.Preco -= produto.Preco * (Porcentagem / 100.0);
                    }
                }
            }
            else if (Secao != null)
            {
                foreach (var produto in Secao.Produtos)
                {
                    produto.Preco -= produto.Preco * (Porcentagem / 100.0);
                }
            }
            else if (Produto != null)
            {
                Produto.Preco -= Produto.Preco * (Porcentagem / 100.0);
            }
        }

    }
    public void DesfazerPromocao()
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            if (Departamento != null)
            {
                foreach (var setor in Departamento.Setores)
                {
                    foreach (var secao in setor.Secoes)
                    {
                        foreach (var produto in secao.Produtos)
                        {
                            // Revertendo o preço para o original
                            produto.Preco = produto.PrecoOriginal;

                            // Atualizando o produto no banco de dados
                            using (SqlCommand command = new SqlCommand("UPDATE produto SET preco = @Preco WHERE id_produto = @Id", connection))
                            {
                                command.Parameters.AddWithValue("@Id", produto.Id);
                                command.Parameters.AddWithValue("@Preco", produto.Preco);
                                command.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
            else if (Setor != null)
            {
                foreach (var secao in Setor.Secoes)
                {
                    foreach (var produto in secao.Produtos)
                    {
                        produto.Preco = produto.PrecoOriginal;
                    }
                }
            }
            else if (Secao != null)
            {
                foreach (var produto in Secao.Produtos)
                {
                    produto.Preco = produto.PrecoOriginal;
                }
            }
            else if (Produto != null)
            {
                Produto.Preco = Produto.PrecoOriginal;
            }
        }
    }
}