using Microsoft.AspNetCore.Mvc;

namespace PetaVerseApi.DTOs
{
    public abstract class BaseDTO<T>
    {
        [FromRoute]
        public T Id { get; set; } = default!;
    }

    public abstract class BaseDTO : BaseDTO<string> { }
}
