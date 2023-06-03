IF NOT EXISTS (SELECT * FROM SYS.objects WHERE TYPE = 'P' AND OBJECT_ID = OBJECT_ID('dbo.USP_GetAreaDetailsById'))
	EXEC('CREATE PROCEDURE [dbo].[USP_GetAreaDetailsById] AS BEGIN SET NOCOUNT ON; SET QUOTED_IDENTIFIER ON; END')
GO
ALTER PROCEDURE [dbo].[USP_GetAreaDetailsById] 
(
	@Area_ID VARCHAR(10)
)
AS
BEGIN
	BEGIN TRY
		SELECT HAD.Area_Code,HAD.Area_Name,SD.State_Code,SD.State_Name,
		DD.District_Code,DD.District_Name,VD.Village_Code,VD.Village_Name 
		FROM Harvest_Area_Village_Details HAVD
		INNER JOIN Harvest_Area_Details HAD ON HAD.Area_ID = HAVD.Area_ID
		INNER JOIN Country_Details CD ON CD.Country_Code = HAVD.Country_Code
		INNER JOIN State_Details SD ON SD.State_Code = HAVD.State_Code
		INNER JOIN District_Details DD ON DD.District_Code = HAVD.District_Code
		INNER JOIN Village_Details VD ON VD.Village_Code = HAVD.Village_Code
		WHERE HAVD.Area_ID = @Area_ID
	END TRY
	BEGIN CATCH
		
	END CATCH
END