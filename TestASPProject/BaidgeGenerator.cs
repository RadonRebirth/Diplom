using MessagingToolkit.QRCode.Codec;
using System.Drawing;
using System.IO;

public class BaidgeGenerator
{
    public Bitmap GenerateBadge(string fullName, string about, string url, Stream userImageStream)
    {
        // Генерация QR-кода
        QRCodeEncoder qrEncoder = new QRCodeEncoder();
        qrEncoder.QRCodeVersion = 7;
        qrEncoder.QRCodeScale = 3;
        Bitmap qrCodeImage = qrEncoder.Encode(url);

        // Создание изображения бейджа
        Bitmap badge = new Bitmap(400, 300);
        using (Graphics graphics = Graphics.FromImage(badge))
        {
            graphics.FillRectangle(Brushes.White, 0, 0, badge.Width, badge.Height);

            if (userImageStream != null)
            {
                using (Image userImage = Image.FromStream(userImageStream))
                {
                    graphics.DrawImage(userImage, new Rectangle(80, 10, 120, 140));
                }
            }

            graphics.DrawImage(qrCodeImage, 210, 10);

            // Отрисовка текста
            // ... (оставшаяся часть кода для отрисовки текста)
        }

        return badge;
    }
}
