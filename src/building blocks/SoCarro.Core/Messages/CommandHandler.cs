﻿using FluentValidation.Results;
using SoCarro.Core.Data;

namespace SoCarro.Core.Messages;
public abstract class CommandHandler
{
    protected ValidationResult ValidationResult;

    protected CommandHandler()
    {
        ValidationResult = new ValidationResult();
    }

    protected void AdicionarErro(string mensagem)
    {
        ValidationResult.Errors.Add(new ValidationFailure(string.Empty, mensagem));
    }

    //Método genérico para ser utilizado nos respositórios.
    protected async Task<ValidationResult> PersistirDados(IUnitOfWork uow)
    {
        if (!await uow.Commit()) AdicionarErro("Houve um erro ao persistir os dados");

        return ValidationResult;
    }
}