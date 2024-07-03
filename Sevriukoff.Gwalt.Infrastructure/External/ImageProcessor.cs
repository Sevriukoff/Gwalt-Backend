using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Png;

namespace Sevriukoff.Gwalt.Infrastructure.External;

public class ImageProcessor
{
    public Stream GenerateAvatar(string initials, int imageWidth = 100, int imageHeight = 100, int fontSize = 36,
        Color topColor = default, Color bottomColor = default)
    {
        LinearGradientBrush gradientBrush = null;
        
        if (topColor == default && bottomColor == default)
        {
            var baseColor = GetRandomColor(GetBasicColors());
            var darkenedColor = GetDarkenColor(baseColor, 0.2f);
            
            gradientBrush = new LinearGradientBrush(
                new PointF(0, imageHeight),
                new PointF(0, 0),
                GradientRepetitionMode.None,
                new ColorStop(0, darkenedColor),
                new ColorStop(1, baseColor)
            );
        }
        else
        {
            gradientBrush = new LinearGradientBrush(
                new PointF(0, imageHeight),
                new PointF(0, 0),
                GradientRepetitionMode.None,
                new ColorStop(0, GetDarkenColor(bottomColor, 0.1f)),
                new ColorStop(1, topColor)
            );
        }
        
        using var image = new Image<Rgba32>(imageWidth, imageHeight);
        
        image.Mutate(x => x.Fill(gradientBrush));
        
        var font = SystemFonts.CreateFont("Arial", fontSize);
        
        var textOptions = new TextOptions(font)
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            Origin = new PointF(imageWidth / 2, imageHeight / 2)
        };
        
        var richTextOptions = new RichTextOptions(font)
        {
            Origin = new PointF(imageWidth / 2, imageHeight / 2),
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };
        
        image.Mutate(x => x.DrawText(richTextOptions, initials, Color.White));
        
        var ms = new MemoryStream();
        image.Save(ms, new PngEncoder());
        
        return ms;
    }
    
    public Stream GenerateBackground(int width = 2480, int height = 520,
        Color startColor = default, Color endColor = default)
    {
        if (startColor == default && endColor == default)
        {
            startColor = GetRandomPastelColor();
            endColor = GetRandomPastelColor();
        }
        
        using var image = new Image<Rgba32>(width, height);
        
        var gradientBrush = new LinearGradientBrush(
            new PointF(0, 0),
            new PointF(width, height),
            GradientRepetitionMode.None,
            new ColorStop(0, startColor),
            new ColorStop(1, endColor)
        );
        
        image.Mutate(x => x.Fill(gradientBrush));
        
        var ms = new MemoryStream();
        image.Save(ms, new PngEncoder());
        
        return ms;
    }
    
    public async Task<Stream> CropImageAsync(Stream imageStream, int x, int y, int width, int height)
    {
        var outputStream = new MemoryStream();
        imageStream.Seek(0, SeekOrigin.Begin);
        
        using var image = await Image.LoadAsync(imageStream);
        image.Mutate(ctx => ctx.Crop(new Rectangle(x, y, width, height)));
        
        await image.SaveAsJpegAsync(outputStream);
        outputStream.Seek(0, SeekOrigin.Begin);

        return outputStream;
    }
    
    public Color GetRandomPastelColor()
    {
        var random = new Random();
        byte r = (byte)((random.Next(256) + 255 + 255) / 3);
        byte g = (byte)((random.Next(256) + 255 + 255) / 3);
        byte b = (byte)((random.Next(256) + 255 + 255) / 3);
        return Color.FromRgb(r, g, b);
    }
    
    public Color GetSimilarPastelColor(Color baseColor)
    {
        var random = new Random();
        var (h, s, v) = RgbToHsv(baseColor);
        var hueShift = GenerateShift(random);
        var newHue = (h + hueShift) % 360;
        if (newHue < 0) newHue += 360;
        return HsvToRgb(newHue, s, v);
    }

    #region PrivateMethods
    
    private Color GetDarkenColor(Color color, float factor)
    {
        var pixel = color.ToPixel<Rgba32>();
        return Color.FromRgba(
            (byte)(pixel.R * (1 - factor)),
            (byte)(pixel.G * (1 - factor)),
            (byte)(pixel.B * (1 - factor)),
            pixel.A);
    }

    private Color GetRandomColor(Color[] colors)
    {
        var random = new Random();
        return colors[random.Next(colors.Length)];
    }
    
    private int GenerateShift(Random random)
        => random.Next(2) == 0 ? random.Next(-60, -29) : random.Next(30, 61);

    private (float H, float S, float V) RgbToHsv(Color color)
    {
        var pixel = color.ToPixel<Rgba32>();
        float r = pixel.R / 255f;
        float g = pixel.G / 255f;
        float b = pixel.B / 255f;

        float max = Math.Max(r, Math.Max(g, b));
        float min = Math.Min(r, Math.Min(g, b));
        float delta = max - min;

        float h = 0;
        if (delta != 0)
        {
            if (max == r)
                h = 60 * (((g - b) / delta) % 6);
            else if (max == g)
                h = 60 * (((b - r) / delta) + 2);
            else if (max == b)
                h = 60 * (((r - g) / delta) + 4);
        }

        float s = max == 0 ? 0 : delta / max;
        float v = max;

        return (h, s, v);
    }
    
    private Color HsvToRgb(float h, float s, float v)
    {
        float c = v * s;
        float x = c * (1 - Math.Abs((h / 60) % 2 - 1));
        float m = v - c;

        float r, g, b;
        if (h < 60)
        {
            r = c;
            g = x;
            b = 0;
        }
        else if (h < 120)
        {
            r = x;
            g = c;
            b = 0;
        }
        else if (h < 180)
        {
            r = 0;
            g = c;
            b = x;
        }
        else if (h < 240)
        {
            r = 0;
            g = x;
            b = c;
        }
        else if (h < 300)
        {
            r = x;
            g = 0;
            b = c;
        }
        else
        {
            r = c;
            g = 0;
            b = x;
        }

        byte R = (byte)((r + m) * 255);
        byte G = (byte)((g + m) * 255);
        byte B = (byte)((b + m) * 255);

        return Color.FromRgb(R, G, B);
    }
    
    private Color[] GetBasicColors()
    {
        return new Color[]
        {
            Color.FromRgb(255, 179, 186), // Light Pink
            Color.FromRgb(255, 223, 186), // Light Peach
            Color.FromRgb(255, 255, 186), // Light Yellow
            Color.FromRgb(186, 255, 201), // Light Green
            Color.FromRgb(186, 225, 255), // Light Blue
            Color.FromRgb(201, 186, 255), // Light Purple
            Color.FromRgb(255, 186, 255)  // Light Magenta
        };
    }
    
    #endregion
}