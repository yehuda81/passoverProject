USE [Calculator]
GO
/****** Object:  StoredProcedure [dbo].[ADDX]    Script Date: 22/04/2019 19:43:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[ADDX] @x INT
AS
INSERT INTO Table_X(X) VALUES (@x) 
