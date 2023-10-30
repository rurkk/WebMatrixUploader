namespace WebMatrixUploader.Services.Matrix;
using MathNet.Numerics.LinearAlgebra;

public class MatrixService
{
    public Matrix<double> ReadMatrixFromTextFile(string filePath)
    {
        try
        {
            var lines = File.ReadAllLines(filePath);
            var data = new List<List<double>>();
            foreach (var line in lines)
            {
                var values = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(double.Parse)
                    .ToList();
                data.Add(values);
            }

            return Matrix<double>.Build.DenseOfRows(data);
        }
        catch (Exception ex)
        {
            // Обработка ошибок, если файл не является матрицей или содержит недопустимые значения
            throw new Exception("Failed to read matrix from file.", ex);
        }
    }
}