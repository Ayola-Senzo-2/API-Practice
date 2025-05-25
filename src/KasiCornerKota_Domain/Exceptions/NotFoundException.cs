
namespace KasiCornerKota_Domain.Exceptions
{
    public class NotFoundException(string resource, string resourceIdentitfier) 
        : Exception($"{resource} with id: {resourceIdentitfier} does not exist")
    {

    }
}
