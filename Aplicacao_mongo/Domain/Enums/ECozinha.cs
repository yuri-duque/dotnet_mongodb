using System;

namespace Domain.Enums
{
    public enum ECozinha
    {
        Brasileira = 1,
        Italiana = 2,
        Arabe = 3,
        Japonesa = 4,
        FastFood = 5
    }

    public static class ECozinhaHelper
    {
        public static ECozinha ConverterDeInteiro(int valor)
        {
            if (!Enum.IsDefined(typeof(ECozinha), valor))
                throw new ArgumentOutOfRangeException("cozinha");

            Enum.TryParse(valor.ToString(), out ECozinha cozinha);
            return cozinha;
        }
    }
}