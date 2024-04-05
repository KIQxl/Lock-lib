using System.Security.Cryptography;
using System.Text;

namespace Lock.LockManager
{
    public static class LockManager
    {
        /// <summary>
        /// Gera um hash seguro para uma senha, opcionalmente utilizando um salt.
        /// </summary>
        /// <param name="password">A senha a ser hashed.</param>
        /// <param name="salt">O salt a ser concatenado à senha antes de gerar o hash. Opcional.</param>
        /// <returns>Hash seguro gerado para a senha.</returns>
        public static string GenerateHashByPassword(string passsword, string salt = "")
        {
            //concatenando password com salt
            string passwordWithSalt = passsword + salt;

            using (var sha256 = SHA256.Create())
            {
                // convertendo o resultado da concatenação para um array de bytes
                byte[] bytes = Encoding.UTF8.GetBytes(passwordWithSalt);

                // criando o hash
                byte[] hash = sha256.ComputeHash(bytes);

                // convertendo o hash para uma string base 64 e retornando a mesma
                return Convert.ToBase64String(hash);
            }
        }


        /// <summary>
        /// Gera um salt aleatório de 16 bytes para ser usado em operações de hashing.
        /// </summary>
        /// <returns>Salt aleatório de 16 bytes em formato de string Base64.</returns>
        public static string GenerateSalt16Bytes()
        {
            byte[] saltBytes = new byte[16];

            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }

            return Convert.ToBase64String(saltBytes);
        }

        /// <summary>
        /// Gera um salt aleatório de 32 bytes para ser usado em operações de hashing.
        /// </summary>
        /// <returns>O salt aleatório de 32 bytes em formato de string Base64.</returns>
        public static string GenerateSalt32Bytes()
        {
            byte[] saltBytes = new byte[32];

            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }

            return Convert.ToBase64String(saltBytes);
        }


        /// <summary>
        /// Gera uma senha aleatória de 15 caracteres, contendo pelo menos uma letra maiúscula, uma letra minúscula, um número e um caractere especial.
        /// </summary>
        /// <returns>Senha aleatória gerada.</returns>
        public static string GenerateRandomPassword()
        {
            const string lowerCase = "abcdefghijklmnopqrstuvwxyz";
            const string capitalLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string numbers = "0123456789";
            const string specialCharacters = "!@#$%^&*()_+-=<>?";
            Random random = new Random();

            // Gera uma sequência de caracteres aleatórios do conjunto permitido
            string password = new string(
                new[]
                 {
                    capitalLetters[random.Next(capitalLetters.Length)],
                    lowerCase[random.Next(lowerCase.Length)],
                    numbers[random.Next(numbers.Length)],
                    specialCharacters[random.Next(specialCharacters.Length)]
                 }
             .Concat(Enumerable.Repeat(capitalLetters + lowerCase + numbers + specialCharacters, 11)
                 .Select(s => s[random.Next(s.Length)]))
             .ToArray());

            // Embaralha os caracteres
            password = new string(password.ToCharArray().OrderBy(c => random.Next()).ToArray());

            return password;
        }
    }
}
