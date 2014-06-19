using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orcamento.Domain.Util
{

    public static class VerificarTipo
    {
        public class CasoInformacao
        {
            public bool IsDefault { get; set; }
            public Type Target { get; set; }
            public Action<object> Action { get; set; }
        }

        public static void Verifique(object source, params CasoInformacao[] casos)
        {
            var type = source.GetType();
            foreach (var entry in casos)
            {
                if (entry.IsDefault || type == entry.Target)
                {
                    entry.Action(source);
                    break;
                }
            }
        }

        public static CasoInformacao Caso<T>(Action action)
        {
            return new CasoInformacao()
            {
                Action = x => action(),
                Target = typeof(T)
            };
        }

        public static CasoInformacao Caso<T>(Action<T> action)
        {
            return new CasoInformacao()
            {
                Action = (x) => action((T)x),
                Target = typeof(T)
            };
        }

        public static CasoInformacao Default(Action action)
        {
            return new CasoInformacao()
            {
                Action = x => action(),
                IsDefault = true
            };
        }
    }
}
