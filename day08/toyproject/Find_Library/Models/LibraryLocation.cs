using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Find_Library.Models
{
    public class LibraryLocation
    {
        public int Id { get; set; }
        public string 주요_매출원 { get; set; }
        public string 휴무일 { get; set; }
        public string 개점일_개관일 { get; set; }
        public string 운영시간 { get; set; }
        public string 이름 { get; set; }
        public string 전화번호 { get; set; }
        public string 주소 { get; set; }
        public string 웹사이트 { get; set; }
        public string 상세주소 { get; set; }
        public string 대표자_운영주체 { get; set; }
        public string 총면적_연면적_m2_ { get; set; }
        public string 구분 { get; set; }
        public int 좌석수 { get; set; }
        public double 경도 { get; set; }
        public string 보유도서량 { get; set; }
        public string E_mail { get; set; }
        public double 위도 { get; set; }
        public string 공간유형 { get; set; }
        public int Objectid { get; set; }

        public static readonly string SELECT_QUERY = @"SELECT [Id]
                                                              ,[주요_매출원]
                                                              ,[휴무일]
                                                              ,[개점일_개관일]
                                                              ,[운영시간]
                                                              ,[이름]
                                                              ,[전화번호]
                                                              ,[주소]
                                                              ,[웹사이트]
                                                              ,[상세주소]
                                                              ,[대표자_운영주체]
                                                              ,[총면적_연면적_m2_]
                                                              ,[구분]
                                                              ,[좌석수]
                                                              ,[경도]
                                                              ,[보유도서량]
                                                              ,[E_mail]
                                                              ,[위도]
                                                              ,[공간유형]
                                                              ,[Objectid]
                                                          FROM [dbo].[LibraryItem]";
        public static readonly string INSERT_QUERY = @"INSERT INTO [dbo].[LibraryItem]
                                                                   ([주요_매출원]
                                                                   ,[휴무일]
                                                                   ,[개점일_개관일]
                                                                   ,[운영시간]
                                                                   ,[이름]
                                                                   ,[전화번호]
                                                                   ,[주소]
                                                                   ,[웹사이트]
                                                                   ,[상세주소]
                                                                   ,[대표자_운영주체]
                                                                   ,[총면적_연면적_m2_]
                                                                   ,[구분]
                                                                   ,[좌석수]
                                                                   ,[경도]
                                                                   ,[보유도서량]
                                                                   ,[E_mail]
                                                                   ,[위도]
                                                                   ,[공간유형]
                                                                   ,[Objectid])
                                                             VALUES
                                                                   (@주요_매출원
                                                                   ,@휴무일
                                                                   ,@개점일_개관일
                                                                   ,@운영시간
                                                                   ,@이름
                                                                   ,@전화번호
                                                                   ,@주소
                                                                   ,@웹사이트
                                                                   ,@상세주소
                                                                   ,@대표자_운영주체
                                                                   ,@총면적_연면적_m2_
                                                                   ,@구분
                                                                   ,@좌석수
                                                                   ,@경도
                                                                   ,@보유도서량
                                                                   ,@E_mail
                                                                   ,@위도
                                                                   ,@공간유형
                                                                   ,@Objectid)";
        public static readonly string GETDATE_QUERY = @"SELECT CONVERT(CHAR(10), 구분, 11) AS Save_Date
                                                          FROM [LibraryItem]
                                                         GROUP BY CONVERT(CHAR(10), 구분, 11)";
    }
}