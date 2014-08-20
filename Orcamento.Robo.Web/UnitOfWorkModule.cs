using Orcamento.Domain.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orcamento.Robo.Web
{
    public class UnitOfWorkModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.BeginRequest += context_BeginRequest;
            context.EndRequest += context_EndRequest;
        }

        public void Dispose() { }

        private void context_BeginRequest(object sender, EventArgs e)
        {
            IUnitOfWork instance = UnitOfWorkFactory.GetDefault();
            instance.Begin();
        }

        private void context_EndRequest(object sender, EventArgs e)
        {
            IUnitOfWork instance = UnitOfWorkFactory.GetDefault();
            try
            {
                instance.Commit();
            }
            catch (Exception ex)
            {
                instance.RollBack();
                throw new Exception("Problemas com o unitOfWork", ex);
            }
            finally
            {
                instance.Dispose();
            }
        }
    }
}