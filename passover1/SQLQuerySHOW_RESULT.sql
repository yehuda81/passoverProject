USE [Calculator]
GO
/****** Object:  StoredProcedure [dbo].[SHOW_RESULT]    Script Date: 22/04/2019 19:46:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[SHOW_RESULT]
AS
insert into Table_R (X, Operation, Y)
select x.X, CalcXY.Calc, y.Y from Table_X x cross join Table_Y y cross join CalcXY
