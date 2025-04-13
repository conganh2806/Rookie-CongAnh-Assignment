using ECommerce.Core.Entities;
using ECommerce.Domain.Interfaces;
using ECommerce.Infrastructure.Persistence;

namespace ECommerce.Infrastructure.Repositories
{
    public class MediaFileRepository : IMediaFileRepository
    {
        private readonly ApplicationDbContext _context;

        public MediaFileRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<MediaFile> Entity => _context.MediaFiles.AsQueryable();

        public IUnitOfWork UnitOfWork => _context;

        public void Add(MediaFile entity)
        {
            _context.MediaFiles.Add(entity);
        }

        public void Delete(MediaFile entity)
        {
            _context.MediaFiles.Remove(entity);
        }

        public void Update(MediaFile entity)
        {
            _context.MediaFiles.Update(entity);
        }
    }
}