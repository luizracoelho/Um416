using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using Um416.DAL.Base;

namespace Um416.DAL
{
    public class NotificacaoRepository : BaseRepository<Notificacao>
    {
        public IEnumerable<NotificacaoLeitura> List(long id)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                return db.Query<NotificacaoLeitura>(
                    "Select Notificacoes.*, LeiturasNotificacoes.Lida, LeiturasNotificacoes.DataHoraLeitura From LeiturasNotificacoes Left Join Notificacoes On Notificacoes.Id = LeiturasNotificacoes.NotificacaoId Where ClienteId = @ClienteId Order By Notificacoes.DataHora Desc;",
                    new { ClienteId = id }
                );
            }
        }
    }
}
