USE ONLINE_SHOP
GO

/* Bài 11 : tính tổng 2 số( Nguyên ) a, b và in kết quả  */
IF OBJECT_ID('uspTinhtong1') IS NOT NULL
	DROP PROC uspTinhtong1
GO
CREATE PROC uspTinhtong1
	@X INT = 0,
	@Y INT = 0
AS
	RETURN @X + @Y
GO

--GỌI
DECLARE @X INT = 10 , @Y INT = 20  , @TONG INT
EXEC @TONG = uspTinhtong1 --@X,@Y
PRINT @TONG

/* Bài 11.1 : tính tổng 2 số( Thực ) a, b và in kết quả  */
IF OBJECT_ID('uspTinhtong2') IS NOT NULL
	DROP PROC uspTinhtong2
GO
CREATE PROC uspTinhtong2
	@X FLOAT = 0.0 ,
	@Y FLOAT = 0.0 ,
	@TONG FLOAT OUTPUT
AS
	SET @TONG = @X + @Y
GO

--GỌI
DECLARE @X FLOAT = 10.5 , @Y FLOAT = 20.2  , @TONG FLOAT = 0.5
EXEC uspTinhtong2 @X,@Y,@TONG OUT
PRINT CAST (@X AS nvarchar) + '+' +
CAST (@Y AS nvarchar) + '=' +
CAST (@TONG AS nvarchar)