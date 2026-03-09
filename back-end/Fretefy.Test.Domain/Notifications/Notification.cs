using Fretefy.Test.Domain.Interfaces.Notifications;
using System.Collections.Generic;
using System.Linq;

namespace Fretefy.Test.Domain.Notifications
{
    public abstract class Notification : INotification
    {
        private readonly List<string> _mensagens = new List<string>();
        public IReadOnlyCollection<string> Mensagens => _mensagens.AsReadOnly();
        public bool Invalido => _mensagens.Any();

        public void AdicionarMensagem(string mensagem)
        {
            if (!string.IsNullOrWhiteSpace(mensagem))
                _mensagens.Add(mensagem);
        }

        public void AdicionarRangeMensagens(IEnumerable<string> mensagens)
        {
            if (mensagens != null)
                _mensagens.AddRange(mensagens.Where(m => !string.IsNullOrWhiteSpace(m)));
        }
    }
}
