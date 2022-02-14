namespace SoCarro.Business.Validations.Documentos;

public static class RenavamValidacao
{

    public const int TamanhoRenavam = 11;

    public static bool Validar(string renavam)
    {
        var renavamNumeros = Utils.ApenasNumeros(renavam);

        if (!TamanhoValido(renavamNumeros)) return false;
        return !TemDigitosRepetidos(renavamNumeros) && TemDigitosValidos(renavamNumeros);
    }

    private static bool TamanhoValido(string valor) => valor.Length == TamanhoRenavam;

    private static bool TemDigitosRepetidos(string valor)
    {
        string[] invalidNumbers =
        {
                "00000000000000",
                "11111111111111",
                "22222222222222",
                "33333333333333",
                "44444444444444",
                "55555555555555",
                "66666666666666",
                "77777777777777",
                "88888888888888",
                "99999999999999"
            };
        return invalidNumbers.Contains(valor);
    }

    private static bool TemDigitosValidos(string renavam)
    {
        if (string.IsNullOrEmpty(renavam.Trim())) return false;

        int[] digitos = new int[11];

        int valor = 0;

        for (int i = 0; i < 11; i++)
            digitos[i] = Convert.ToInt32(renavam.Substring(i, 1));

        for (int i = 0; i < 10; i++)
            valor += digitos[i] * Convert.ToInt32(renavam.Substring(i, 1));

        valor = (valor * 10) % 11; valor = (valor != 10) ? valor : 0;
        return (valor == digitos[10]);
    }
}