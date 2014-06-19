using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orcamento.Acesso
{
    public interface ITransactionManager : IDisposable
    {
        void VoteRollBack();
        void VoteCommit();
        void Initialize();
    }
}
