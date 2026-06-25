
namespace VatroApi.V1.Shared
{
    public record Error(
        RecordErrorType Code,
        string Description
    );
}