USE [Calculator]
GO
/****** Object:  StoredProcedure [dbo].[ADDY]    Script Date: 22/04/2019 19:46:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[ADDY] @y INT
AS
INSERT INTO Table_Y(Y) VALUES (@y) 
