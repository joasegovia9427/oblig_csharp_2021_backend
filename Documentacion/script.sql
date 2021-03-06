USE [master]
GO
/****** Object:  Database [musicmanagements]    Script Date: 21/11/2021 18:15:06 ******/
CREATE DATABASE [musicmanagements]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'musicmanagment', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\musicmanagment.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'musicmanagment_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\musicmanagment_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [musicmanagements] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [musicmanagements].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [musicmanagements] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [musicmanagements] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [musicmanagements] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [musicmanagements] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [musicmanagements] SET ARITHABORT OFF 
GO
ALTER DATABASE [musicmanagements] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [musicmanagements] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [musicmanagements] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [musicmanagements] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [musicmanagements] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [musicmanagements] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [musicmanagements] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [musicmanagements] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [musicmanagements] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [musicmanagements] SET  DISABLE_BROKER 
GO
ALTER DATABASE [musicmanagements] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [musicmanagements] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [musicmanagements] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [musicmanagements] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [musicmanagements] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [musicmanagements] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [musicmanagements] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [musicmanagements] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [musicmanagements] SET  MULTI_USER 
GO
ALTER DATABASE [musicmanagements] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [musicmanagements] SET DB_CHAINING OFF 
GO
ALTER DATABASE [musicmanagements] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [musicmanagements] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [musicmanagements] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [musicmanagements] SET QUERY_STORE = OFF
GO
USE [musicmanagements]
GO
/****** Object:  User [genexus]    Script Date: 21/11/2021 18:15:06 ******/
CREATE USER [genexus] FOR LOGIN [genexus] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[Album]    Script Date: 21/11/2021 18:15:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Album](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](40) NOT NULL,
	[AnioCreacion] [int] NOT NULL,
	[GeneroMusicalId] [int] NOT NULL,
	[BandaId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AlbumCancion]    Script Date: 21/11/2021 18:15:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AlbumCancion](
	[AlbumId] [int] NOT NULL,
	[CancionId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[AlbumId] ASC,
	[CancionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Banda]    Script Date: 21/11/2021 18:15:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Banda](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](40) NOT NULL,
	[AnioCreacion] [int] NOT NULL,
	[AnioSeparacion] [int] NULL,
	[GeneroMusicalId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cancion]    Script Date: 21/11/2021 18:15:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cancion](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](40) NOT NULL,
	[Duracion] [decimal](18, 0) NOT NULL,
	[Anio] [int] NOT NULL,
	[Dato] [text] NULL,
	[IntegranteId] [int] NULL,
	[GeneroMusicalId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GeneroMusical]    Script Date: 21/11/2021 18:15:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GeneroMusical](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](40) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Integrante]    Script Date: 21/11/2021 18:15:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Integrante](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FechaNacimiento] [date] NOT NULL,
	[Foto] [text] NULL,
	[PersonaId] [int] NOT NULL,
	[BandaId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Persona]    Script Date: 21/11/2021 18:15:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Persona](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](40) NOT NULL,
	[Apellido] [varchar](40) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Resenia]    Script Date: 21/11/2021 18:15:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Resenia](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Puntaje] [int] NOT NULL,
	[Resenia] [text] NOT NULL,
	[UsuarioId] [int] NULL,
	[BandaId] [int] NULL,
	[CancionId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 21/11/2021 18:15:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NombreUsuario] [varchar](40) NOT NULL,
	[Contrasenia] [varchar](40) NOT NULL,
	[PersonaId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Banda] ADD  DEFAULT (NULL) FOR [AnioSeparacion]
GO
ALTER TABLE [dbo].[Cancion] ADD  DEFAULT ((0)) FOR [IntegranteId]
GO
ALTER TABLE [dbo].[Resenia] ADD  DEFAULT (NULL) FOR [UsuarioId]
GO
ALTER TABLE [dbo].[Resenia] ADD  DEFAULT (NULL) FOR [BandaId]
GO
ALTER TABLE [dbo].[Resenia] ADD  DEFAULT (NULL) FOR [CancionId]
GO
ALTER TABLE [dbo].[Album]  WITH CHECK ADD  CONSTRAINT [fk_Albumgeneromusic] FOREIGN KEY([GeneroMusicalId])
REFERENCES [dbo].[GeneroMusical] ([Id])
GO
ALTER TABLE [dbo].[Album] CHECK CONSTRAINT [fk_Albumgeneromusic]
GO
ALTER TABLE [dbo].[AlbumCancion]  WITH CHECK ADD  CONSTRAINT [fk_albumcanc] FOREIGN KEY([AlbumId])
REFERENCES [dbo].[Album] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AlbumCancion] CHECK CONSTRAINT [fk_albumcanc]
GO
ALTER TABLE [dbo].[AlbumCancion]  WITH CHECK ADD  CONSTRAINT [fk_cancalbum] FOREIGN KEY([CancionId])
REFERENCES [dbo].[Cancion] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AlbumCancion] CHECK CONSTRAINT [fk_cancalbum]
GO
ALTER TABLE [dbo].[Banda]  WITH CHECK ADD  CONSTRAINT [fk_generomusicalbanda] FOREIGN KEY([GeneroMusicalId])
REFERENCES [dbo].[GeneroMusical] ([Id])
GO
ALTER TABLE [dbo].[Banda] CHECK CONSTRAINT [fk_generomusicalbanda]
GO
ALTER TABLE [dbo].[Cancion]  WITH CHECK ADD  CONSTRAINT [fk_generoMusicaCancion] FOREIGN KEY([GeneroMusicalId])
REFERENCES [dbo].[GeneroMusical] ([Id])
GO
ALTER TABLE [dbo].[Cancion] CHECK CONSTRAINT [fk_generoMusicaCancion]
GO
ALTER TABLE [dbo].[Cancion]  WITH CHECK ADD  CONSTRAINT [fk_integrante] FOREIGN KEY([IntegranteId])
REFERENCES [dbo].[Integrante] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Cancion] CHECK CONSTRAINT [fk_integrante]
GO
ALTER TABLE [dbo].[Integrante]  WITH CHECK ADD  CONSTRAINT [fk_integrantepersona] FOREIGN KEY([PersonaId])
REFERENCES [dbo].[Persona] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Integrante] CHECK CONSTRAINT [fk_integrantepersona]
GO
ALTER TABLE [dbo].[Resenia]  WITH CHECK ADD  CONSTRAINT [fk_usuarioresenia] FOREIGN KEY([UsuarioId])
REFERENCES [dbo].[Usuario] ([Id])
GO
ALTER TABLE [dbo].[Resenia] CHECK CONSTRAINT [fk_usuarioresenia]
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [fk_usuariopersona] FOREIGN KEY([PersonaId])
REFERENCES [dbo].[Persona] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [fk_usuariopersona]
GO
USE [master]
GO
ALTER DATABASE [musicmanagements] SET  READ_WRITE 
GO
