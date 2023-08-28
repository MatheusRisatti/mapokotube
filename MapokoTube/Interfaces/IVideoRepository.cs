using MapokoTube.Models;

namespace MapokoTube.Interfaces;

public interface IVideoRepository : IRepository<Video>
{
    List<Video> ReadAllDetailed();

    Video ReadByIdDetailed(int id);
}
