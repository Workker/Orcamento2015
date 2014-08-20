using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PostSharp.Aspects;
using PostSharp.Laos;

namespace Orcamento.Domain.Session
{
    /// <summary>
    /// Atributo de Controle de transação
    /// </summary>
    [Serializable]
    public class TransactionalAttribute : PostSharp.Laos.OnMethodBoundaryAspect
    {
        /// <summary>
        /// Transaction Manager
        /// </summary>
        private ITransactionManager transactionManager;

        /// <summary>
        /// Pilha de transações 
        /// </summary>
        [ThreadStatic]
        private static Stack<ITransactionManager> scopes;

        /// <summary>
        /// É uma nova transação?
        /// </summary>
        public Boolean IsNew { get; set; }

        /// <summary>
        /// Somente Leitura
        /// </summary>
        public Boolean ReadOnly { get; set; }

        /// <summary>
        /// Flag para nunca comitar
        /// </summary>
        public Boolean NeverCommit { get; set; }

        /// <summary>
        /// Transaction Manager
        /// </summary>
        public ITransactionManager TransactionManager
        {
            get
            {
                if (transactionManager == null) // TODO:Retirar acoplamento
                    transactionManager = new TransactionManagerFluent();

                return transactionManager;
            }
            set { transactionManager = value; }
        }

        /// <summary>
        /// Construtor
        /// </summary>
        public TransactionalAttribute()
        {
            AspectPriority = 2;
            IsNew = false;
            ReadOnly = false;
            NeverCommit = false;
        }

        /// <summary>
        /// Gerencia o início de uma transação e é chamado quando um método qualquer que utiliza o atributo [Transactional] é executado
        /// </summary>
        /// <param name="e">Argummentos de evento do POST SHARP</param>
        public override void OnEntry(MethodExecutionEventArgs e)
        {
            base.OnEntry(e);

            if (scopes == null)
                scopes = new Stack<ITransactionManager>();

            TransactionModeEnum transactionMode = IsNew || ReadOnly ? TransactionModeEnum.New : TransactionModeEnum.Inherits;

            TransactionManager.Initialize();

            scopes.Push(transactionManager);
        }

        /// <summary>
        /// É executado quando um método qualquer que utiliza o atributo [Transacional] estorou uma execeção
        /// o comportamento deste método executa um RollBack em todas as transações da pilha
        /// </summary>
        /// <param name="e">Argummentos de evento do POST SHARP</param>
        public override void OnException(MethodExecutionEventArgs e)
        {
            base.OnException(e);

            var transaction = scopes.Peek();
            
            transaction.VoteRollBack();
        }

        /// <summary>
        /// No caso de um método qualquer que utilize o atributo [Transactional] ser executado com sucesso este evento é executado 
        /// comitando a pilha de transações
        /// </summary>
        /// <param name="e">Argummentos de evento do POST SHARP</param>
        public override void OnSuccess(MethodExecutionEventArgs e)
        {
            base.OnSuccess(e);

            if (!ReadOnly && !NeverCommit)
                scopes.Peek().VoteCommit();
            else
                scopes.Peek().VoteRollBack();
        }

        /// <summary>
        /// Após o método OnSucess ou OnException ser executado este método é automaticamente disparado com um evento de limpeza da pilha e da
        /// classe corrente no Garbage Collector
        /// </summary>
        /// <param name="eventArgs"></param>
        public override void OnExit(MethodExecutionEventArgs eventArgs)
        {
            base.OnExit(eventArgs);

            scopes.Pop().Dispose();
        }
    }
}
