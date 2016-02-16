using System.Linq;

namespace Ticket.Core
{
    public class RequestView
    {
        private Request _request;

        public RequestView(Request request)
        {
            _request = request;

            var requestView = _request.Itens
                .Select(i => $"{i.NumberOfItens} tickets for {i.Event.Title}")
                .ToArray();

            Description = requestView.Any()
                ? requestView.Aggregate((c, r) => $"{c} <br/> {r}")
                : "No itens for this request";
            StatusId = request.StatusId;
            Date = _request.Date.ToString("dd/MM/yyy HH:mm:ss");
        }

        /// <summary>Gets a date</summary>
        public string Date { get; }


        public string Description { get; }

        public string Number => _request.Id.ToString();

        public int StatusId { get; private set; }

        public decimal Total => _request.Total;
    }
}