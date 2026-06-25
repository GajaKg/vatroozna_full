
namespace VatroApi.V1.Shared.ErrorHandling
{
    public static class ResultErrors
    {
        public static Error RecordNotFound(string column)
            => new(
                RecordErrorType.NotFound,
                $"Upis sa id/imenom: '{column}' ne postoji."
            );

        public static Error RecordAlreadyExists(string name)
            => new(
                RecordErrorType.Exists,
                $"Upis sa imenom '{name}' već postoji."
            );

        public static Error ServerError()
            => new(
                RecordErrorType.ServerError,
                "Došlo je do greške. Pokušajte ponovo."
            );
    }
}