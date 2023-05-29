static void Main(string[] args)
{
    // Crie uma instância do repositório
    DepartamentoRepositorio repositorio = new DepartamentoRepositorio();

    // Crie um novo departamento
    Departamento novoDepartamento = new Departamento { Id = 1, Name = "Novo Departamento", Number = 101 };

    // Chame o método Cadastrar
    repositorio.Cadastrar(novoDepartamento);

    Console.WriteLine("Departamento cadastrado com sucesso!");
}
