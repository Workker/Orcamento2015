USE [ORCAMENTO]
GO


/****** Object:  Table [dbo].[Detalhe]    Script Date: 25/06/2014 19:38:47 ******/
DROP TABLE [dbo].[Detalhe]
GO

/****** Object:  Table [dbo].[Detalhe]    Script Date: 25/06/2014 19:38:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Detalhe](
	[Id] [uniqueidentifier] NOT NULL,
	[Nome] [nvarchar](255) NULL,
	[Descricao] [nvarchar](255) NULL,
	[Linha] INT null,
	[Carga_id] [uniqueidentifier] NULL,
PRIMARY KEY CLUSTERED 

(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Detalhe]  WITH CHECK ADD  CONSTRAINT [FKC4A804511F15F64] FOREIGN KEY([Carga_id])
REFERENCES [dbo].[Carga] ([Id])
GO

ALTER TABLE [dbo].[Detalhe] CHECK CONSTRAINT [FKC4A804511F15F64]
GO


