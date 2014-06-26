USE [ORCAMENTO]
GO

/****** Object:  Table [dbo].[Carga]    Script Date: 25/06/2014 19:37:58 ******/
DROP TABLE [dbo].[Carga]
GO

/****** Object:  Table [dbo].[Carga]    Script Date: 25/06/2014 19:37:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Carga](
	[Id] [uniqueidentifier] NOT NULL,
	[DataCriacao] [datetime] NULL,
	[DataFim] [datetime] NULL,
	[DataInicio] [datetime] NULL,
	[Diretorio] [nvarchar](255) NULL,
	[Status] [nvarchar](255) NULL,
	[NomeArquivo] [nvarchar](255) NULL,
	[Usuario_id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Carga]  WITH CHECK ADD  CONSTRAINT [FKA897D6443C8B03EA] FOREIGN KEY([Usuario_id])
REFERENCES [dbo].[Usuario] ([Id])
GO

ALTER TABLE [dbo].[Carga] CHECK CONSTRAINT [FKA897D6443C8B03EA]
GO


