using Microsoft.EntityFrameworkCore;
using NASAProj.Data.IRepositories;
using NASAProj.Domain.Configurations;
using NASAProj.Domain.Models;
using NASAProj.Service.DTOs;
using NASAProj.Service.Exceptions;
using NASAProj.Service.Extensions;
using NASAProj.Service.Helpers;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Linq.Expressions;
using NASAProj.Domain.Enums;
using NASAProj.Service.Interfaces;

namespace NASAProj.Service.Services
{
#pragma warning disable
    public class CertificateService : ICertificateService
    {
        private readonly IAttachmentService attachmentService;
        private readonly IGenericRepository<User> userRepository;
        private readonly IGenericRepository<Certificate> certificateRepository;
        public CertificateService(IAttachmentService attachmentService,
            IGenericRepository<User> userRepository,
            IGenericRepository<Certificate> certificateRepository)
        {
            this.attachmentService = attachmentService;
            this.userRepository = userRepository;
            this.certificateRepository = certificateRepository;
        }

        public async ValueTask<Attachment> CreateAsync(CertificateForCreationDTO certificateForCreation)
        {
            var user = await userRepository.GetAsync(u => u.Id.Equals(certificateForCreation.UserId));

            var certifcate = await GenerateAsync(user.FirstName + " " + user.LastName,certificateForCreation.Result.PassedPoint, certificateForCreation.Result.Percentage);
            var attachment = await attachmentService.CreateAsync(certifcate.fileName, certifcate.filePath);

            return attachment;
        }

        public async ValueTask<IEnumerable<Certificate>> GetAllAsync(PaginationParams @params, Expression<Func<Certificate, bool>> expression = null)
        {
            var certificates = certificateRepository.GetAll(expression, "Attachment");

            return await certificates.Where(c => c.UserId.Equals(HttpContextHelper.UserId)).ToPagedList(@params).ToListAsync();
        }

        public async ValueTask<Certificate> GetAsync(Expression<Func<Certificate, bool>> expression)
        {
            var certificate = await certificateRepository.GetAsync(expression, "Attachment");

            if (certificate is null)
                throw new HttpStatusCodeException(404, "Certificate not found");

            return certificate;
        }

        private ValueTask<(string fileName, string filePath)> GenerateAsync(string fullName, string passedPoint, double percentage)
        {
            string filePath = Path.Combine(EnvironmentHelper.WebRootPath, "certificate.png");

            Bitmap bitmap = new Bitmap(filePath);

            // determine the level
            string result = string.Empty;

            if (percentage <= 75)
                result = Enum.GetName(typeof(CertificateLevel), 0);
            else if (percentage > 75 && percentage < 90)
                result = Enum.GetName(typeof(CertificateLevel), 1);
            else if (percentage >= 90)
                result = Enum.GetName(typeof(CertificateLevel), 2);

            // initialize Graphics class object
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            Brush brush = new SolidBrush(Color.FromKnownColor(KnownColor.Black));
            Brush brushForLevel = new SolidBrush(Color.FromArgb(36, 38, 93));

            // set text font
            Font arialOfName = new Font("Arial", 30, FontStyle.Italic);
            Font arialOfCourseName = new Font("Arial", 16, FontStyle.Italic);
            Font arialOfLevel = new Font("Arial", 25, FontStyle.Italic);
            Font arialOfPassedPoint = new Font("Arial", 10, FontStyle.Italic);

            // draw text
            SizeF sizeOfName = graphics.MeasureString(fullName, arialOfName);
            SizeF sizeOfPassedPoint = graphics.MeasureString(passedPoint, arialOfName);
            SizeF sizeOfLevel = graphics.MeasureString(result, arialOfLevel);

            graphics.DrawString(fullName, arialOfName, brush, new PointF((bitmap.Width - sizeOfName.Width) / 2, 725));
            graphics.DrawString(result, arialOfLevel, brushForLevel, new PointF((bitmap.Width - sizeOfLevel.Width) / 2, 1000));
            graphics.DrawString(passedPoint, arialOfPassedPoint, brush, new PointF((bitmap.Width - sizeOfPassedPoint.Width) / 2, 1030));

            string outputFileName = Guid.NewGuid().ToString("N") + ".png";
            string staticPath = Path.Combine(EnvironmentHelper.CertificatePath, outputFileName);
            string outputFilePath = Path.Combine(EnvironmentHelper.WebRootPath, staticPath);

            bitmap.Save(outputFilePath, ImageFormat.Png);

            return ValueTask.FromResult((outputFileName, staticPath));
        }
    }
}
