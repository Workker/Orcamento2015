USE [ORCAMENTO]
GO

INSERT INTO [dbo].[Carga]
           ([Id]
           ,[DataCriacao]
           ,[DataFim]
           ,[DataInicio]
           ,[Diretorio]
           ,[Status]
           ,[NomeArquivo]
           ,[Usuario_id])
     VALUES
           ('AEBF563A-F07D-4FD0-80D9-54BFAC313358'
           ,getDate()
           ,getDate()
           ,getDate()
           ,'D:\ProjectsWorkker\Orcamento2015\Orcamento.Robo.Web'
           ,'Erro processo'
           ,'FuncionarioCorporativo'
           ,24)
GO

INSERT INTO [dbo].[Carga]
           ([Id]
           ,[DataCriacao]
           ,[DataFim]
           ,[DataInicio]
           ,[Diretorio]
           ,[Status]
           ,[NomeArquivo]
           ,[Usuario_id])
     VALUES
           ('67F2BEAD-FB87-48B3-B3E8-BD1610E15A5A'
           ,getDate()
           ,getDate()
           ,getDate()
           ,'D:\ProjectsWorkker\Orcamento2015\Orcamento'
           ,'Erro processo'
           ,'Funcionario'
           ,24)
GO
