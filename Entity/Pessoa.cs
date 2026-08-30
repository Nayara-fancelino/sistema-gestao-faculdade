namespace sistema_gestao_faculdade.Entity
{

    public abstract class Pessoa
{
    public string Nome { get; set; }
    public string CPF { get; set; }
    public string Email { get; set; }
    public List<string> Notificacoes { get; private set; } = new List<string>();

    protected Pessoa(string nome, string cpf, string email)
    {
        Nome = nome;
        CPF = cpf;
        Email = email;
    }

    public void ReceberNotificacao(string mensagem)
    {
        Notificacoes.Add(mensagem);
    }
}
}