using System.ComponentModel.DataAnnotations;

namespace WebMatrixUploader.Data.DataForMatrix;

public class CurveData
{
    [Key] public int CurveDataId { get; set; }
    public int FileId { get; set; }
}