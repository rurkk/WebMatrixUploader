using System.Globalization;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using WebMatrixUploader.Data;
using WebMatrixUploader.Data.DataForMatrix;
using WebMatrixUploader.Services.Matrix;
using MathNet.Numerics.LinearAlgebra;

namespace WebMatrixUploader.Controllers;

public class FileController : Controller
{
    private readonly ApplicationDbContext _context;

    public FileController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult UploadFile(IFormFile? file, int cellCount, string xColumnNames, string yColumnNames)
    {
        // If file is empty do nothing
        if (file is not { Length: > 0 }) return RedirectToAction("Index", "Home");

        // Folder selector
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId is null) throw new ArgumentNullException(nameof(userId));
        var userFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads", userId);
        if (!Directory.Exists(userFolderPath)) Directory.CreateDirectory(userFolderPath);
        // New filename generator
        var fileName = Path.GetFileName(file.FileName);
        var filePath = Path.Combine(userFolderPath, fileName);
        var fileExtension = Path.GetExtension(file.FileName).ToLower();
        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
        var count = 1;
        while (System.IO.File.Exists(filePath))
        {
            fileName = $"{fileNameWithoutExtension}({count}){fileExtension}";
            filePath = Path.Combine(userFolderPath, fileName);
            count++;
        }
        
        // Check file is matrix and Upload to server if ok
        try
        {
            using var reader = new StreamReader(file.OpenReadStream());
            var matrix = ReadMatrixFromStream(reader);
            if (matrix.RowCount == 1 && cellCount == 1)
            {
                var rowIndices = Vector<double>.Build.Dense(matrix.ColumnCount, i => i + 1);
                matrix = matrix.InsertRow(0, rowIndices);
                var matrixString = string.Join(Environment.NewLine, matrix.ToColumnArrays().Select(row => string.Join(" ", row)));
                // Uploading file to server
                using StreamWriter sw = new StreamWriter(filePath);
                sw.Write(matrixString);
            }
            else if (matrix.RowCount == cellCount * 2)
            {
                // Uploading file to server
                using var stream = new FileStream(filePath, FileMode.Create);
                file.CopyTo(stream);
            }
            else
            {
                TempData["ErrorMessage"] = "Number of entered columns does not match the matrix";
                return RedirectToAction("Index", "Home");
            }
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "There is no matrix in file or it is in the wrong format";
            return RedirectToAction("Index", "Home");
        }
        
        // Uploading new Data to DataBase
        var fileRecord = new UserFile
        {
            FileName = fileName,
            FilePath = filePath,
            UserId = userId
        };
        _context.UsersFiles.Add(fileRecord);

        var xColumns = JsonSerializer.Deserialize<List<string>>(xColumnNames);
        var yColumns = JsonSerializer.Deserialize<List<string>>(yColumnNames);
        for (var i = 0; i < cellCount; i++)
        {
            var curveDataRecord = new CurveData
            {
                FileId = fileRecord.FileId,
            };
            _context.CurveData.Add(curveDataRecord);

            var abscissaRecord = new Abscissa
            {
                Name = xColumns?[i],
                CurveDataId = curveDataRecord.CurveDataId,
            };
            _context.Abscissa.Add(abscissaRecord);
            
            var ordinateRecord = new Ordinate
            {
                Name = yColumns?[i],
                CurveDataId = curveDataRecord.CurveDataId,
            };
            _context.Ordinates.Add(ordinateRecord);
        }
        
        _context.SaveChanges();

        return RedirectToAction("Index", "Home");
    }
    
    
    private Matrix<double> ReadMatrixFromStream(TextReader reader)
    {
        var matrix = Matrix<double>.Build.DenseOfMatrix(MathNet.Numerics.LinearAlgebra.Double.DenseMatrix.OfColumnArrays(
            reader.ReadToEnd().Split('\n')
                .Select(row => row.Split(' ')
                    .Select(double.Parse)
                    .ToArray()
                )
                .ToArray()
        ));
    
        return matrix;
    }
}