namespace Lock.LockManager
{
    public static class LoginManager
    {

        /// <summary>
        /// Verifica se a senha fornecida corresponde ao hash de senha armazenado para o usuário.
        /// </summary>
        /// <param name="requestPassword">A senha fornecida pelo usuário durante a tentativa de login.</param>
        /// <param name="userHash">O hash de senha armazenado para o usuário.</param>
        /// <param name="salt">O salt usado para gerar o hash de senha. Opcional.</param>
        /// <returns>Verdadeiro se a senha fornecida corresponder ao hash de senha armazenado, senão falso.</returns>
        public static bool LoginAccess(string requestPassword, string userHash, string salt = "")
        {
            var requestHash = LockManager.GenerateHashByPassword(requestPassword, salt);

            if (requestHash == userHash) return true;

            return false;
        }
    }
}