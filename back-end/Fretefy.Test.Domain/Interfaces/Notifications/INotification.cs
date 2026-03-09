using System.Collections.Generic;

namespace Fretefy.Test.Domain.Interfaces.Notifications
{
    public interface INotification
    {
        IReadOnlyCollection<string> Mensagens { get; }
        bool Invalido { get; }
        void AdicionarMensagem(string mensagem);
        void AdicionarRangeMensagens(IEnumerable<string> mensagens);
    }
}
