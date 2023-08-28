using MapokoTube.Models;

namespace MapokoTube.Interfaces;

public interface IVideoTagRepository
{
    void Create(int VideoId, byte TagId);

    void Delete(int VideoId, byte TagId);

    void Delete(int VideoIdd);

    List<VideoTag> ReadVideoTag();

    List<Video> ReadVideosByTag(byte TagId);

    List<Tag> ReadTagsByVideo(int VideoId);
}