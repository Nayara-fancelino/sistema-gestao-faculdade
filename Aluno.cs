using System;

public class Aluno : Pessoa
{
    public string NumeroMatricula { get; set; }

    public Aluno(string nome, string cpf, string email, string numeroMatricula)
        : base(nome, cpf, email)
    {
        NumeroMatricula = numeroMatricula;
    }
}