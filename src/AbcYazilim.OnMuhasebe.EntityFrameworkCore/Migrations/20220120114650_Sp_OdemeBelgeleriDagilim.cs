using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AbcYazilim.OnMuhasebe.Migrations
{
    public partial class Sp_OdemeBelgeleriDagilim : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sqlProc = @"CREATE OR ALTER PROCEDURE [dbo].[OdemeBelgeleriDagilim] 
	                        @SubeId uniqueidentifier,
                            @DonemId uniqueidentifier                            	                          
                       AS
                       BEGIN
                           SET NOCOUNT ON
                           DECLARE @SQL VARCHAR(MAX)
                       
                           SET @SQL=  'SELECT   dbo.AppMakbuzHareketler.OdemeTuru, SUM(dbo.AppMakbuzHareketler.Tutar) AS Tutar
								       FROM     dbo.AppMakbuzHareketler LEFT OUTER JOIN
                                                dbo.AppMakbuzlar ON dbo.AppMakbuzHareketler.MakbuzId = dbo.AppMakbuzlar.Id
									   GROUP BY dbo.AppMakbuzHareketler.OdemeTuru, dbo.AppMakbuzlar.MakbuzTuru, dbo.AppMakbuzlar.DonemId, dbo.AppMakbuzlar.SubeId, dbo.AppMakbuzHareketler.IsDeleted							         
								       HAVING	dbo.AppMakbuzlar.SubeId ='''+CONVERT(varchar(MAX), @SubeId)+''' AND 
                                                dbo.AppMakbuzlar.DonemId ='''+CONVERT(varchar(MAX), @DonemId)+''' AND                                                       
                                                dbo.AppMakbuzlar.MakbuzTuru = 1 AND 
												dbo.AppMakbuzHareketler.IsDeleted=0'                                       
                       END
 
                       EXEC(@SQL);";

            migrationBuilder.Sql(sqlProc);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROC OdemeBelgeleriDagilim");
        }
    }
}
