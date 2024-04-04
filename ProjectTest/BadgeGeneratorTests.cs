using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using System.IO;

[TestClass]
public class BadgeGeneratorTests
{
    [TestMethod]
    public void GenerateBadge_CreatesExpectedImage()
    {
        // Arrange
        var generator = new BaidgeGenerator();
        string fullName = "John Doe";
        string about = "Software Developer";
        string url = "http://example.com";
        Stream userImageStream = File.OpenRead("00f297d5-4d97-4c03-ae7d-4b332d986a03.jpg"); // Убедитесь, что здесь указан правильный путь к тестовому изображению


        // Act
        Bitmap badge = generator.GenerateBadge(fullName, about, url, userImageStream);

        // Assert
        Assert.IsNotNull(badge);
        // Здесь можно добавить дополнительные проверки, например, размеры изображения, цвета и т.д.
    }
}
