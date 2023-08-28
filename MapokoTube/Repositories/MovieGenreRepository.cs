using System.Data;
using MapokoTube.Interfaces;
using MapokoTube.Models;
using MySql.Data.MySqlClient;

namespace MapokoTube.Repositories;

public class VideoTagRepository : IVideoTagRepository
{
    readonly string connectionString = "server=localhost;port=3306;database=MapokoTubedb;uid=root;pwd=''";

    public void Create(int VideoId, byte TagId)
    {
        MySqlConnection connection = new(connectionString);
        string sql = "insert into VideoTag(VideoId, TagId) values (@VideoId, @TagId)";
        MySqlCommand command = new(sql, connection)
        {
            CommandType = CommandType.Text
        };
        command.Parameters.AddWithValue("@VideoId", VideoId);
        command.Parameters.AddWithValue("@TagId", TagId);
        
        connection.Open();
        command.ExecuteNonQuery();
        connection.Close();
    }

    public void Delete(int VideoId, byte TagId)
    {
        MySqlConnection connection = new(connectionString);
        string sql = "delete from VideoTag where VideoId = @VideoId and TagId = @TagId";
        MySqlCommand command = new(sql, connection)
        {
            CommandType = CommandType.Text
        };
        command.Parameters.AddWithValue("@VideoId", VideoId);
        command.Parameters.AddWithValue("@TagId", TagId);
        
        connection.Open();
        command.ExecuteNonQuery();
        connection.Close();
    }

    public void Delete(int VideoId)
    {
        MySqlConnection connection = new(connectionString);
        string sql = "delete from VideoTag where VideoId = @VideoId";
        MySqlCommand command = new(sql, connection)
        {
            CommandType = CommandType.Text
        };
        command.Parameters.AddWithValue("@VideoId", VideoId);
        
        connection.Open();
        command.ExecuteNonQuery();
        connection.Close();
    }

    public List<Tag> ReadGenresByMovie(int VideoId)
    {
        MySqlConnection connection = new(connectionString);
        string sql = "select * from genre where id in "
                   + "(select TagId from VideoTag where VideoId = @VideoId)";
        MySqlCommand command = new(sql, connection)
        {
            CommandType = CommandType.Text
        };
        command.Parameters.AddWithValue("@VideoId", VideoId);
        
        List<Tag> tags = new();
        connection.Open();
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            Tag tag = new()
            {
                Id = reader.GetByte("id"),
                Name = reader.GetString("name")
            };
            tags.Add(tag);
        }
        connection.Close();
        return tags;
    }

    public List<VideoTag> ReadVideoTag()
    {
        MySqlConnection connection = new(connectionString);
        string sql = "select * from VideoTag";
        MySqlCommand command = new(sql, connection)
        {
            CommandType = CommandType.Text
        };
        
        List<VideoTag> VideoTags = new();
        connection.Open();
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            VideoTag VideoTag = new()
            {
                VideoId = reader.GetInt32("VideoId"),
                TagId = reader.GetByte("TagId")
            };
            VideoTags.Add(VideoTag);
        }
        connection.Close();
        return VideoTags;
    }

    public List<Video> ReadMoviesByGenre(byte TagId)
    {
        MySqlConnection connection = new(connectionString);
        string sql = "select * from movie where id in "
                   + "(select VideoId from VideoTag where TagId = @TagId)";
        MySqlCommand command = new(sql, connection)
        {
            CommandType = CommandType.Text
        };
        command.Parameters.AddWithValue("@TagId", TagId);
        
        List<Video> movies = new();
        connection.Open();
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            Video video = new()
            {
                Id = reader.GetInt32("id"),
                Name = reader.GetString("name"),
                Description = reader.GetString("description"),
                UploadDate = reader.GetDateTime("uploadDate"),
                Duration = reader.GetInt16("duration"),
                Thumbnail = reader.GetString("thumbnail")
            };
            movies.Add(video);
        }
        connection.Close();
        return movies;
    }
}