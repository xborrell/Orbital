using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class LogItem
{
    public int Level { get; protected set; }
    public string Titulo { get; protected set; }
    public string Descripcion { get; protected set; }

    public LogItem(int level, string titulo, string descripcion)
    {
        Level = level;
        Titulo = titulo;
        Descripcion = descripcion;
    }

    public LogItem(int level, string titulo)
    {
        Level = level;
        Titulo = titulo;
        Descripcion = titulo;
    }
}