create PROCEDURE [dbo].[USP_InsertRawMaterialDetails]     
(    
 @Raw_Material_Group_Code VARCHAR(6),  
 @Raw_Material_Details_Name VARCHAR(100),
 @Raw_Material_QC_Norms VARCHAR(5)
           ,@Raw_Material_UOM VARCHAR(15)
           ,@Raw_Material_Reorder_Stock int
           ,@Raw_Material_HSN_CODE_No int
           ,@Raw_Material_IGST_Rate int
           ,@Raw_Material_CGST_Rate int
           ,@Raw_Material_SGST_Rate int
           ,@Raw_Material_Cess_Rate int
)    
AS    
BEGIN    
 BEGIN TRY    
  INSERT INTO [dbo].[Raw_Material_Details]
           ([Raw_Material_Group_Code]
           ,[Raw_Material_Details_Name]
           ,[Raw_Material_QC_Norms]
           ,[Raw_Material_UOM]
           ,[Raw_Material_Reorder_Stock]
           ,[Raw_Material_HSN_CODE_No]
           ,[Raw_Material_IGST_Rate]
           ,[Raw_Material_CGST_Rate]
           ,[Raw_Material_SGST_Rate]
           ,[Raw_Material_Cess_Rate])
     VALUES
           (@Raw_Material_Group_Code
           ,@Raw_Material_Details_Name
           ,@Raw_Material_QC_Norms
           ,@Raw_Material_UOM
           ,@Raw_Material_Reorder_Stock
           ,@Raw_Material_HSN_CODE_No
           ,@Raw_Material_IGST_Rate
           ,@Raw_Material_CGST_Rate
           ,@Raw_Material_SGST_Rate
           ,@Raw_Material_Cess_Rate)

	Select Cast(Scope_identity() as int) 
 END TRY    
 BEGIN CATCH    
      
 END CATCH    
END