using Form.Core;

namespace Form.Data
{
    public interface IClientFormRepository
    {
        Task<ClientForm> CreateFormAsync(ClientForm form);

        Task<IEnumerable<ClientForm>> GetFormAsync(string questionType);


     }
}
