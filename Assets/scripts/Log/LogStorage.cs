using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class LogStorage
{
    const int numeroDeMensajesMaximo = 200;

    private List<LogItem> mensajes;
    public IEnumerable<LogItem> Mensajes { get { return mensajes; } }

    public LogStorage()
    {
        mensajes = new List<LogItem>();
    }

    public void Informar(ILogable logable)
    {
        LogItem logItem = logable.LogItem;

        mensajes.Insert(0, logItem);

        if (mensajes.Count > numeroDeMensajesMaximo)
            mensajes.RemoveAt(numeroDeMensajesMaximo);
    }
}