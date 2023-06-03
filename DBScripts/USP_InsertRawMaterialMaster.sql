create PROCEDURE [dbo].[USP_InsertRawMaterialMaster]     
(    
 @material_Purchases VARCHAR(15),  
 @raw_Material_Group  VARCHAR(200)  
)    
AS    
BEGIN    
 BEGIN TRY    
  INSERT INTO [dbo].[Raw_Material_Group_Master]  
           ([Material_Purchases]  
           ,[Raw_Material_Group])  
     VALUES  
           (@material_Purchases,@raw_Material_Group)  

	Select Cast(Scope_identity() as int) 
 END TRY    
 BEGIN CATCH    
      
 END CATCH    
END